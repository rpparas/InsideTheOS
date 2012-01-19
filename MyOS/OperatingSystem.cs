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
    public partial class OperatingSystem : Form
    {
        private Thread updateThread, memoryThread, dbThread;
        private Scheduler scheduler;
        private Memory memory;
        private Processor processor;
        private bool isStop;

        public OperatingSystem()
        {
            InitializeComponent();
            Initialize();
            ResourceList.Initialize();
        }

        /**Enables or disables appropriate menu selections to facilitate
         * intuitive user interaction.
         **/
        private void Initialize()
        {
            int os = (int)(new Random().NextDouble() * 130) + 120;
            memory = new Memory(1000, os);
            processor = new Processor(1000, 4);
            scheduler = new Scheduler(memory, processor);

            stopToolStripMenuItem.Enabled = false;
            pauseToolStripMenuItem.Enabled = false;
            quantumText.Text = processor.Quantum + "";
            freeLabel.Text = memory.FreeMemory + " kb";

            updateThread = new Thread(UpdateGUI);
            memoryThread = new Thread(Repaint);
            dbThread = new Thread(UpdateDatabase);
        }

        #region Menustrip Controller
        /**This method performs the actions associated after clicking the 'START' option.*
         * */
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool compact = compactionToolStripMenuItem.Checked;
            bool regular = regularPagingToolStripMenuItem.Checked;
            bool demand = demandPagingToolStripMenuItem.Checked;
            try
            {
                processor.Quantum = int.Parse(quantumText.Text);
                updateThread.Start();
                memoryThread.Start();
                dbThread.Start();
                /*Notifies the scheduler, processor and memory of the memory allocation scheme to use.*/
                scheduler.Start(compact, regular, demand);
                startToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;
                pauseToolStripMenuItem.Enabled = false;
                settingsToolStripMenuItem.Enabled = false;
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please supply a valid numeric value.", "Quantum Number");
            }
        }

        /**This method performs the actions associated after clicking the 'STOP' option.*
         * */
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                updateThread.Abort();
                memoryThread.Abort();
                dbThread.Abort();
                scheduler.Stop();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                isStop = true;
                startToolStripMenuItem.Enabled = true;
                pauseToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
                settingsToolStripMenuItem.Enabled = true;
            }
            /*OnFormClosing(null);*/
        }

        /**This method performs the actions associated after clicking the 'PAUSE' option.*
         * */
        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pauseToolStripMenuItem.Text == "Pause")
            {
                scheduler.Pause(true);
                memoryThread.Suspend();
                updateThread.Suspend();
                dbThread.Suspend();
                pauseToolStripMenuItem.Text = "Resume";
            }
            else
            {
                scheduler.Pause(false);
                memoryThread.Resume();
                updateThread.Resume();
                dbThread.Resume();
                pauseToolStripMenuItem.Text = "Pause";
            }
        }

        /**This method performs the actions associated after clicking the 'COMPACTION' option.*
         * */
        private void compactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            compactionToolStripMenuItem.Checked = true;
            regularPagingToolStripMenuItem.Checked = false;
            demandPagingToolStripMenuItem.Checked = false;
            compactionGridView.Visible = true;
            pagingGridView.Visible = false;
        }

        /**This method performs the actions associated after clicking 
         * the 'REGULAR PAGING' option.*
         * */
        private void pagingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                compactionToolStripMenuItem.Checked = false;
                regularPagingToolStripMenuItem.Checked = true;
                demandPagingToolStripMenuItem.Checked = false;
                compactionGridView.Visible = false;
                pagingGridView.Visible = true;
                label14.Text = compactionToolStripMenuItem.Checked ? "Free Memory" : "Free Pages";
                memory.InitPages();     /** partitions the memory into pages */
                freeLabel.Text = memory.FreePages + "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /**This method performs the actions associated after clicking 
         * the 'DEMAND PAGING' option.*
         * */
        private void demandPagingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            compactionToolStripMenuItem.Checked = false;
            regularPagingToolStripMenuItem.Checked = false;
            demandPagingToolStripMenuItem.Checked = true;
            compactionGridView.Visible = false;
            pagingGridView.Visible = true;
            memory.InitPages();
            label14.Text = compactionToolStripMenuItem.Checked ? "Free Memory:" : "Free Pages:";
            memory.InitPages();     /** partitions the memory into pages */
            freeLabel.Text = memory.FreePages + "";
        }
        #endregion

        #region Thread Management
        /** Updates the Listboxes, status messages and PCB labels on screen.
         * */
        private void UpdateGUI()
        {
            while (true)
            {
                if (isStop)
                {
                    break;
                }
                UpdateStatus();
                UpdateList();
                UpdateProcess();
            }
        }

        /**Causes the Form to repaint the Memory to reflection the changes 
         * in the memory allocation
         * */
        private void Repaint()
        {
            while (true)
            {
                if (isStop)
                {
                    break;
                }
                if (scheduler.IsUpdate)
                {
                    Invalidate(new Rectangle(161, 27, 137, 450));
                }
                Thread.Sleep(1000);
            }
        }

        private void UpdateStatus()
        {
            try
            {
                toolStripStatusLabel1.Text = scheduler.Status(Scheduler.JOB);
                toolStripStatusLabel2.Text = scheduler.Status(Scheduler.READY);
                toolStripStatusLabel3.Text = scheduler.Status(Scheduler.RUN);
                toolStripStatusLabel4.Text = scheduler.Status(Scheduler.DEVICE);

                if (scheduler != null)
                {
                    jqLabel.Text = scheduler.JobQueue.Length + "";
                    rqLabel.Text = scheduler.ReadyQueue.Length + "";
                    dqLabel.Text = scheduler.DeviceQueue.Length + "";
                }
                lock (memory)
                {
                    freeLabel.Text = compactionToolStripMenuItem.Checked ?
                        memory.FreeMemory + " kb" : memory.FreePages + "";
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("status\n" + ex.Message+"\n"+ex.StackTrace);
            }
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

        /** This method displays the currently running process.
         * */
        public void UpdateProcess()
        {
            lock (scheduler)
            {
                ProcessControlBlock activePCB = scheduler.Processor.ActivePCB;
                if (activePCB != null)
                {
                    if (processText.Text != "Process " + activePCB.ID)
                    {
                        resourcesList.Items.Clear();
                        resourcesList.Items.AddRange(activePCB.Resources);

                        processText.Text = "Process " + activePCB.ID;
                        baseText.Text = activePCB.BaseNum + "";
                        limitText.Text = activePCB.Limit + "";
                        memoryText.Text = activePCB.MemSize + "";
                    }
                    baseText.Enabled = compactionToolStripMenuItem.Checked;
                    limitText.Enabled = compactionToolStripMenuItem.Checked;
                    cpuText.Text = (activePCB.CPUTime > 0 ? activePCB.CPUTime : 0) + "";
                }
            }
        }

        /**Displays the currently ready, running and waiting processes
         * */
        private void UpdateDatabase()
        {
            while (true)
            {
                //break;
                if (isStop)
                {
                    break;
                }
                try
                {
                    ProcessControlBlock[] items = (ProcessControlBlock[])scheduler.Processes.ToArray(typeof(ProcessControlBlock));
                    compactionGridView.Rows.Clear();
                    pagingGridView.Rows.Clear();
                    foreach (ProcessControlBlock pcb in items)
                    {
                        if (pcb != null)
                        {
                            PropagateDB(pcb);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("update database");
                }
                Thread.Sleep(1000);
            }
        }

        /**This method fills the DataGridView with PCB information.
         * */
        private void PropagateDB(ProcessControlBlock pcb)
        {
            string state = "";
            object[] values = null;
            DataGridViewRow row = new DataGridViewRow();
            row.HeaderCell.Value = pcb.ID + "";

            switch (pcb.State)
            {
                case ProcessControlBlock.READY:
                    state = "Ready";
                    break;
                case ProcessControlBlock.RUNNING:
                    state = "Running";
                    break;
                case ProcessControlBlock.WAITING:
                    state = "Waiting";
                    break;
                case ProcessControlBlock.TERMINATING:
                    state = "Terminating";
                    break;
                case ProcessControlBlock.INTERRUPTED:
                    state = "Interrupted";
                    break;
                default: return;
            }
            if (compactionToolStripMenuItem.Checked)
            {
                lock (compactionGridView)
                {
                    if (compactionGridView.ColumnCount == 5)
                    {
                        values = new object[] { pcb.BaseNum, pcb.Limit, pcb.MemSize, state, pcb.State == ProcessControlBlock.TERMINATING ? 0 : pcb.CPUTime };
                        row.CreateCells(compactionGridView, values);
                        compactionGridView.Rows.Add(row);
                    }
                }
            }
            else
            {
                lock (pagingGridView)
                {
                    if (pagingGridView.ColumnCount == 5)
                    {
                        values = new object[] { pcb.MemSize, pcb.Pages.Count, pcb.Request - pcb.Pages.Count, state, pcb.State == ProcessControlBlock.TERMINATING ? 0 : pcb.CPUTime };
                        row.CreateCells(pagingGridView, values);
                        pagingGridView.Rows.Add(row);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
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

        #endregion

        #region Graphics Management
        /**This method plots how every PCB's data is managed by the memory
         * depending on the allocation algorithm utilized.
         * */
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Font font = new Font("Arial",10);
            SolidBrush brush = new SolidBrush(Color.White);
            Pen pen = new Pen(SystemColors.Highlight);

            e.Graphics.FillRectangle(brush, 161, 32, 137, 389);
            e.Graphics.DrawRectangle(pen, 161, 32, 137, 389);
            brush.Color = pen.Color;
            e.Graphics.DrawString("Memory", font, brush, new PointF(166, 31));
            
            int height = memory.OSBlockSize * 360 / memory.Capacity;
            int x = 163;
            int y = 418;
            int temp = 0;

            brush.Color = Color.IndianRed;
            pen.Color = Color.HotPink;
            e.Graphics.FillRectangle(brush, x + 2, y - temp - height + 2, 131, height - 2);
            e.Graphics.DrawRectangle(pen, x, y - temp - height, 133, height);
            brush.Color = Color.Yellow;
            e.Graphics.DrawString("Operating System", font, brush, x + 15, y - temp - height + 5);
            e.Graphics.DrawString(memory.OSBlockSize + " kilobytes", font, brush, x + 30, y - temp - height + 25);

            object[] items = scheduler.Processes.ToArray();
            foreach (object o in items)
            {
                ProcessControlBlock pcb = (ProcessControlBlock)o;
                if (pcb.State != ProcessControlBlock.NEW)
                {
                    if (compactionToolStripMenuItem.Checked)
                    {
                        height = pcb.MemSize * 360 / memory.Capacity;
                        temp = pcb.BaseNum * 360 / memory.Capacity;
                        brush.Color = pcb.BlockColor;
                        pen.Color = Color.FromArgb((brush.Color.R + 10) % 256, (brush.Color.G + 10) % 256, (brush.Color.B + 10) % 256);
                        e.Graphics.FillRectangle(brush, x + 2, y - temp - height + 2, 131, height - 2);
                        e.Graphics.DrawRectangle(pen, x, y - temp - height, 133, height);
                        brush.Color = pcb.TextColor;
                        e.Graphics.DrawString("Process " + (pcb.ID > 9 ? "" : "0") + pcb.ID + " | " + pcb.MemSize + " kb.", font, brush, x + 5, y - temp - height + 2);
                    }
                    else
                    {
                        int textHeight = Page.BLOCK_SIZE * 360 / memory.Capacity;
                        Page[] pages = (Page[])pcb.Pages.ToArray(typeof(Page));
                        for (int i = 0; i < pages.Length; i++)
                        {
                            height = pages[i].Occupied * 360 / memory.Capacity;
                            temp = pages[i].BaseNum * 360 / memory.Capacity;
                            brush.Color = pcb.BlockColor;
                            pen.Color = Color.FromArgb((brush.Color.R + 10) % 256, (brush.Color.G + 10) % 256, (brush.Color.B + 10) % 256);
                            e.Graphics.FillRectangle(brush, x + 2, y - temp - height + 2, 131, height-2);
                            height = Page.BLOCK_SIZE * 360 / memory.Capacity;
                            e.Graphics.DrawRectangle(pen, x, y - temp - height, 133, height);
                            brush.Color = pcb.TextColor;
                            e.Graphics.DrawString("Process " + (pcb.ID > 9 ? "" : "0") + pcb.ID, font, brush, x + 35, y - temp - textHeight + 2);
                        }
                    }
                }
            }
        }
        #endregion

        /**Compels garbage collection at the end of execution.
         **/
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            this.Dispose();
            GC.Collect();
            Environment.Exit(0);
            MessageBox.Show("AH!!!!");
        }
    }
}