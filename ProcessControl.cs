using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MyOS
{
    class ProcessControl
    {
        public const int JOB = 0;
        public const int READY = 1;
        public const int PROCESS = 2;

        private Thread createThread, readyThread;
        private Thread runThread, ioThread;
        private Queue jobQueue, readyQueue;
        private String statusJob, statusReady, statusProcess;
        private int counter;
        private Memory memory;
        private Processor processor;

        public ProcessControl()
        {
            jobQueue = new Queue(10);
            readyQueue = new Queue(10);
            memory = new Memory(1000);
            processor = new Processor(1000);

            createThread = new Thread(Create);
            readyThread = new Thread(Ready);
            runThread = new Thread(Run);
            ioThread = new Thread(IO);
        }

        public void Start()
        {
            createThread.Start();
            readyThread.Start();
            runThread.Start();
        }

        public void Stop()
        {
            createThread.Abort();
            readyThread.Abort();
            runThread.Abort();
        }

        private void Create()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (jobQueue.Count < 10)
                {
                    OSProcess osp = new OSProcess(++counter);
                    jobQueue.Enqueue(osp);

                    statusJob = "Created new Process " + osp.ID + ".";
                    //MessageBox.Show(osp.ID + "");
                }
                else
                {
                    //Thread.Sleep(1000);
                }
            }
        }

        private void Ready()
        {
            while (true)
            {
                Thread.Sleep(800);
                if (jobQueue.Count > 0 && readyQueue.Count < 10)
                {
                    OSProcess osp = (OSProcess)(jobQueue.Peek());
                    osp.BaseNum = memory.Fill(osp.ID, osp.MemSize);

                    //MessageBox.Show(osp.ID + " s:" + osp.MemSize + " b:" + osp.BaseNum);
                    statusReady = "Placed Process " + osp.ID + " in Ready Queue.";

                    if (osp.BaseNum != Memory.EMPTY)
                    {
                        jobQueue.Dequeue();
                        readyQueue.Enqueue(osp);
                    }
                }
                else
                {
                    //Thread.Sleep(1000);
                }
            }
        }

        private void Run()
        {
            while (true)
            {
                Thread.Sleep(1000);
                OSProcess osp = null;

                if (readyQueue.Count > 0 && processor.IsFree)
                {
                    osp = (OSProcess)(readyQueue.Dequeue());
                    memory.Free(osp.BaseNum, osp.MemSize);
                    processor.SetActiveOSP(osp);

                    statusProcess = "Running " + osp.ID + " for " + osp.CPUTime + "ms.";
                    //MessageBox.Show("Running " + osp.ID + " for " + osp.CPUTime + "ms.");
                    //MessageBox.Show("PQ: " + jobQueue.Count + " RQ: " + readyQueue.Count + " osp: " + osp.ID);
                }
                else
                {
                    //Thread.Sleep(1000);
                }
            }
        }

        private void IO()
        {

        }

        public String Status(int control)
        {
            switch(control)
            {
                case JOB:  
                    return statusJob;
                case READY:   
                    return statusReady;
                default: 
                    return statusProcess;
            }
        }

        public int JobQueueCount
        {
            get
            {
                return jobQueue.Count;
            }
        }

        public int ReadyQueueCount
        {
            get
            {
                return readyQueue.Count;
            }
        }
    }
}
