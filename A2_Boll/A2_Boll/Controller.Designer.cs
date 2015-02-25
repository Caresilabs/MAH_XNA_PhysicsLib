namespace A2_Boll
{
    partial class Controller
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
            this.labelNewton = new System.Windows.Forms.Label();
            this.BarSpeed = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.BarSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // labelNewton
            // 
            this.labelNewton.AutoSize = true;
            this.labelNewton.Location = new System.Drawing.Point(59, 58);
            this.labelNewton.Name = "labelNewton";
            this.labelNewton.Size = new System.Drawing.Size(209, 25);
            this.labelNewton.TabIndex = 9;
            this.labelNewton.Text = "Force Magnitude (N)";
            // 
            // BarSpeed
            // 
            this.BarSpeed.Location = new System.Drawing.Point(64, 86);
            this.BarSpeed.Maximum = 1000;
            this.BarSpeed.Name = "BarSpeed";
            this.BarSpeed.Size = new System.Drawing.Size(353, 90);
            this.BarSpeed.TabIndex = 8;
            this.BarSpeed.Value = 300;
            // 
            // Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 214);
            this.Controls.Add(this.labelNewton);
            this.Controls.Add(this.BarSpeed);
            this.Name = "Controller";
            this.Text = "Controller";
            ((System.ComponentModel.ISupportInitialize)(this.BarSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labelNewton;
        public System.Windows.Forms.TrackBar BarSpeed;
    }
}