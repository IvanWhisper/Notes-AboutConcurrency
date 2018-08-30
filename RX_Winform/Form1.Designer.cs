namespace RX_Winform
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
            this.btnFirstEventMode = new System.Windows.Forms.Button();
            this.btnFirstReactiveMode = new System.Windows.Forms.Button();
            this.btnIncreasement = new System.Windows.Forms.Button();
            this.btnDecrement = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnFirstEventMode
            // 
            this.btnFirstEventMode.Location = new System.Drawing.Point(294, 107);
            this.btnFirstEventMode.Name = "btnFirstEventMode";
            this.btnFirstEventMode.Size = new System.Drawing.Size(75, 23);
            this.btnFirstEventMode.TabIndex = 0;
            this.btnFirstEventMode.Text = "Normal";
            this.btnFirstEventMode.UseVisualStyleBackColor = true;
            // 
            // btnFirstReactiveMode
            // 
            this.btnFirstReactiveMode.Location = new System.Drawing.Point(294, 146);
            this.btnFirstReactiveMode.Name = "btnFirstReactiveMode";
            this.btnFirstReactiveMode.Size = new System.Drawing.Size(75, 23);
            this.btnFirstReactiveMode.TabIndex = 1;
            this.btnFirstReactiveMode.Text = "Reactive";
            this.btnFirstReactiveMode.UseVisualStyleBackColor = true;
            // 
            // btnIncreasement
            // 
            this.btnIncreasement.Location = new System.Drawing.Point(293, 250);
            this.btnIncreasement.Name = "btnIncreasement";
            this.btnIncreasement.Size = new System.Drawing.Size(75, 23);
            this.btnIncreasement.TabIndex = 2;
            this.btnIncreasement.Text = "+";
            this.btnIncreasement.UseVisualStyleBackColor = true;
            // 
            // btnDecrement
            // 
            this.btnDecrement.Location = new System.Drawing.Point(293, 280);
            this.btnDecrement.Name = "btnDecrement";
            this.btnDecrement.Size = new System.Drawing.Size(75, 23);
            this.btnDecrement.TabIndex = 3;
            this.btnDecrement.Text = "-";
            this.btnDecrement.UseVisualStyleBackColor = true;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(290, 212);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(35, 13);
            this.lblResult.TabIndex = 4;
            this.lblResult.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 388);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnDecrement);
            this.Controls.Add(this.btnIncreasement);
            this.Controls.Add(this.btnFirstReactiveMode);
            this.Controls.Add(this.btnFirstEventMode);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFirstEventMode;
        private System.Windows.Forms.Button btnFirstReactiveMode;
        private System.Windows.Forms.Button btnIncreasement;
        private System.Windows.Forms.Button btnDecrement;
        private System.Windows.Forms.Label lblResult;
    }
}

