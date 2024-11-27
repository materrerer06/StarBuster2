namespace StarBuster
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
            MainTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // MainTimer
            // 
            MainTimer.Enabled = true;
            MainTimer.Interval = 30;
            MainTimer.Tick += MainTimer_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(795, 425);
            DoubleBuffered = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            Paint += Form1_Paint;
            KeyDown += MainForm_KeyDown;
            KeyUp += MainForm_KeyUp;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer MainTimer;
    }
}