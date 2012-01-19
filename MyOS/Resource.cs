using System;
using System.Collections;
using System.Text;

namespace MyOS
{
    class Resource
    {
        private int id;
        private string name;
        private bool isFree;
        
        private System.Threading.Timer timer;
        private Queue processes;
        
        public Resource(int id, String name)
        {
            ID = id;
            Name = name;
            processes = new Queue();
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

        public string Name
        {
            set
            {
                name = value;
            }
            get
            {
                return name;
            }
        }

        public void EnqueueProcess(ProcessControlBlock pcb)
        {
            processes.Enqueue(pcb);
        }

        public int WaitingProcesses
        {
            get
            {
                return processes.Count - 1;
            }
        }

        public ProcessControlBlock ActivePCB
        {
            get
            {
                return (ProcessControlBlock)processes.Peek();
            }
        }
    }
}
