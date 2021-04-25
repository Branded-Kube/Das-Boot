
namespace DataBros
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
            this.textboxActualValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelVariance = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textboxActualValue
            // 
            this.textboxActualValue.Location = new System.Drawing.Point(31, 40);
            this.textboxActualValue.Name = "textboxActualValue";
            this.textboxActualValue.Size = new System.Drawing.Size(274, 20);
            this.textboxActualValue.TabIndex = 0;
            this.textboxActualValue.TextChanged += new System.EventHandler(this.textboxActualValue_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Actual Value";
            // 
            // labelVariance
            // 
            this.labelVariance.AutoSize = true;
            this.labelVariance.Location = new System.Drawing.Point(256, 24);
            this.labelVariance.Name = "labelVariance";
            this.labelVariance.Size = new System.Drawing.Size(49, 13);
            this.labelVariance.TabIndex = 2;
            this.labelVariance.Text = "Variance";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 94);
            this.Controls.Add(this.labelVariance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textboxActualValue);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textboxActualValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelVariance;
    }
}