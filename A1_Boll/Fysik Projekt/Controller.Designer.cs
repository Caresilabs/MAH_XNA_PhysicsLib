namespace Fysik_Projekt
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
            this.labelDegree = new System.Windows.Forms.Label();
            this.BarDegrees = new System.Windows.Forms.TrackBar();
            this.labelNewton = new System.Windows.Forms.Label();
            this.BarSpeed = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.BarDegrees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDegree
            // 
            this.labelDegree.AutoSize = true;
            this.labelDegree.Location = new System.Drawing.Point(35, 48);
            this.labelDegree.Name = "labelDegree";
            this.labelDegree.Size = new System.Drawing.Size(249, 25);
            this.labelDegree.TabIndex = 3;
            this.labelDegree.Text = "Change Angle (Degrees)";
            // 
            // BarDegrees
            // 
            this.BarDegrees.Location = new System.Drawing.Point(40, 76);
            this.BarDegrees.Maximum = 100;
            this.BarDegrees.Name = "BarDegrees";
            this.BarDegrees.Size = new System.Drawing.Size(353, 90);
            this.BarDegrees.TabIndex = 2;
            this.BarDegrees.Value = 45;
            // 
            // labelNewton
            // 
            this.labelNewton.AutoSize = true;
            this.labelNewton.Location = new System.Drawing.Point(35, 157);
            this.labelNewton.Name = "labelNewton";
            this.labelNewton.Size = new System.Drawing.Size(209, 25);
            this.labelNewton.TabIndex = 5;
            this.labelNewton.Text = "Force Magnitude (N)";
            // 
            // BarSpeed
            // 
            this.BarSpeed.Location = new System.Drawing.Point(40, 185);
            this.BarSpeed.Maximum = 1000;
            this.BarSpeed.Name = "BarSpeed";
            this.BarSpeed.Size = new System.Drawing.Size(353, 90);
            this.BarSpeed.TabIndex = 4;
            this.BarSpeed.Value = 300;
            // 
            // Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 300);
            this.Controls.Add(this.labelNewton);
            this.Controls.Add(this.BarSpeed);
            this.Controls.Add(this.labelDegree);
            this.Controls.Add(this.BarDegrees);
            this.Name = "Controller";
            this.Text = "Controller";
            ((System.ComponentModel.ISupportInitialize)(this.BarDegrees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TrackBar BarDegrees;
        public System.Windows.Forms.TrackBar BarSpeed;
        public System.Windows.Forms.Label labelDegree;
        public System.Windows.Forms.Label labelNewton;
    }
}