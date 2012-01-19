namespace MyOS
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.powerButton = new System.Windows.Forms.Button();
            this.cableRadio = new System.Windows.Forms.RadioButton();
            this.dvdRadio = new System.Windows.Forms.RadioButton();
            this.hdRadio = new System.Windows.Forms.RadioButton();
            this.chNumeric = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.volProgress = new System.Windows.Forms.ProgressBar();
            this.tvRadio = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.webRadio = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // powerButton
            // 
            this.powerButton.ForeColor = System.Drawing.Color.Red;
            this.powerButton.Location = new System.Drawing.Point(467, 330);
            this.powerButton.Name = "powerButton";
            this.powerButton.Size = new System.Drawing.Size(52, 20);
            this.powerButton.TabIndex = 3;
            this.powerButton.Text = "Power";
            this.powerButton.UseVisualStyleBackColor = true;
            this.powerButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // cableRadio
            // 
            this.cableRadio.AutoSize = true;
            this.cableRadio.BackColor = System.Drawing.Color.Black;
            this.cableRadio.ForeColor = System.Drawing.Color.Red;
            this.cableRadio.Location = new System.Drawing.Point(0, 332);
            this.cableRadio.Name = "cableRadio";
            this.cableRadio.Size = new System.Drawing.Size(52, 17);
            this.cableRadio.TabIndex = 5;
            this.cableRadio.TabStop = true;
            this.cableRadio.Text = "Cable";
            this.cableRadio.UseVisualStyleBackColor = false;
            // 
            // dvdRadio
            // 
            this.dvdRadio.AutoSize = true;
            this.dvdRadio.BackColor = System.Drawing.Color.Black;
            this.dvdRadio.ForeColor = System.Drawing.Color.Red;
            this.dvdRadio.Location = new System.Drawing.Point(58, 332);
            this.dvdRadio.Name = "dvdRadio";
            this.dvdRadio.Size = new System.Drawing.Size(48, 17);
            this.dvdRadio.TabIndex = 6;
            this.dvdRadio.TabStop = true;
            this.dvdRadio.Text = "DVD";
            this.dvdRadio.UseVisualStyleBackColor = false;
            // 
            // hdRadio
            // 
            this.hdRadio.AutoSize = true;
            this.hdRadio.BackColor = System.Drawing.Color.Black;
            this.hdRadio.ForeColor = System.Drawing.Color.Red;
            this.hdRadio.Location = new System.Drawing.Point(112, 332);
            this.hdRadio.Name = "hdRadio";
            this.hdRadio.Size = new System.Drawing.Size(54, 17);
            this.hdRadio.TabIndex = 7;
            this.hdRadio.TabStop = true;
            this.hdRadio.Text = "HDisk";
            this.hdRadio.UseVisualStyleBackColor = false;
            // 
            // chNumeric
            // 
            this.chNumeric.BackColor = System.Drawing.Color.White;
            this.chNumeric.ForeColor = System.Drawing.Color.Red;
            this.chNumeric.Location = new System.Drawing.Point(421, 329);
            this.chNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.chNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.chNumeric.Name = "chNumeric";
            this.chNumeric.Size = new System.Drawing.Size(42, 20);
            this.chNumeric.TabIndex = 9;
            this.chNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(276, 334);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Vol";
            // 
            // volProgress
            // 
            this.volProgress.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.volProgress.ForeColor = System.Drawing.Color.Red;
            this.volProgress.Location = new System.Drawing.Point(303, 332);
            this.volProgress.Name = "volProgress";
            this.volProgress.Size = new System.Drawing.Size(91, 18);
            this.volProgress.TabIndex = 11;
            this.volProgress.Value = 30;
            // 
            // tvRadio
            // 
            this.tvRadio.AutoSize = true;
            this.tvRadio.BackColor = System.Drawing.Color.Black;
            this.tvRadio.ForeColor = System.Drawing.Color.Red;
            this.tvRadio.Location = new System.Drawing.Point(172, 332);
            this.tvRadio.Name = "tvRadio";
            this.tvRadio.Size = new System.Drawing.Size(39, 17);
            this.tvRadio.TabIndex = 8;
            this.tvRadio.TabStop = true;
            this.tvRadio.Text = "TV";
            this.tvRadio.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(396, 334);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Ch";
            // 
            // webRadio
            // 
            this.webRadio.AutoSize = true;
            this.webRadio.BackColor = System.Drawing.Color.Black;
            this.webRadio.ForeColor = System.Drawing.Color.Red;
            this.webRadio.Location = new System.Drawing.Point(218, 332);
            this.webRadio.Name = "webRadio";
            this.webRadio.Size = new System.Drawing.Size(48, 17);
            this.webRadio.TabIndex = 13;
            this.webRadio.TabStop = true;
            this.webRadio.Text = "Web";
            this.webRadio.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(519, 323);
            this.button2.TabIndex = 14;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(522, 355);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.webRadio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.volProgress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chNumeric);
            this.Controls.Add(this.tvRadio);
            this.Controls.Add(this.hdRadio);
            this.Controls.Add(this.dvdRadio);
            this.Controls.Add(this.cableRadio);
            this.Controls.Add(this.powerButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button powerButton;
        private System.Windows.Forms.RadioButton cableRadio;
        private System.Windows.Forms.RadioButton dvdRadio;
        private System.Windows.Forms.RadioButton hdRadio;
        private System.Windows.Forms.NumericUpDown chNumeric;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar volProgress;
        private System.Windows.Forms.RadioButton tvRadio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton webRadio;
        private System.Windows.Forms.Button button2;
    }
}

