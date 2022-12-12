namespace MuonTraSach
{
    partial class FormDanhSachPM
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpBorrow = new System.Windows.Forms.DateTimePicker();
            this.dtgv = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.readerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnUpdate = new MuonTraSach.But();
            this.btnCancel = new MuonTraSach.But();
            this.btnDelete = new MuonTraSach.But();
            this.btnDetail = new MuonTraSach.But();
            this.btnExit = new MuonTraSach.But();
            this.pnlSlipId = new System.Windows.Forms.Panel();
            this.lbSlipId = new System.Windows.Forms.Label();
            this.lbReaderName = new System.Windows.Forms.Label();
            this.pnlReaderName = new System.Windows.Forms.Panel();
            this.pnlReaderId = new System.Windows.Forms.Panel();
            this.lbReaderId = new System.Windows.Forms.Label();
            this.lbReturnDate = new System.Windows.Forms.Label();
            this.pnlReturnDate = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpBorrow
            // 
            this.dtpBorrow.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBorrow.Checked = false;
            this.dtpBorrow.CustomFormat = "dd/MM/yyyy";
            this.dtpBorrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBorrow.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBorrow.Location = new System.Drawing.Point(798, 240);
            this.dtpBorrow.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.dtpBorrow.Name = "dtpBorrow";
            this.dtpBorrow.Size = new System.Drawing.Size(162, 35);
            this.dtpBorrow.TabIndex = 90;
            this.dtpBorrow.ValueChanged += new System.EventHandler(this.dtpBorrow_ValueChanged);
            this.dtpBorrow.DropDown += new System.EventHandler(this.dtpBorrow_DropDown);
            // 
            // dtgv
            // 
            this.dtgv.AllowUserToAddRows = false;
            this.dtgv.AllowUserToResizeRows = false;
            this.dtgv.BackgroundColor = System.Drawing.Color.White;
            this.dtgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(84)))), ((int)(((byte)(131)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(84)))), ((int)(((byte)(131)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.readerName,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10});
            this.dtgv.EnableHeadersVisualStyles = false;
            this.dtgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(84)))), ((int)(((byte)(131)))));
            this.dtgv.Location = new System.Drawing.Point(30, 408);
            this.dtgv.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.dtgv.MultiSelect = false;
            this.dtgv.Name = "dtgv";
            this.dtgv.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgv.RowHeadersVisible = false;
            this.dtgv.RowHeadersWidth = 40;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dtgv.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dtgv.RowTemplate.Height = 26;
            this.dtgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgv.Size = new System.Drawing.Size(1440, 431);
            this.dtgv.TabIndex = 89;
            this.dtgv.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgv_CellMouseDown);
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "stt";
            this.dataGridViewTextBoxColumn6.HeaderText = "STT";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 50;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "slipCode";
            this.dataGridViewTextBoxColumn7.HeaderText = "Mã phiếu mượn";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 160;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "code";
            this.dataGridViewTextBoxColumn8.HeaderText = "Mã độc giả";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 160;
            // 
            // readerName
            // 
            this.readerName.DataPropertyName = "name";
            this.readerName.HeaderText = "Tên độc giả";
            this.readerName.MinimumWidth = 8;
            this.readerName.Name = "readerName";
            this.readerName.ReadOnly = true;
            this.readerName.Width = 267;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "borrowDate";
            this.dataGridViewTextBoxColumn9.HeaderText = "Ngày mượn";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 160;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "returnDate";
            this.dataGridViewTextBoxColumn10.HeaderText = "Ngày trả";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 160;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(30, 154);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 33);
            this.label2.TabIndex = 88;
            this.label2.Text = "Mã phiếu mượn:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(660, 329);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 33);
            this.label6.TabIndex = 87;
            this.label6.Text = "Ngày trả:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(616, 240);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(172, 33);
            this.label5.TabIndex = 86;
            this.label5.Text = "Ngày mượn:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(76, 329);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 33);
            this.label4.TabIndex = 85;
            this.label4.Text = "Tên độc giả:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(88, 240);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 33);
            this.label3.TabIndex = 84;
            this.label3.Text = "Mã độc giả:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(490, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(519, 61);
            this.label1.TabIndex = 83;
            this.label1.Text = "Danh Sách Phiếu Mượn";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(84)))), ((int)(((byte)(131)))));
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1500, 58);
            this.pnlTop.TabIndex = 82;
            this.pnlTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseDown);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnUpdate.BackgroundColor = System.Drawing.Color.CornflowerBlue;
            this.btnUpdate.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnUpdate.BorderRadius = 12;
            this.btnUpdate.BorderSize = 0;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(1081, 213);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(180, 65);
            this.btnUpdate.TabIndex = 93;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.TextColor = System.Drawing.Color.White;
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.IndianRed;
            this.btnCancel.BackgroundColor = System.Drawing.Color.IndianRed;
            this.btnCancel.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnCancel.BorderRadius = 12;
            this.btnCancel.BorderSize = 0;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(1290, 213);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 65);
            this.btnCancel.TabIndex = 93;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.TextColor = System.Drawing.Color.White;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.Chocolate;
            this.btnDelete.BackgroundColor = System.Drawing.Color.Chocolate;
            this.btnDelete.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnDelete.BorderRadius = 12;
            this.btnDelete.BorderSize = 0;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(1081, 299);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(180, 65);
            this.btnDelete.TabIndex = 93;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextColor = System.Drawing.Color.White;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnDetail
            // 
            this.btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetail.BackColor = System.Drawing.Color.MediumPurple;
            this.btnDetail.BackgroundColor = System.Drawing.Color.MediumPurple;
            this.btnDetail.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnDetail.BorderRadius = 12;
            this.btnDetail.BorderSize = 0;
            this.btnDetail.FlatAppearance.BorderSize = 0;
            this.btnDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetail.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnDetail.ForeColor = System.Drawing.Color.White;
            this.btnDetail.Location = new System.Drawing.Point(1290, 299);
            this.btnDetail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(180, 65);
            this.btnDetail.TabIndex = 93;
            this.btnDetail.Text = "Chi tiết";
            this.btnDetail.TextColor = System.Drawing.Color.White;
            this.btnDetail.UseVisualStyleBackColor = false;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.LightCoral;
            this.btnExit.BackgroundColor = System.Drawing.Color.LightCoral;
            this.btnExit.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnExit.BorderRadius = 20;
            this.btnExit.BorderSize = 0;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(654, 860);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(192, 72);
            this.btnExit.TabIndex = 94;
            this.btnExit.Text = "Thoát";
            this.btnExit.TextColor = System.Drawing.Color.White;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlSlipId
            // 
            this.pnlSlipId.BackColor = System.Drawing.Color.Black;
            this.pnlSlipId.Location = new System.Drawing.Point(267, 191);
            this.pnlSlipId.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pnlSlipId.Name = "pnlSlipId";
            this.pnlSlipId.Size = new System.Drawing.Size(0, 2);
            this.pnlSlipId.TabIndex = 100;
            // 
            // lbSlipId
            // 
            this.lbSlipId.AutoSize = true;
            this.lbSlipId.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSlipId.Location = new System.Drawing.Point(264, 154);
            this.lbSlipId.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbSlipId.Name = "lbSlipId";
            this.lbSlipId.Size = new System.Drawing.Size(0, 35);
            this.lbSlipId.TabIndex = 99;
            // 
            // lbReaderName
            // 
            this.lbReaderName.AutoSize = true;
            this.lbReaderName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReaderName.Location = new System.Drawing.Point(264, 329);
            this.lbReaderName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbReaderName.Name = "lbReaderName";
            this.lbReaderName.Size = new System.Drawing.Size(0, 35);
            this.lbReaderName.TabIndex = 98;
            // 
            // pnlReaderName
            // 
            this.pnlReaderName.BackColor = System.Drawing.Color.Black;
            this.pnlReaderName.Location = new System.Drawing.Point(267, 366);
            this.pnlReaderName.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pnlReaderName.Name = "pnlReaderName";
            this.pnlReaderName.Size = new System.Drawing.Size(0, 2);
            this.pnlReaderName.TabIndex = 95;
            // 
            // pnlReaderId
            // 
            this.pnlReaderId.BackColor = System.Drawing.Color.Black;
            this.pnlReaderId.Location = new System.Drawing.Point(267, 277);
            this.pnlReaderId.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pnlReaderId.Name = "pnlReaderId";
            this.pnlReaderId.Size = new System.Drawing.Size(0, 2);
            this.pnlReaderId.TabIndex = 96;
            // 
            // lbReaderId
            // 
            this.lbReaderId.AutoSize = true;
            this.lbReaderId.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReaderId.Location = new System.Drawing.Point(264, 240);
            this.lbReaderId.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbReaderId.Name = "lbReaderId";
            this.lbReaderId.Size = new System.Drawing.Size(0, 35);
            this.lbReaderId.TabIndex = 97;
            // 
            // lbReturnDate
            // 
            this.lbReturnDate.AutoSize = true;
            this.lbReturnDate.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReturnDate.Location = new System.Drawing.Point(798, 329);
            this.lbReturnDate.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbReturnDate.Name = "lbReturnDate";
            this.lbReturnDate.Size = new System.Drawing.Size(0, 35);
            this.lbReturnDate.TabIndex = 102;
            // 
            // pnlReturnDate
            // 
            this.pnlReturnDate.BackColor = System.Drawing.Color.Black;
            this.pnlReturnDate.Location = new System.Drawing.Point(801, 366);
            this.pnlReturnDate.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pnlReturnDate.Name = "pnlReturnDate";
            this.pnlReturnDate.Size = new System.Drawing.Size(0, 2);
            this.pnlReturnDate.TabIndex = 101;
            // 
            // FormDanhSachPM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 962);
            this.Controls.Add(this.lbReturnDate);
            this.Controls.Add(this.pnlReturnDate);
            this.Controls.Add(this.pnlSlipId);
            this.Controls.Add(this.lbSlipId);
            this.Controls.Add(this.lbReaderName);
            this.Controls.Add(this.pnlReaderName);
            this.Controls.Add(this.pnlReaderId);
            this.Controls.Add(this.lbReaderId);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.dtpBorrow);
            this.Controls.Add(this.dtgv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDanhSachPM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách Phiếu mượn";
            this.Load += new System.EventHandler(this.FormDanhSachPM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dtpBorrow;
        private System.Windows.Forms.DataGridView dtgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn readerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlTop;
        private But btnUpdate;
        private But btnCancel;
        private But btnDelete;
        private But btnDetail;
        private But btnExit;
        private System.Windows.Forms.Panel pnlSlipId;
        private System.Windows.Forms.Label lbSlipId;
        private System.Windows.Forms.Label lbReaderName;
        private System.Windows.Forms.Panel pnlReaderName;
        private System.Windows.Forms.Panel pnlReaderId;
        private System.Windows.Forms.Label lbReaderId;
        private System.Windows.Forms.Label lbReturnDate;
        private System.Windows.Forms.Panel pnlReturnDate;
    }
}