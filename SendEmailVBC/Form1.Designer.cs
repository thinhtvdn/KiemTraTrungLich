namespace SendEmailVBC
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
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtSoLuongNV = new System.Windows.Forms.TextBox();
            this.txtFileDuLieu = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.btExcel = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btDong = new System.Windows.Forms.Button();
            this.oGrid = new System.Windows.Forms.DataGridView();
            this.cmbGV = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.oGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "OpenFileDialog1";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(1028, 22);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(139, 20);
            this.Label6.TabIndex = 50;
            this.Label6.Text = "Số dòng dữ liệu";
            // 
            // txtSoLuongNV
            // 
            this.txtSoLuongNV.Location = new System.Drawing.Point(1184, 22);
            this.txtSoLuongNV.Name = "txtSoLuongNV";
            this.txtSoLuongNV.Size = new System.Drawing.Size(88, 26);
            this.txtSoLuongNV.TabIndex = 49;
            // 
            // txtFileDuLieu
            // 
            this.txtFileDuLieu.Location = new System.Drawing.Point(1184, 54);
            this.txtFileDuLieu.Name = "txtFileDuLieu";
            this.txtFileDuLieu.Size = new System.Drawing.Size(449, 26);
            this.txtFileDuLieu.TabIndex = 52;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.Color.Black;
            this.Label7.Location = new System.Drawing.Point(1034, 56);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(133, 21);
            this.Label7.TabIndex = 51;
            this.Label7.Text = "Tập tin dữ liệu";
            // 
            // btExcel
            // 
            this.btExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExcel.Location = new System.Drawing.Point(16, 8);
            this.btExcel.Name = "btExcel";
            this.btExcel.Size = new System.Drawing.Size(279, 55);
            this.btExcel.TabIndex = 53;
            this.btExcel.Text = "Lấy dữ liệu từ tập tin EXCEL";
            this.btExcel.UseVisualStyleBackColor = true;
            this.btExcel.Click += new System.EventHandler(this.btExcel_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.Location = new System.Drawing.Point(303, 8);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(279, 55);
            this.btnCheck.TabIndex = 54;
            this.btnCheck.Text = "Kiểm tra trùng lịch";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btDong
            // 
            this.btDong.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDong.Location = new System.Drawing.Point(588, 8);
            this.btDong.Name = "btDong";
            this.btDong.Size = new System.Drawing.Size(75, 55);
            this.btDong.TabIndex = 56;
            this.btDong.Text = "Đóng";
            this.btDong.UseVisualStyleBackColor = true;
            this.btDong.Click += new System.EventHandler(this.btDong_Click);
            // 
            // oGrid
            // 
            this.oGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.oGrid.Location = new System.Drawing.Point(0, 256);
            this.oGrid.Name = "oGrid";
            this.oGrid.RowHeadersWidth = 62;
            this.oGrid.Size = new System.Drawing.Size(1633, 563);
            this.oGrid.TabIndex = 42;
            // 
            // cmbGV
            // 
            this.cmbGV.FormattingEnabled = true;
            this.cmbGV.Location = new System.Drawing.Point(158, 82);
            this.cmbGV.Name = "cmbGV";
            this.cmbGV.Size = new System.Drawing.Size(312, 28);
            this.cmbGV.TabIndex = 57;
            this.cmbGV.SelectedIndexChanged += new System.EventHandler(this.cmbGV_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.label1.Location = new System.Drawing.Point(12, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 20);
            this.label1.TabIndex = 58;
            this.label1.Text = "Danh sách GV";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1645, 819);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbGV);
            this.Controls.Add(this.btDong);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btExcel);
            this.Controls.Add(this.txtFileDuLieu);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.txtSoLuongNV);
            this.Controls.Add(this.oGrid);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.oGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox txtSoLuongNV;
        internal System.Windows.Forms.TextBox txtFileDuLieu;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Button btExcel;
        internal System.Windows.Forms.Button btnCheck;
        internal System.Windows.Forms.Button btDong;
        internal System.Windows.Forms.DataGridView oGrid;
        private System.Windows.Forms.ComboBox cmbGV;
        private System.Windows.Forms.Label label1;
    }
}

