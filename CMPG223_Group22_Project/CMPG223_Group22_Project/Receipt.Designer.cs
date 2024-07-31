
namespace CMPG223_Group22_Project
{
    partial class Receipt
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
            this.lbReceipt = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbReceipt
            // 
            this.lbReceipt.FormattingEnabled = true;
            this.lbReceipt.ItemHeight = 16;
            this.lbReceipt.Location = new System.Drawing.Point(26, 34);
            this.lbReceipt.Name = "lbReceipt";
            this.lbReceipt.Size = new System.Drawing.Size(691, 372);
            this.lbReceipt.TabIndex = 0;
            // 
            // Receipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbReceipt);
            this.Name = "Receipt";
            this.Text = "Reseipt";
            this.Load += new System.EventHandler(this.Receipt_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbReceipt;
    }
}