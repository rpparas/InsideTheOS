using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MyOS
{
    class OSProcess
    {
        const int NEW = 0;
        const int BLOCKED = 1;
        const int READY = 2;
        const int RUNNING = 3;
        const int TERMINATED = 4;
        
        private int id, memSize, cpuTime, baseNum;
        private int limit, state;
        private Color color;

        public OSProcess(int id)
        {
            ID = id;
            init();
        }

        public void init()
        {
            Random random = new Random();
            MemSize = (int)(random.NextDouble() * 400) + 100;
            CPUTime = (int)(random.NextDouble() * 6000) + 1000;
            Limit = (int)(random.NextDouble() * 400000) + 100000;

            int red = (int)(random.NextDouble() * 255);
            int green = (int)(random.NextDouble() * 255);
            int blue = (int)(random.NextDouble() * 255);
            color = Color.FromArgb(red, green, blue);
        }

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

        public Color Color
        {
            get
            {
                return color;
            }
        }
    }
}
