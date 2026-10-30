namespace BT4
{
    partial class Form1
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
        private System.Windows.Forms.ListBox lstMenu;
        private System.Windows.Forms.ListBox lstSelected;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblTotal;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.lstMenu = new System.Windows.Forms.ListBox();
            this.lstSelected = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstMenu
            // 
            this.lstMenu.FormattingEnabled = true;
            this.lstMenu.ItemHeight = 15;
            this.lstMenu.Location = new System.Drawing.Point(12, 12);
            this.lstMenu.Name = "lstMenu";
            this.lstMenu.Size = new System.Drawing.Size(200, 259);
            this.lstMenu.TabIndex = 0;
            // 
            // lstSelected
            // 
            this.lstSelected.FormattingEnabled = true;
            this.lstSelected.ItemHeight = 15;
            this.lstSelected.Location = new System.Drawing.Point(388, 12);
            this.lstSelected.Name = "lstSelected";
            this.lstSelected.Size = new System.Drawing.Size(200, 259);
            this.lstSelected.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(237, 90);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = ">";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(237, 140);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 30);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "<";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(12, 290);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(91, 15);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Tổng tiền: 0k";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 330);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstSelected);
            this.Controls.Add(this.lstMenu);
            this.Name = "Form1";
            this.Text = "Quản lý món ăn";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
