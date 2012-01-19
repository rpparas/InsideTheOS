using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Threading;

namespace MyOS
{
    class ProcessControlBlock : IComparable
    {
        public const int NEW = 0;
        public const int READY = 1;
        public const int RUNNING = 2;
        public const int WAITING = 3;
        public const int TERMINATING = 4;
        public const int INTERRUPTED = 5;

        public const int IDENT = 0;
        public const int BASE = 1;
        public const int LIMIT = 2;
        public const int SIZE = 3;
        public const int BURST = 4;
        public static int SORT;

        private int id, memSize, baseNum;
        private int limit, state;
        private int cpuTime, deviceTime;
        private int request;
        private bool isDone;
        private ArrayList pages;
        private Color blockColor, textColor;

        private ArrayList resources;
        private System.Threading.Timer timer;

        public ProcessControlBlock(int id)
        {
            ID = id;
            Initialize();
        }

        /**Randomizes the values (properties) associated with the PCB
         * */
        public void Initialize()
        {
            Random random = new Random();
            resources = new ArrayList();
            CPUTime = RandomTime(6);
            MemSize = (int)(random.NextDouble() * 180) + 120;
            request = MemSize / Page.BLOCK_SIZE + (MemSize % Page.BLOCK_SIZE == 0 ? 0 : 1);

            int red = (int)(random.NextDouble() * 255);
            int green = (int)(random.NextDouble() * 255);
            int blue = (int)(random.NextDouble() * 255);
            blockColor = Color.FromArgb(red, green, blue);
            textColor = Color.FromArgb((blockColor.R + 128) % 256, (blockColor.G + 128) % 256, (blockColor.B + 128) % 256);
        }

        #region Property Sheets
        public int ID
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
        }

        public int MemSize
        {
            set
            {
                memSize = value;
            }
            get
            {
                return memSize;
            }
        }

        /** CPU Burst Time
         * */
        public int CPUTime
        {
            set
            {
                cpuTime = value;
            }
            get
            {
                return cpuTime;
            }
        }

        /** I/O Waiting Time
         * */
        public int DeviceTime
        {
            set
            {
                deviceTime = value;
            }
            get
            {
                return deviceTime;
            }
        }

        public int BaseNum
        {
            set
            {
                baseNum = value;
            }
            get
            {
                return baseNum;
            }
        }

        public int Limit
        {
            set
            {
                limit = value;
            }
            get
            {
                return limit;
            }
        }

        public int State
        {
            set
            {
                state = value;
            }
            get
            {
                return state;
            }
        }

        public ArrayList Pages
        {
            set
            {
                pages = value;
            }
            get
            {
                return pages;
            }
        }

        public int Request
        {
            get
            {
                return request;
            }
        }

        public Color BlockColor
        {
            get
            {
                return blockColor;
            }
        }

        public Color TextColor
        {
            get
            {
                return textColor;
            }
        }
        #endregion

        #region Time Management
        /**Starts the timer controlling this PCB's waiting time.
         * */
        public void TimeResource(Resource rsc, int deviceTime)
        {
            this.deviceTime = deviceTime;
            TimerCallback tc = new TimerCallback(this.CheckStatus);
            timer = new System.Threading.Timer(tc, null, 1000, 1000);            
        }

        /**Checks the remaining time left and disposes the timer correspondingly.
         * */
        public void CheckStatus(object o)
        {
            --deviceTime;
            if (deviceTime <= 0)
            {
                timer.Dispose();
                isDone = true;
            }
        }

        public bool IsDoneWithDevice
        {
            get
            {
                return isDone;
            }
        }

        public static int RandomTime(int max)
        {
            return (int)(new Random().NextDouble() * max) + 1;
        }
        #endregion

        #region Resource Management
        /**Binds resources (input and output devices) to this PCB.
         * */
        public void AddResources()
        {
            int count = ResourceList.GetRandomNumber();
            for (int i = 0; i < count; i++)
            {
                int index = ResourceList.GetRandomNumber();
                Resource rsc = ResourceList.GetResource(index, true);
                if (rsc != null && !resources.Contains(rsc))
                {
                    resources.Add(rsc);
                }
            }
        }

        public object[] Resources
        {
            get
            {
                object[] names = new object[resources.Count];
                names = resources.ToArray();
                for (int i = 0; i < names.Length; i++)
                {
                    Resource rsc = (Resource)names[i];
                    names[i] = rsc.Name;
                }
                return names;
            }
        }
        #endregion

        /**Represents the PCB in terms of numeric values for comparison with other PCB's.
         * */
        public int CompareTo(object o)
        {
            ProcessControlBlock otherPCB = (ProcessControlBlock)o;
            switch (SORT)
            {
                case IDENT:
                    return (otherPCB.ID < this.ID) ? 1 : -1;
                case BASE:
                    return (otherPCB.BaseNum < this.BaseNum) ? 1 : -1;
                case LIMIT:
                    return (otherPCB.Limit < this.Limit) ? 1 : -1;
                case SIZE:
                    return (otherPCB.MemSize < this.MemSize) ? 1 : -1;
                case BURST:
                    return (otherPCB.CPUTime < this.CPUTime) ? 1 : -1;
            }
            return 0;
        }
       
    }
}
