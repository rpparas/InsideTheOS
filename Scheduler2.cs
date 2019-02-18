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
        private static ArrayList processes;
        private Memory memory;
        private Processor processor;

        private string statusJob = "Waiting for new process.";
        private string statusReady = "Initializing Ready Queue.";
        private string statusRun = "Processor starting up";
        private string statusDevice = "Nothing occupies Device Queue.";
        private int counter;
        private bool isCompact, isDemand;

        public Scheduler(Memory memory, Processor processor)
        {
            this.memory = memory;
            this.processor = processor;
            jobQueue = new Queue(10);
            readyQueue = new Queue(10);
            deviceQueue = new Queue(10);
            processes = new ArrayList();
        }

        #region Thread Management
        /**This method springboards the threads to be run by the scheduler.
         * */
        public void Start(bool isCompact, bool isDemand)
        {
            createThread = new Thread(Create);
            readyThread = new Thread(Ready);
            runThread = new Thread(Run);
            ioThread = new Thread(IO);
            compactThread = new Thread(Compact);

            createThread.Start();
            readyThread.Start();
            runThread.Start();
            ioThread.Start();

            this.isCompact = isCompact;
            this.isDemand = isDemand;
            
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
            compactThread.Abort();
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
                compactThread.Suspend();
            }
            else
            {
                createThread.Resume();
                readyThread.Resume();
                runThread.Resume();
                ioThread.Resume();
                compactThread.Resume();
            }
        }
        #endregion

        #region Process State Management
        /**This method facilitates Job Creation and regulates the Job Queue.
         * */
        private void Create()
        {
            ProcessControlBlock pcb = null;
            while (true)
            {
                if (jobQueue.Count < 3 && readyQueue.Count < 4)
                {
                    pcb = new ProcessControlBlock(++counter);
                    pcb.State = ProcessControlBlock.NEW;
                    AddProcess(pcb);
                    jobQueue.Enqueue(pcb);
                    statusJob = "Created Process " + pcb.ID + ".";
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
                if (jobQueue.Count > 0 && readyQueue.Count < 10)
                {
                    pcb = (ProcessControlBlock)(jobQueue.Peek());
                    bool isProceed = false;
                    lock (memory)
                    {
                        if (pcb.MemSize > memory.FreeMemory)
                        {
                            continue;
                        }

                        /**Performs compacting of the memory */
                        if (isCompact)
                        {
                            pcb.BaseNum = memory.Fill(pcb.ID, pcb.MemSize);
                            if (pcb.BaseNum != Memory.FULL)
                            {
                                memory.UsedMemory += pcb.MemSize;
                                pcb.Limit = pcb.BaseNum + pcb.MemSize - 1;
                                isProceed = true;
                            }
                        }
                        /**Assigns pages to the current PCB */
                        else
                        {
                            pcb.Pages = memory.CreatePages(pcb.MemSize, pcb.Request, false);
                            if (pcb.Pages != null)
                            {
                                memory.UsedMemory += pcb.MemSize;
                                isProceed = true;
                            }
                        }
                        if (isProceed)
                        {
                            pcb.State = ProcessControlBlock.READY;
                            statusReady = "Queing Process " + pcb.ID + " in Ready Queue.";
                            jobQueue.Dequeue();
                            readyQueue.Enqueue(pcb);
                            AddProcess(pcb);
                        }
                    }
                }
            }
        }

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
                        for (int i = 0; i < processes.Count; i++)
                        {
                            ProcessControlBlock pcb = GetProcess(i);
                            if (pcb.State != ProcessControlBlock.NEW)
                            {
                                lock (pcb)
                                {
                                    pcb.BaseNum = curBase;
                                    pcb.Limit = curBase + pcb.MemSize - 1;
                                    curBase = pcb.Limit + 1;
                                    memory.Fill(pcb.ID, pcb.MemSize);
                                    AddProcess(pcb);
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(2000);
            }
        }

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
                        if (isCompact)
                        {
                            memory.Free(pcb.BaseNum, pcb.MemSize);
                        }
                        else
                        {
                            memory.Free(pcb.Pages);
                        }
                        memory.UsedMemory -= pcb.MemSize;
                        processor.FreeCPU(true);
                        RemoveProcess(pcb);
                    }

                    if (readyQueue.Count > 0)
                    {
                        if (isDemand)
                        {
                            pcb = (ProcessControlBlock)(readyQueue.Peek());
                            if (pcb.Pages.Length < pcb.Request)
                            {
                                Page[] pages = SelectVictims(pcb.Request - pcb.Pages.Length);
                                //here
                            }
                        }
                        else
                        {
                            pcb = (ProcessControlBlock)(readyQueue.Dequeue());
                            pcb.AddResources();
                            pcb.State = ProcessControlBlock.RUNNING;
                            AddProcess(pcb);
                            processor.ActivePCB = pcb;
                            processor.Start();
                        }
                    }
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
                }
                else if (pcb.State == ProcessControlBlock.READY)
                {
                    AddProcess(pcb);
                    readyQueue.Enqueue(pcb);
                    processor.FreeCPU(true);
                    statusReady = "Process " + pcb.ID + " was interrupted.";
                }
                else if (pcb.State == ProcessControlBlock.WAITING)
                {
                    AddProcess(pcb);
                    deviceQueue.Enqueue(pcb);
                    processor.FreeCPU(true);
                    statusDevice = "Process " + pcb.ID + " is waiting for a resource.";
                }

                if (pcb != null)
                {
                    statusRun = "Running Process " + pcb.ID + " for " +  pcb.CPUTime + " seconds.";
                }
            }
        }

        private ArrayList SelectVictims(int request)
        {
            ArrayList pages = new ArrayList();

            object[] rq = this.ReadyQueue;
            ProcessControlBlock victim = null;
            lock (readyQueue)
            {
                for (int i = rq.Length - 1, acquiredPages = 0; i > 0 && acquiredPages < request; i--)
                {
                    victim = (ProcessControlBlock)(rq[i]);
                    acquiredPages += victim.Pages.Length;
                    jobQueue.Enqueue(victim);
                    pages.Add(victim.Pages);
                    rq[i] = null;
                }
                readyQueue.Clear();
                for (int i = 0; rq[i] != null; i++)
                {
                    readyQueue.Enqueue(rq[i]);
                }
            }
            return pages;
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
            lock(processes)
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
                        break;
                    }
                }
            }
        }

        public ProcessControlBlock GetProcess(int id)
        {
            object[] items = new object[processes.Count];
            processes.CopyTo(items, 0);
            try
            {
                return (ProcessControlBlock)processes[id];
            }
            catch
            {
                return null;
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
                lock (readyQueue)
                {
                    object[] names = new object[jobQueue.Count];
                    names = jobQueue.ToArray();
                    for (int i = 0; i < names.Length; i++)
                    {
                        ProcessControlBlock pcb = (ProcessControlBlock)names[i];
                        names[i] = "Process " + pcb.ID;
                    }
                    return names;
                }
            }
        }

        public object[] ReadyQueue
        {
            get
            {
                lock (readyQueue)
                {
                    object[] names = readyQueue.ToArray();
                    for (int i = 0; i < names.Length; i++)
                    {
                        ProcessControlBlock pcb = (ProcessControlBlock)names[i];
                        names[i] = "Process " + pcb.ID + " | " + pcb.MemSize + "kb";
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
                        names[i] = "Process " + pcb.ID + " | " + pcb.MemSize + "kb";
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

        /**Returns the processes currently ready, running and waiting.
         * */
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
