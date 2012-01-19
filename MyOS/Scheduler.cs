using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MyOS
{
    class Scheduler
    {
        public const int JOB = 0;
        public const int READY = 1;
        public const int RUN = 2;
        public const int DEVICE = 3;

        public Thread createThread, readyThread;
        public Thread runThread, ioThread, compactThread;
        private Queue jobQueue, readyQueue, deviceQueue;
        private Processor processor;
        private Memory memory;
        private static ArrayList processes;

        private string statusJob = "Waiting for new process.";
        private string statusReady = "Initializing Ready Queue.";
        private string statusRun = "Processor starting up";
        private string statusDevice = "Nothing occupies Device Queue.";
        private bool isCompact, isRegular, isDemand, isUpdate;
        private int counter;

        public Scheduler(Memory memory, Processor processor)
        {
            this.memory = memory;
            this.processor = processor;
            jobQueue = new Queue(10);
            readyQueue = new Queue(10);
            deviceQueue = new Queue(10);
            processes = new ArrayList();
            
            createThread = new Thread(Create);
            readyThread = new Thread(Ready);
            runThread = new Thread(Run);
            ioThread = new Thread(IO);
            compactThread = new Thread(Compact);
        }

        #region Thread Management
        /**This method springboards the threads to be run by the scheduler.
         * */
        public void Start(bool isCompact, bool isRegular, bool isDemand)
        {
            this.isCompact = isCompact;
            this.isRegular = isRegular;
            this.isDemand = isDemand;

            createThread.Start();
            readyThread.Start();
            runThread.Start();
            ioThread.Start();

            if (isCompact)
            {
                compactThread.Start();
            }
        }

        /**This method stops the threads being run by the scheduler.
         * */
        public void Stop()
        {
            createThread.Abort();
            readyThread.Abort();
            runThread.Abort();
            ioThread.Abort();
            if (isCompact)
            {
                compactThread.Abort();
            }
        }

        /**This method halts the threads being run by the scheduler.
         * */
        public void Pause(bool isPause)
        {
            if (isPause)
            {
                createThread.Suspend();
                readyThread.Suspend();
                runThread.Suspend();
                ioThread.Suspend();
                if (isCompact)
                {
                    compactThread.Suspend();
                }
            }
            else
            {
                createThread.Resume();
                readyThread.Resume();
                runThread.Resume();
                ioThread.Resume();
                if (isCompact)
                {
                    compactThread.Resume();
                }
            }
        }
        #endregion

        #region Long-Term Scheduler
        /**This method facilitates Job Creation and regulates the Job Queue.
         * */
        private void Create()
        {
            ProcessControlBlock pcb = null;
            while (true)
            {
                lock (jobQueue)
                {
                    if (readyQueue.Count < 4)
                    {
                        pcb = new ProcessControlBlock(counter+1);
                        if (jobQueue.Count == 0 && PlaceInMemory(pcb))
                        {
                            counter++;
                        }
                        else if (jobQueue.Count < 3)
                        {
                            pcb.State = ProcessControlBlock.NEW;
                            AddProcess(pcb);
                            jobQueue.Enqueue(pcb);
                            counter++;
                            statusJob = "Created Process " + pcb.ID + ".";
                        }
                    }
                }
                Thread.Sleep(3000);
            }
        }

        /**This method facilitates Process Creation and Processing Requeuing 
         * while regulating the Job Queue.
         * */
        private void Ready()
        {
            ProcessControlBlock pcb = null;
            while (true)
            {
                Thread.Sleep(200);
                {
                    if (jobQueue.Count > 0 && (readyQueue.Count < 4 || isDemand))
                    {
                        pcb = (ProcessControlBlock)(jobQueue.Peek());
                        if (isCompact && pcb.MemSize > memory.FreeMemory)
                        {
                            continue;
                        }
                        else if (isRegular && pcb.Request > memory.FreePages)
                        {
                            continue;
                        }

                        if (PlaceInMemory(pcb))
                        {
                            lock (jobQueue)
                            {
                                jobQueue.Dequeue();
                            }
                        }
                    }
                }
            }
        }

        /** Checks and allocates if a process has enough space to occupy in memory.
         * */
        private bool PlaceInMemory(ProcessControlBlock pcb)
        {
            bool isProceed = false;
            if (isCompact)                  /**Performs compacting of the memory */
            {
                pcb.BaseNum = memory.Fill(pcb.ID, pcb.MemSize);
                if (pcb.BaseNum != Memory.FULL)
                {
                    lock (memory)
                    {
                        memory.UsedMemory += pcb.MemSize;
                    }
                    pcb.Limit = pcb.BaseNum + pcb.MemSize - 1;
                    isProceed = true;
                }
            }
            else                            /**Assigns pages to the current PCB */
            {
                lock (memory)
                {
                    pcb.Pages = memory.CreatePages(pcb.MemSize, pcb.Request, isDemand);
                }
                
                if (pcb.Pages != null)
                {
                    isProceed = true;
                }
                else if (isDemand)
                {
                    pcb.Pages = SelectVictims(2);
                    isProceed = pcb.Pages.Count > 0;
                }
            }
            if (isProceed)                  /** Queue new process to ready queue */
            {
                pcb.State = ProcessControlBlock.READY;
                statusReady = "Queing Process " + pcb.ID + " in Ready Queue.";
                lock (readyQueue)
                {
                    readyQueue.Enqueue(pcb);
                }
                AddProcess(pcb);
                isUpdate = true;
            }
            return isProceed;
        }
        #endregion

        /**This method facilitates the compaction of the memory.
         * */
        private void Compact()
        {
            while (true)
            {
                lock (memory)
                {
                    int curBase = memory.OSBlockSize;
                    memory.Clear(curBase);
                    lock (processes)
                    {
                        processes.Sort();
                        for (int i = 0; i < processes.Count; i++)
                        {
                            ProcessControlBlock pcb = (ProcessControlBlock)processes[i];
                            if (pcb.State != ProcessControlBlock.NEW)
                            {
                                pcb.BaseNum = curBase;
                                pcb.Limit = curBase + pcb.MemSize - 1;
                                curBase = pcb.Limit + 1;
                                memory.Fill(pcb.ID, pcb.MemSize);
                                AddProcess(pcb);
                                isUpdate = true;
                            }
                        }
                    }
                    isUpdate = true;
                }
                Thread.Sleep(2000);
            }
        }

        #region Short-Term Scheduler
        /**This method handles the running operation of a process, including
         * interruption and blocking.
         * */
        private void Run()
        {
            ProcessControlBlock pcb = null;
            while (true)
            {
                Thread.Sleep(1000);
                pcb = processor.ActivePCB;
                if (processor.IsFree)
                {
                    if (pcb != null && pcb.State == ProcessControlBlock.TERMINATING)
                    {
                        lock (memory)
                        {
                            if (isCompact)
                            {
                                memory.Free(pcb.BaseNum, pcb.MemSize);
                                memory.UsedMemory -= pcb.MemSize;
                            }
                            else
                            {
                                memory.Free(pcb.Pages);
                            }
                            lock (processor)
                            {
                                processor.FreeCPU(true);
                            }
                            RemoveProcess(pcb);
                            isUpdate = true;
                        }
                    }

                    pcb = StartProcess();
                }
                else if (pcb.State == ProcessControlBlock.INTERRUPTED)
                {
                    AddProcess(pcb);
                    Thread.Sleep(1000);
                    pcb.State = ProcessControlBlock.READY;
                    AddProcess(pcb);
                    readyQueue.Enqueue(pcb);
                    processor.FreeCPU(true);
                    statusReady = "Process " + pcb.ID + " was interrupted.";

                    pcb = StartProcess();
                }
                else if (pcb.State == ProcessControlBlock.READY)
                {
                    AddProcess(pcb);
                    readyQueue.Enqueue(pcb);
                    processor.FreeCPU(true);
                    statusReady = "Process " + pcb.ID + " returned to Ready Queue.";
                }
                else if (pcb.State == ProcessControlBlock.WAITING)
                {
                    AddProcess(pcb);
                    deviceQueue.Enqueue(pcb);
                    processor.FreeCPU(true);
                    statusDevice = "Process " + pcb.ID + " is waiting for a resource.";
                }
                else
                {
                    if (!isDemand)
                    {
                        continue;
                    }
                    if (pcb.Pages.Count < pcb.Request)
                    {
                        ArrayList pages = null;
                        int demand = 0;

                        lock (processor)
                        {
                            demand = processor.IsDemand(pcb.Request - pcb.Pages.Count);
                        }
                        lock (memory)
                        {
                            pages = memory.CreatePages(demand);
                            pcb.Pages.AddRange(pages.ToArray());
                        }
                        lock (processor)
                        {
                            demand = processor.IsDemand(pcb.Request - pcb.Pages.Count);
                        }

                        if (demand > 0)
                        {
                            pages = SelectVictims(demand);
                            pcb.Pages.AddRange(pages.ToArray());
                        }
                        AddProcess(pcb);
                    }
                }

                if (pcb != null)
                {
                    statusRun = "Running Process " + pcb.ID + " for " + pcb.CPUTime + " seconds.";
                }
            }
        }

        /** Mobilizes the CPU to execute a process from the Ready Queue*/
        private ProcessControlBlock StartProcess()
        {
            ProcessControlBlock pcb = null;
            lock (readyQueue)
            {
                if (readyQueue.Count > 0)
                {
                    pcb = (ProcessControlBlock)(readyQueue.Dequeue());
                    pcb.AddResources();
                    pcb.State = ProcessControlBlock.RUNNING;
                    AddProcess(pcb);
                    lock (processor)
                    {
                        processor.ActivePCB = pcb;
                        processor.Start();
                    }
                }
            }
            return pcb;
        }

        /**Targets processes waiting in the ready queue to give off some 
         * of their pages to other processes. */
        private ArrayList SelectVictims(int needed)
        {
            Stack<ProcessControlBlock> stack1 = new Stack<ProcessControlBlock>();
            Stack<ProcessControlBlock> stack2 = new Stack<ProcessControlBlock>();
            ArrayList acquiredPages = new ArrayList();

            if (needed == 0)
            {
                return acquiredPages;
            }

            lock (readyQueue)
            {
                for(int i = 0; i < readyQueue.Count; i++)
                {
                    ProcessControlBlock pcb = (ProcessControlBlock)readyQueue.Dequeue();
                    stack1.Push(pcb);
                    readyQueue.Enqueue(pcb);
                }

                while (acquiredPages.Count < needed)
                {
                    if (stack1.Count == 0)
                    {
                        break;
                    }
                    ProcessControlBlock pcb = stack1.Pop();
                    if (pcb.Pages.Count > 0)
                    {
                        int supply = pcb.Pages.Count - 1;
                        if (supply < needed)
                        {
                            acquiredPages.AddRange(pcb.Pages.GetRange(0, supply));
                            pcb.Pages.RemoveRange(0, supply);
                        }
                        else
                        {
                            acquiredPages.AddRange(pcb.Pages.GetRange(0, needed));
                            pcb.Pages.RemoveRange(0, needed);
                        }
                    }
                    stack2.Push(pcb);
                }

                while (stack1.Count > 0)
                {
                    stack2.Push(stack1.Pop());
                }

                readyQueue.Clear();
                while (stack2.Count > 0)
                {
                    readyQueue.Enqueue(stack2.Pop());
                }
            }
            return acquiredPages;
        }
        /**This method handles the waiting operation of a process for resources.
         * */
        private void IO()
        {
            ProcessControlBlock pcb = null;
            bool ioFree = true;
            while (true)
            {
                if (pcb != null && pcb.IsDoneWithDevice)
                {
                    deviceQueue.Dequeue();
                    pcb.State = ProcessControlBlock.READY;
                    readyQueue.Enqueue(pcb);
                    statusDevice = "Resource is now available for Process " + pcb.ID + ".";
                    pcb = null;
                    ioFree = true;
                }

                //io should handle remaining processes in cpu
                if (deviceQueue.Count > 0)
                {
                    if (ioFree)
                    {
                        pcb = (ProcessControlBlock)deviceQueue.Peek();
                        Resource rsc = UseResource();
                        int time = ProcessControlBlock.RandomTime(8);
                        pcb.TimeResource(rsc, time);
                        ioFree = false;
                        statusDevice = "Process " + pcb.ID + " waits for device for " + time + " seconds.";
                    }
                    else
                    {
                        statusDevice = "Process " + pcb.ID + " waits for device for " + pcb.DeviceTime + " seconds.";
                    }
                }
                else
                {
                    statusDevice = "Nothing occupies Device Queue.";
                }
            }
        }
        #endregion

        #region Auxiliary Action Management
        private void AddProcess(ProcessControlBlock pcb)
        {
            lock (processes)
            {
                object[] items = new object[processes.Count];
                processes.CopyTo(items, 0);
                foreach (object o in items)
                {
                    ProcessControlBlock old = (ProcessControlBlock)o;
                    if (old.ID == pcb.ID)
                    {
                        processes.Remove(o);
                        RemoveProcess(pcb);
                        break;
                    }
                }
                processes.Add(pcb);
                processes.Sort();
            }
        }

        private void RemoveProcess(ProcessControlBlock pcb)
        {
            lock (processes)
            {
                object[] items = new object[processes.Count];
                processes.CopyTo(items, 0);
                foreach (object o in items)
                {
                    ProcessControlBlock old = (ProcessControlBlock)o;
                    if (old.ID == pcb.ID)
                    {
                        processes.Remove(o);
                        processes.Sort();
                        //isUpdate = true;
                        break;
                    }
                }
            }
        }

        private Resource UseResource()
        {
            int luck = (int)(new Random().NextDouble() * 100);
            Resource rsc = null;
            switch (luck)
            {
                case 0:
                    rsc = new Resource(0, "Lights Controller");
                    break;
                case 1:
                    rsc = new Resource(1, "Doors Controller");
                    break;
                case 2:
                    rsc = new Resource(2, "Sounds Controller");
                    break;
                default:
                    rsc = null;
                    break;
            }
            return rsc;
        }
        #endregion

        #region  Queue Content Management
        public object[] JobQueue
        {
            get
            {
                lock (this)
                {
                    object[] names = new object[jobQueue.Count];
                    names = jobQueue.ToArray();
                    for (int i = 0; i < names.Length; i++)
                    {
                        ProcessControlBlock pcb = (ProcessControlBlock)names[i];
                        if (pcb != null)
                        {
                            names[i] = "Process " + (pcb.ID > 9 ? "" : "0") + pcb.ID + 
                                (isCompact ? "" : " : " + pcb.Request + " pages");
                        }
                    }
                    return names;
                }
            }
        }

        public object[] ReadyQueue
        {
            get
            {
                lock (this)
                {
                    object[] names = readyQueue.ToArray();
                    for (int i = 0; i < names.Length; i++)
                    {
                        ProcessControlBlock pcb = (ProcessControlBlock)names[i];
                        if (pcb != null)
                        {
                            names[i] = "Process " + (pcb.ID > 9 ? "" : "0") + pcb.ID + " | " + pcb.MemSize + "kb";
                        }
                    }
                    return names;
                }
            }
        }

        public object[] DeviceQueue
        {
            get
            {
                lock (this)
                {
                    object[] names = deviceQueue.ToArray();
                    for (int i = 0; i < names.Length; i++)
                    {
                        ProcessControlBlock pcb = (ProcessControlBlock)names[i];
                        if (pcb != null)
                        {
                            names[i] = "Process " + (pcb.ID > 9 ? "" : "0") + pcb.ID + " | " + pcb.MemSize + "kb";
                        }
                    }
                    return names;
                }
            }
        }
        #endregion

        /**Return messages reflecting the operation successfully 
         * initiated by the scheduler.
         **/
        public String Status(int control)
        {
            switch (control)
            {
                case JOB:
                    return statusJob;
                case READY:
                    return statusReady;
                case RUN:
                    return statusRun;
                case DEVICE:
                    return statusDevice;
                default:
                    return "";
            }
        }

        public bool IsUpdate
        {
            get
            {
                bool temp = isUpdate;
                isUpdate = false;
                return temp;
            }
        }
        public ArrayList Processes
        {
            get
            {
                return processes;
            }
        }

        public Processor Processor
        {
            get
            {
                return processor;
            }
        }
    }
}
