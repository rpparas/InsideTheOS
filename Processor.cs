using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MyOS
{
    class Processor
    {
        public const int CONTINUE = 0;
        public const int INTERRUPT = 1;
        public const int WAITING = 2;

        private ProcessControlBlock pcb;
        private System.Threading.Timer timer;
        private Random random;

        private bool isFree;
        private int runTime, speed;
        private int quantum, minBurst, maxBurst;

        /**Forwards the CPU specification */
        public Processor(int speed, int quantum)
        {
            this.speed = speed;
            this.quantum = quantum;
            this.isFree = true;
            random = new Random();
        }

        /**Binds a process to the CPU
         * */
        public ProcessControlBlock ActivePCB
        {
            set
            {
                pcb = value;
            }
            get
            {
                return pcb;
            }
        }

        /**Begins execution of the process with regard to its burst time.
         * */
        public bool Start()
        {
            if (isFree)
            {
                runTime = 0;
                isFree = false;
                TimerCallback tc = new TimerCallback(this.CheckStatus);
                timer = new System.Threading.Timer(tc, null, 1000, 1000);
                return true;
            }
            return false;
        }

        /**Computes the possibility for a process to enter the waiting state.
         * */
        private bool IsWait()
        {
            return pcb.ID > 4 && random.NextDouble() > .75;
        }

        public int IsDemand(int max)
        {
            return max > 0 ? (int)(random.NextDouble() * max) : 0;
        }

        /**Checks the current state, burst time and probability for waiting.
         * */
        public void CheckStatus(object o)
        {
            try
            {
                --pcb.CPUTime;
                runTime++;

                if (pcb.CPUTime > 0)
                {
                    if (runTime >= quantum)
                    {
                        pcb.State = ProcessControlBlock.INTERRUPTED;                        
                        FreeCPU(false);
                    }
                    
                    if (IsWait())
                    {
                        pcb.State = ProcessControlBlock.WAITING;
                        FreeCPU(false);
                    }
                }
                else
                {
                    pcb.State = ProcessControlBlock.TERMINATING;
                    isFree = true;
                }
            }
            catch (NullReferenceException e)
            {
            }
        }

        /**Releases a process's hold on the CPU*/
        public void FreeCPU(bool newProcess)
        {
            timer.Dispose();
            if (newProcess)
            {
                isFree = true;
                pcb = null;
            }
        }

        public int Quantum
        {
            set
            {
                this.quantum = value;
            }
            get
            {
                return quantum;
            }
        }

        public bool IsFree
        {
            get
            {
                return isFree;
            }
        }
    }
}
