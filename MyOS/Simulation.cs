using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Data.Sql;

namespace MyOS
{
    public partial class Simulation : Form
    {
        private Thread updateThread, memoryThread, dbThread;
        private Scheduler scheduler;
        private Memory memory;
        private Processor processor;

        public Simulation()
        {
            InitializeComponent();
            InitializeWindow();
            memory = new Memory(1000);
            processor = new Processor(1000, 4);
            scheduler = new Scheduler(memory, processor);
        }

        private void InitializeWindow()
        {
            stopToolStripMenuItem.Enabled = false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            stopToolStripMenuItem_Click(null, null);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //quantum = 4;
            updateThread = new Thread(UpdateGUI);
            memoryThread = new Thread(Repaint);
            dbThread = new Thread(UpdateDatabase);
            updateThread.Start();
            memoryThread.Start();
            dbThread.Start();
            scheduler.Start();
            startToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;
        }

        private void UpdateGUI()
        {
            while (true)
            {
                UpdateStatus();
                UpdateList();
                UpdateProcess();
            }
        }

        private void Repaint()
        {
            while (true)
            {
                Invalidate(new Rectangle(161, 27, 137, 450));
                Thread.Sleep(2000);
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateThread.Abort();
            memoryThread.Abort();
            dbThread.Abort();
            scheduler.Stop();
            startToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = false;
        }

        private void UpdateStatus()
        {
            toolStripStatusLabel1.Text = scheduler.Status(Scheduler.JOB);
            toolStripStatusLabel2.Text = scheduler.Status(Scheduler.READY);
            toolStripStatusLabel3.Text = scheduler.Status(Scheduler.RUN);
            toolStripStatusLabel4.Text = scheduler.Status(Scheduler.DEVICE);

            jqLabel.Text = scheduler.JobQueue.Length + "";
            rqLabel.Text = scheduler.ReadyQueue.Length + "";
            dqLabel.Text = scheduler.DeviceQueue.Length + "";
        }

        private void UpdateList()
        {
            if (scheduler.JobQueue.Length != jqList.Items.Count)
            {
                jqList.Items.Clear();
                jqList.Items.AddRange(scheduler.JobQueue);
            }
            if (scheduler.ReadyQueue.Length != rqList.Items.Count)
            {
                rqList.Items.Clear();
                rqList.Items.AddRange(scheduler.ReadyQueue);
            }
            if (scheduler.DeviceQueue.Length != dqList.Items.Count)
            {
                dqList.Items.Clear();
                dqList.Items.AddRange(scheduler.DeviceQueue);
            }
        }

        public void UpdateProcess()
        {
            ProcessControlBlock activePCB = scheduler.Processor.ActivePCB;
            if (activePCB != null)
            {
                processText.Text = "Process " + activePCB.ID;
                baseText.Text = activePCB.BaseNum + "";
                limitText.Text = activePCB.Limit + "";
                memoryText.Text = activePCB.MemSize + "";
                cpuText.Text = (activePCB.CPUTime > 0 ? activePCB.CPUTime : 0) + "";
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Font font = new Font("Arial",10);
            SolidBrush brush = new SolidBrush(Color.FromArgb(236, 233, 216));
            Pen pen = new Pen(SystemColors.Highlight);
            e.Graphics.FillRectangle(brush, 161, 32, 137, 379);
            e.Graphics.DrawRectangle(pen, 161, 32, 137, 379);
            brush.Color = pen.Color;
            e.Graphics.DrawString("Memory", font, brush, new PointF(166, 32));
            //e.Graphics.FillEllipse(new SolidBrush(Color.Black), 162, 405, 5, 5);
            
            object[] items = scheduler.Processes.ToArray();
            int height = 0;
            int x = 163;
            int y = 408;
            int temp = 0;

            foreach (object o in items)
            {
                ProcessControlBlock pcb = (ProcessControlBlock)o;
                if (pcb.State != ProcessControlBlock.NEW)
                {
                    height = pcb.MemSize * 360 / memory.Capacity;
                    temp = (pcb.BaseNum) * 360 / memory.Capacity;
                    //y = temp;
                    //y = y - temp - height;
                    brush.Color = pcb.BlockColor;
                    pen.Color = Color.FromArgb((brush.Color.R + 10)%256, (brush.Color.G + 10)%256, (brush.Color.B + 10)%256);
                    e.Graphics.FillRectangle(brush, x+2, y-temp-height+2, 131, height-2);
                    e.Graphics.DrawRectangle(pen, x, y - temp - height, 133, height);
                    brush.Color = pcb.TextColor;
                    e.Graphics.DrawString("Process " + pcb.ID + " | " + pcb.MemSize + " kb.", font, brush, x + 3, y - temp - height + 5);
                }
            }
        }

        private void UpdateDatabase()
        {
            while (true)
            {
                object[] items = scheduler.Processes.ToArray();
                dataGridView1.Rows.Clear();
                foreach (object o in items)
                {
                    ProcessControlBlock pcb = (ProcessControlBlock)o;
                    if(pcb != null)
                    {
                        PropagateDB(pcb);
                    }
                }
                Thread.Sleep(1000);
            }
        }

        private void PropagateDB(ProcessControlBlock pcb)
        {
            string state = "";
            switch (pcb.State)
            {
                case ProcessControlBlock.READY:
                    state = "Ready";
                    dataGridView1.Rows.Add(new object[] { pcb.ID, pcb.BaseNum + "", pcb.Limit + "", pcb.MemSize + "", state, pcb.CPUTime + "" });
                    break;
                case ProcessControlBlock.RUNNING:
                    state = "Running";
                    dataGridView1.Rows.Add(new object[] { pcb.ID, pcb.BaseNum + "", pcb.Limit + "", pcb.MemSize + "", state, pcb.CPUTime + "" });
                    break;
                case ProcessControlBlock.WAITING:
                    state = "Waiting";
                    dataGridView1.Rows.Add(new object[] { pcb.ID, pcb.BaseNum + "", pcb.Limit + "", pcb.MemSize + "", state, pcb.CPUTime + "" });
                    break;
                case ProcessControlBlock.TERMINATING:
                    state = "Terminating";
                    dataGridView1.Rows.Add(new object[] { pcb.ID, pcb.BaseNum + "", pcb.Limit + "", pcb.MemSize + "", state, pcb.CPUTime + "" });
                    break;
                case ProcessControlBlock.INTERRUPTED:
                    state = "Interrupted";
                    dataGridView1.Rows.Add(new object[] { pcb.ID, pcb.BaseNum + "", pcb.Limit + "", pcb.MemSize + "", state, pcb.CPUTime + "" });
                    break;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch(e.ColumnIndex){
                case 0: 
                    ProcessControlBlock.SORT = ProcessControlBlock.IDENT;
                    break;
                case 1:
                    ProcessControlBlock.SORT = ProcessControlBlock.BASE;
                    break;
                case 2:
                    ProcessControlBlock.SORT = ProcessControlBlock.LIMIT;
                    break;
                case 3:
                    ProcessControlBlock.SORT = ProcessControlBlock.SIZE;
                    break;
                case 4:
                    ProcessControlBlock.SORT = ProcessControlBlock.BURST;
                    break;
            }
        }
    }
}