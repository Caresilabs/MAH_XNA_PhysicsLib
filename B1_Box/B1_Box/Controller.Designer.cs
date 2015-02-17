namespace B1_Box
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
            this.BarDegrees = new System.Windows.Forms.TrackBar();
            this.labelDegree = new System.Windows.Forms.Label();
            this.labelStatic = new System.Windows.Forms.Label();
            this.BarStatic = new System.Windows.Forms.TrackBar();
            this.labelKinetic = new System.Windows.Forms.Label();
            this.BarKinetic = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.BarDegrees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarStatic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarKinetic)).BeginInit();
            this.SuspendLayout();
            // 
            // BarDegrees
            // 
            this.BarDegrees.Location = new System.Drawing.Point(51, 71);
            this.BarDegrees.Maximum = 120;
            this.BarDegrees.Minimum = -40;
            this.BarDegrees.Name = "BarDegrees";
            this.BarDegrees.Size = new System.Drawing.Size(353, 90);
            this.BarDegrees.TabIndex = 0;
            this.BarDegrees.Value = 10;
            // 
            // labelDegree
            // 
            this.labelDegree.AutoSize = true;
            this.labelDegree.Location = new System.Drawing.Point(46, 43);
            this.labelDegree.Name = "labelDegree";
            this.labelDegree.Size = new System.Drawing.Size(249, 25);
            this.labelDegree.TabIndex = 1;
            this.labelDegree.Text = "Change Angle (Degrees)";
            // 
            // labelStatic
            // 
            this.labelStatic.AutoSize = true;
            this.labelStatic.Location = new System.Drawing.Point(46, 182);
            this.labelStatic.Name = "labelStatic";
            this.labelStatic.Size = new System.Drawing.Size(224, 25);
            this.labelStatic.TabIndex = 3;
            this.labelStatic.Text = "Static Friction (0 - 1.2)";
            // 
            // BarStatic
            // 
            this.BarStatic.Location = new System.Drawing.Point(51, 210);
            this.BarStatic.Maximum = 120;
            this.BarStatic.Name = "BarStatic";
            this.BarStatic.Size = new System.Drawing.Size(353, 90);
            this.BarStatic.TabIndex = 2;
            this.BarStatic.Value = 100;
            this.BarStatic.Scroll += new System.EventHandler(this.BarStatic_Scroll);
            // 
            // labelKinetic
            // 
            this.labelKinetic.AutoSize = true;
            this.labelKinetic.Location = new System.Drawing.Point(46, 312);
            this.labelKinetic.Name = "labelKinetic";
            this.labelKinetic.Size = new System.Drawing.Size(235, 25);
            this.labelKinetic.TabIndex = 5;
            this.labelKinetic.Text = "Kinetic Friction (0 - 1.2)";
            // 
            // BarKinetic
            // 
            this.BarKinetic.Location = new System.Drawing.Point(51, 340);
            this.BarKinetic.Maximum = 120;
            this.BarKinetic.Name = "BarKinetic";
            this.BarKinetic.Size = new System.Drawing.Size(353, 90);
            this.BarKinetic.TabIndex = 4;
            this.BarKinetic.Value = 30;
            this.BarKinetic.Scroll += new System.EventHandler(this.BarKinetic_Scroll);
            // 
            // Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 467);
            this.Controls.Add(this.labelKinetic);
            this.Controls.Add(this.BarKinetic);
            this.Controls.Add(this.labelStatic);
            this.Controls.Add(this.BarStatic);
            this.Controls.Add(this.labelDegree);
            this.Controls.Add(this.BarDegrees);
            this.Name = "Controller";
            this.Text = "Controller";
            ((System.ComponentModel.ISupportInitialize)(this.BarDegrees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarStatic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarKinetic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TrackBar BarDegrees;
        public System.Windows.Forms.TrackBar BarStatic;
        public System.Windows.Forms.TrackBar BarKinetic;
        public System.Windows.Forms.Label labelDegree;
        public System.Windows.Forms.Label labelStatic;
        public System.Windows.Forms.Label labelKinetic;
    }
}