namespace Screensaver
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            JumpingLabel = new Label();
            timer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // JumpingLabel
            // 
            JumpingLabel.AutoSize = true;
            JumpingLabel.Font = new Font("Segoe UI", 24F);
            JumpingLabel.ForeColor = SystemColors.ControlText;
            JumpingLabel.Location = new Point(346, 191);
            JumpingLabel.Name = "JumpingLabel";
            JumpingLabel.Size = new Size(150, 45);
            JumpingLabel.TabIndex = 0;
            JumpingLabel.Text = "HelloKim";
            JumpingLabel.Click += timer_Tick;
            // 
            // timer
            // 
            timer.Interval = 16;
            timer.Tick += timer_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(JumpingLabel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label JumpingLabel;
        private System.Windows.Forms.Timer timer;
    }
}
