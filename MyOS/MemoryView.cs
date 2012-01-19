using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyOS
{
    public partial class MemoryView : Form
    {
        public MemoryView()
        {
            InitializeComponent();
        }

        public void Display(Memory memory)
        {
            for (int i = 0; i < memory.Capacity; i++)
            {
                if (i != 0 && i % 50 == 0)
                {
                    richTextBox1.Text += "\n";
                }
                
                richTextBox1.Text += memory.GetOccupant(i);
            }
        }
    }
}