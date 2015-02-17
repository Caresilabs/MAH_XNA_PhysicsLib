namespace C1_Car
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
            this.barRadius = new System.Windows.Forms.TrackBar();
            this.barSpeed = new System.Windows.Forms.TrackBar();
            this.btnSpawn = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.barFriction = new System.Windows.Forms.TrackBar();
            this.txtRadius = new System.Windows.Forms.Label();
            this.txtSpeed = new System.Windows.Forms.Label();
            this.txtFriction = new System.Windows.Forms.Label();
            this.txtMake = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.barRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barFriction)).BeginInit();
            this.SuspendLayout();
            // 
            // barRadius
            // 
            this.barRadius.LargeChange = 10;
            this.barRadius.Location = new System.Drawing.Point(12, 24);
            this.barRadius.Maximum = 20;
            this.barRadius.Minimum = 1;
            this.barRadius.Name = "barRadius";
            this.barRadius.Size = new System.Drawing.Size(167, 45);
            this.barRadius.TabIndex = 0;
            this.barRadius.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.barRadius.Value = 10;
            // 
            // barSpeed
            // 
            this.barSpeed.Location = new System.Drawing.Point(12, 92);
            this.barSpeed.Maximum = 50;
            this.barSpeed.Minimum = 1;
            this.barSpeed.Name = "barSpeed";
            this.barSpeed.Size = new System.Drawing.Size(167, 45);
            this.barSpeed.TabIndex = 1;
            this.barSpeed.TickFrequency = 5;
            this.barSpeed.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.barSpeed.Value = 1;
            // 
            // btnSpawn
            // 
            this.btnSpawn.Location = new System.Drawing.Point(12, 260);
            this.btnSpawn.Name = "btnSpawn";
            this.btnSpawn.Size = new System.Drawing.Size(167, 36);
            this.btnSpawn.TabIndex = 2;
            this.btnSpawn.Text = "Spawn Car";
            this.btnSpawn.UseVisualStyleBackColor = true;
            this.btnSpawn.Click += new System.EventHandler(this.btnSpawn_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(12, 302);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(167, 36);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Remove Cars";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // barFriction
            // 
            this.barFriction.Location = new System.Drawing.Point(12, 169);
            this.barFriction.Maximum = 150;
            this.barFriction.Minimum = 1;
            this.barFriction.Name = "barFriction";
            this.barFriction.Size = new System.Drawing.Size(167, 45);
            this.barFriction.TabIndex = 4;
            this.barFriction.TickFrequency = 15;
            this.barFriction.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.barFriction.Value = 1;
            // 
            // txtRadius
            // 
            this.txtRadius.AutoSize = true;
            this.txtRadius.Location = new System.Drawing.Point(12, 8);
            this.txtRadius.Name = "txtRadius";
            this.txtRadius.Size = new System.Drawing.Size(120, 13);
            this.txtRadius.TabIndex = 5;
            this.txtRadius.Text = "Radius of curve (m) [10]";
            // 
            // txtSpeed
            // 
            this.txtSpeed.AutoSize = true;
            this.txtSpeed.Location = new System.Drawing.Point(12, 76);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(80, 13);
            this.txtSpeed.TabIndex = 6;
            this.txtSpeed.Text = "Speed (m/s) [0]";
            // 
            // txtFriction
            // 
            this.txtFriction.AutoSize = true;
            this.txtFriction.Location = new System.Drawing.Point(12, 153);
            this.txtFriction.Name = "txtFriction";
            this.txtFriction.Size = new System.Drawing.Size(118, 13);
            this.txtFriction.TabIndex = 7;
            this.txtFriction.Text = "Coefficient of friction [0]";
            // 
            // txtMake
            // 
            this.txtMake.AutoSize = true;
            this.txtMake.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMake.Location = new System.Drawing.Point(21, 233);
            this.txtMake.Name = "txtMake";
            this.txtMake.Size = new System.Drawing.Size(144, 24);
            this.txtMake.TabIndex = 8;
            this.txtMake.Text = "WILL MAKE IT";
            this.txtMake.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(186, 341);
            this.ControlBox = false;
            this.Controls.Add(this.txtMake);
            this.Controls.Add(this.txtFriction);
            this.Controls.Add(this.txtSpeed);
            this.Controls.Add(this.txtRadius);
            this.Controls.Add(this.barFriction);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSpawn);
            this.Controls.Add(this.barSpeed);
            this.Controls.Add(this.barRadius);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Controller";
            this.Text = "Controller";
            this.Load += new System.EventHandler(this.Controller_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barFriction)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar barRadius;
        private System.Windows.Forms.TrackBar barSpeed;
        private System.Windows.Forms.Button btnSpawn;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TrackBar barFriction;
        public System.Windows.Forms.Label txtRadius;
        public System.Windows.Forms.Label txtSpeed;
        public System.Windows.Forms.Label txtFriction;
        public System.Windows.Forms.Label txtMake;
    }
}