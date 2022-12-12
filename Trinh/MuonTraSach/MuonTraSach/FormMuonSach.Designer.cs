namespace MuonTraSach
{
    partial class FormMuonSach
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.lbAmount = new System.Windows.Forms.Label();
            this.dtgvChosen = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgvStock = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.linkLabelClear = new System.Windows.Forms.LinkLabel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lbHeader = new System.Windows.Forms.Label();
            this.btnRemove = new MuonTraSach.But();
            this.btnAdd = new MuonTraSach.But();
            this.btnBorrowList = new MuonTraSach.But();
            this.btnUpdate = new MuonTraSach.But();
            this.btnBorrow = new MuonTraSach.But();
            this.vbButton2 = new MuonTraSach.VBButton();
            this.toggleButton2 = new MuonTraSach.ToggleButton();
            this.toggleButton1 = new MuonTraSach.ToggleButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbBorrowed = new System.Windows.Forms.Label();
            this.lbMaxBorrow = new System.Windows.Forms.Label();
            this.vbButton1 = new MuonTraSach.VBButton();
            this.lbWCode = new System.Windows.Forms.Label();
            this.txtReaderName = new System.Windows.Forms.TextBox();
            this.dtpReturn = new System.Windows.Forms.DateTimePicker();
            this.dtpBorrow = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbReaderId = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvChosen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvStock)).BeginInit();
            this.vbButton2.SuspendLayout();
            this.vbButton1.SuspendLayout();
            this.SuspendLayout();
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // lbAmount
            // 
            this.lbAmount.AutoSize = true;
            this.lbAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAmount.ForeColor = System.Drawing.Color.Maroon;
            this.lbAmount.Location = new System.Drawing.Point(1702, 583);
            this.lbAmount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbAmount.Name = "lbAmount";
            this.lbAmount.Size = new System.Drawing.Size(161, 33);
            this.lbAmount.TabIndex = 83;
            this.lbAmount.Text = "Số lượng: 5";
            // 
            // dtgvChosen
            // 
            this.dtgvChosen.AllowUserToAddRows = false;
            this.dtgvChosen.AllowUserToDeleteRows = false;
            this.dtgvChosen.AllowUserToResizeRows = false;
            this.dtgvChosen.BackgroundColor = System.Drawing.Color.White;
            this.dtgvChosen.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvChosen.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvChosen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvChosen.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.dtgvChosen.EnableHeadersVisualStyles = false;
            this.dtgvChosen.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.dtgvChosen.Location = new System.Drawing.Point(1036, 623);
            this.dtgvChosen.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.dtgvChosen.MultiSelect = false;
            this.dtgvChosen.Name = "dtgvChosen";
            this.dtgvChosen.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvChosen.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvChosen.RowHeadersVisible = false;
            this.dtgvChosen.RowHeadersWidth = 40;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dtgvChosen.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgvChosen.RowTemplate.Height = 26;
            this.dtgvChosen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvChosen.Size = new System.Drawing.Size(825, 470);
            this.dtgvChosen.TabIndex = 82;
            this.dtgvChosen.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvChosen_CellDoubleClick);
            this.dtgvChosen.SelectionChanged += new System.EventHandler(this.dtgvChosen_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Mã cuốn";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 90;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "bookId";
            this.dataGridViewTextBoxColumn2.HeaderText = "Mã sách";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 90;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn3.HeaderText = "Tên sách";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 167;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "category";
            this.dataGridViewTextBoxColumn4.HeaderText = "Thể loại";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "author";
            this.dataGridViewTextBoxColumn5.HeaderText = "Tác giả";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Chocolate;
            this.label1.Location = new System.Drawing.Point(75, 88);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(264, 38);
            this.label1.TabIndex = 81;
            this.label1.Text = "Nhập Thông Tin";
            // 
            // dtgvStock
            // 
            this.dtgvStock.AllowUserToAddRows = false;
            this.dtgvStock.AllowUserToDeleteRows = false;
            this.dtgvStock.AllowUserToOrderColumns = true;
            this.dtgvStock.AllowUserToResizeRows = false;
            this.dtgvStock.BackgroundColor = System.Drawing.Color.White;
            this.dtgvStock.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvStock.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10});
            this.dtgvStock.EnableHeadersVisualStyles = false;
            this.dtgvStock.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.dtgvStock.Location = new System.Drawing.Point(28, 623);
            this.dtgvStock.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.dtgvStock.MultiSelect = false;
            this.dtgvStock.Name = "dtgvStock";
            this.dtgvStock.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvStock.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgvStock.RowHeadersVisible = false;
            this.dtgvStock.RowHeadersWidth = 40;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dtgvStock.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dtgvStock.RowTemplate.Height = 26;
            this.dtgvStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvStock.Size = new System.Drawing.Size(825, 470);
            this.dtgvStock.TabIndex = 80;
            this.dtgvStock.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvStock_CellDoubleClick);
            this.dtgvStock.SelectionChanged += new System.EventHandler(this.dtgvStock_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn6.HeaderText = "Mã cuốn";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 90;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "bookId";
            this.dataGridViewTextBoxColumn7.HeaderText = "Mã sách";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 90;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn8.HeaderText = "Tên sách";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 167;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "category";
            this.dataGridViewTextBoxColumn9.HeaderText = "Thể loại";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 150;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "author";
            this.dataGridViewTextBoxColumn10.HeaderText = "Tác giả";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 150;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1030, 583);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(208, 35);
            this.label10.TabIndex = 79;
            this.label10.Text = "Sách đã chọn";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(24, 583);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(236, 35);
            this.label9.TabIndex = 78;
            this.label9.Text = "Sách trong kho";
            // 
            // linkLabelClear
            // 
            this.linkLabelClear.AutoSize = true;
            this.linkLabelClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelClear.Location = new System.Drawing.Point(801, 494);
            this.linkLabelClear.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.linkLabelClear.Name = "linkLabelClear";
            this.linkLabelClear.Size = new System.Drawing.Size(51, 26);
            this.linkLabelClear.TabIndex = 72;
            this.linkLabelClear.TabStop = true;
            this.linkLabelClear.Text = "Xóa";
            this.linkLabelClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelClear_LinkClicked);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(172, 488);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(614, 35);
            this.txtSearch.TabIndex = 70;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Chocolate;
            this.label8.Location = new System.Drawing.Point(21, 486);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 38);
            this.label8.TabIndex = 68;
            this.label8.Text = "Tìm sách:";
            // 
            // lbHeader
            // 
            this.lbHeader.AutoSize = true;
            this.lbHeader.BackColor = System.Drawing.Color.Transparent;
            this.lbHeader.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.ForeColor = System.Drawing.Color.Navy;
            this.lbHeader.Location = new System.Drawing.Point(734, 15);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(415, 70);
            this.lbHeader.TabIndex = 65;
            this.lbHeader.Text = "Cho Mượn Sách";
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.BackColor = System.Drawing.Color.LightCoral;
            this.btnRemove.BackgroundColor = System.Drawing.Color.LightCoral;
            this.btnRemove.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnRemove.BorderRadius = 20;
            this.btnRemove.BorderSize = 0;
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(873, 852);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4, 8, 4, 5);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(144, 62);
            this.btnRemove.TabIndex = 89;
            this.btnRemove.Text = "<- Bỏ";
            this.btnRemove.TextColor = System.Drawing.Color.White;
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.YellowGreen;
            this.btnAdd.BackgroundColor = System.Drawing.Color.YellowGreen;
            this.btnAdd.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnAdd.BorderRadius = 20;
            this.btnAdd.BorderSize = 0;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(873, 775);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(144, 62);
            this.btnAdd.TabIndex = 90;
            this.btnAdd.Text = "Thêm ->";
            this.btnAdd.TextColor = System.Drawing.Color.White;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnBorrowList
            // 
            this.btnBorrowList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrowList.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnBorrowList.BackgroundColor = System.Drawing.Color.MediumSeaGreen;
            this.btnBorrowList.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnBorrowList.BorderRadius = 12;
            this.btnBorrowList.BorderSize = 0;
            this.btnBorrowList.FlatAppearance.BorderSize = 0;
            this.btnBorrowList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrowList.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.btnBorrowList.ForeColor = System.Drawing.Color.White;
            this.btnBorrowList.Location = new System.Drawing.Point(1090, 108);
            this.btnBorrowList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBorrowList.Name = "btnBorrowList";
            this.btnBorrowList.Size = new System.Drawing.Size(285, 72);
            this.btnBorrowList.TabIndex = 86;
            this.btnBorrowList.Text = "Xem phiếu mượn";
            this.btnBorrowList.TextColor = System.Drawing.Color.White;
            this.btnBorrowList.UseVisualStyleBackColor = false;
            this.btnBorrowList.Click += new System.EventHandler(this.btnBorrowList_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.Color.LightCoral;
            this.btnUpdate.BackgroundColor = System.Drawing.Color.LightCoral;
            this.btnUpdate.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnUpdate.BorderRadius = 12;
            this.btnUpdate.BorderSize = 0;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(1642, 108);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 15, 5);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(192, 72);
            this.btnUpdate.TabIndex = 87;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.TextColor = System.Drawing.Color.White;
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnBorrow
            // 
            this.btnBorrow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrow.BackColor = System.Drawing.Color.YellowGreen;
            this.btnBorrow.BackgroundColor = System.Drawing.Color.YellowGreen;
            this.btnBorrow.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnBorrow.BorderRadius = 12;
            this.btnBorrow.BorderSize = 0;
            this.btnBorrow.FlatAppearance.BorderSize = 0;
            this.btnBorrow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrow.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.btnBorrow.ForeColor = System.Drawing.Color.White;
            this.btnBorrow.Location = new System.Drawing.Point(1411, 108);
            this.btnBorrow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(192, 72);
            this.btnBorrow.TabIndex = 88;
            this.btnBorrow.Text = "Cho mượn";
            this.btnBorrow.TextColor = System.Drawing.Color.White;
            this.btnBorrow.UseVisualStyleBackColor = false;
            this.btnBorrow.Click += new System.EventHandler(this.btnBorrow_Click);
            // 
            // vbButton2
            // 
            this.vbButton2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.vbButton2.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.vbButton2.BorderColor = System.Drawing.Color.Navy;
            this.vbButton2.BorderRadius = 20;
            this.vbButton2.BorderSize = 1;
            this.vbButton2.Controls.Add(this.toggleButton2);
            this.vbButton2.Controls.Add(this.toggleButton1);
            this.vbButton2.Controls.Add(this.label7);
            this.vbButton2.Controls.Add(this.label6);
            this.vbButton2.Controls.Add(this.lbBorrowed);
            this.vbButton2.Controls.Add(this.lbMaxBorrow);
            this.vbButton2.ForeColor = System.Drawing.Color.White;
            this.vbButton2.Location = new System.Drawing.Point(1070, 203);
            this.vbButton2.Name = "vbButton2";
            this.vbButton2.Size = new System.Drawing.Size(792, 258);
            this.vbButton2.TabIndex = 85;
            this.vbButton2.TextColor = System.Drawing.Color.White;
            // 
            // toggleButton2
            // 
            this.toggleButton2.Checked = true;
            this.toggleButton2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toggleButton2.Location = new System.Drawing.Point(453, 190);
            this.toggleButton2.MinimumSize = new System.Drawing.Size(45, 22);
            this.toggleButton2.Name = "toggleButton2";
            this.toggleButton2.OffBackColor = System.Drawing.Color.Gray;
            this.toggleButton2.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.toggleButton2.OnBackColor = System.Drawing.Color.SlateBlue;
            this.toggleButton2.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.toggleButton2.Size = new System.Drawing.Size(80, 43);
            this.toggleButton2.TabIndex = 65;
            this.toggleButton2.UseVisualStyleBackColor = true;
            this.toggleButton2.CheckedChanged += new System.EventHandler(this.toggleButton2_CheckedChanged);
            // 
            // toggleButton1
            // 
            this.toggleButton1.Checked = true;
            this.toggleButton1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toggleButton1.Location = new System.Drawing.Point(453, 124);
            this.toggleButton1.MinimumSize = new System.Drawing.Size(45, 22);
            this.toggleButton1.Name = "toggleButton1";
            this.toggleButton1.OffBackColor = System.Drawing.Color.Gray;
            this.toggleButton1.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.toggleButton1.OnBackColor = System.Drawing.Color.SlateBlue;
            this.toggleButton1.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.toggleButton1.Size = new System.Drawing.Size(80, 43);
            this.toggleButton1.TabIndex = 65;
            this.toggleButton1.UseVisualStyleBackColor = true;
            this.toggleButton1.CheckedChanged += new System.EventHandler(this.toggleButton1_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Navy;
            this.label7.Location = new System.Drawing.Point(27, 193);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(164, 29);
            this.label7.TabIndex = 64;
            this.label7.Text = "In phiếu mượn";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(27, 127);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(392, 29);
            this.label6.TabIndex = 63;
            this.label6.Text = "Áp dụng số sách tối đa có thể mượn";
            // 
            // lbBorrowed
            // 
            this.lbBorrowed.AutoSize = true;
            this.lbBorrowed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBorrowed.ForeColor = System.Drawing.Color.Maroon;
            this.lbBorrowed.Location = new System.Drawing.Point(27, 73);
            this.lbBorrowed.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbBorrowed.Name = "lbBorrowed";
            this.lbBorrowed.Size = new System.Drawing.Size(325, 33);
            this.lbBorrowed.TabIndex = 62;
            this.lbBorrowed.Text = "Số sách đang mượn: 0";
            // 
            // lbMaxBorrow
            // 
            this.lbMaxBorrow.AutoSize = true;
            this.lbMaxBorrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMaxBorrow.ForeColor = System.Drawing.Color.Navy;
            this.lbMaxBorrow.Location = new System.Drawing.Point(27, 22);
            this.lbMaxBorrow.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbMaxBorrow.Name = "lbMaxBorrow";
            this.lbMaxBorrow.Size = new System.Drawing.Size(410, 33);
            this.lbMaxBorrow.TabIndex = 62;
            this.lbMaxBorrow.Text = "Số sách được mượn tối đa: 5\r\n";
            // 
            // vbButton1
            // 
            this.vbButton1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.vbButton1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.vbButton1.BorderColor = System.Drawing.Color.Navy;
            this.vbButton1.BorderRadius = 20;
            this.vbButton1.BorderSize = 1;
            this.vbButton1.Controls.Add(this.lbWCode);
            this.vbButton1.Controls.Add(this.txtReaderName);
            this.vbButton1.Controls.Add(this.dtpReturn);
            this.vbButton1.Controls.Add(this.dtpBorrow);
            this.vbButton1.Controls.Add(this.label5);
            this.vbButton1.Controls.Add(this.label4);
            this.vbButton1.Controls.Add(this.label3);
            this.vbButton1.Controls.Add(this.cbbReaderId);
            this.vbButton1.Controls.Add(this.label2);
            this.vbButton1.ForeColor = System.Drawing.Color.White;
            this.vbButton1.Location = new System.Drawing.Point(28, 108);
            this.vbButton1.Margin = new System.Windows.Forms.Padding(15);
            this.vbButton1.Name = "vbButton1";
            this.vbButton1.Size = new System.Drawing.Size(1004, 354);
            this.vbButton1.TabIndex = 84;
            this.vbButton1.TextColor = System.Drawing.Color.White;
            // 
            // lbWCode
            // 
            this.lbWCode.AutoSize = true;
            this.lbWCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbWCode.Location = new System.Drawing.Point(278, 95);
            this.lbWCode.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbWCode.Name = "lbWCode";
            this.lbWCode.Size = new System.Drawing.Size(210, 25);
            this.lbWCode.TabIndex = 39;
            this.lbWCode.Text = "Không tìm thấy độc giả";
            this.lbWCode.Visible = false;
            // 
            // txtReaderName
            // 
            this.txtReaderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtReaderName.Location = new System.Drawing.Point(282, 162);
            this.txtReaderName.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.txtReaderName.Name = "txtReaderName";
            this.txtReaderName.Size = new System.Drawing.Size(520, 35);
            this.txtReaderName.TabIndex = 38;
            // 
            // dtpReturn
            // 
            this.dtpReturn.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReturn.CustomFormat = "dd/MM/yyyy";
            this.dtpReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReturn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReturn.Location = new System.Drawing.Point(740, 265);
            this.dtpReturn.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.dtpReturn.Name = "dtpReturn";
            this.dtpReturn.Size = new System.Drawing.Size(162, 35);
            this.dtpReturn.TabIndex = 37;
            // 
            // dtpBorrow
            // 
            this.dtpBorrow.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBorrow.CustomFormat = "dd/MM/yyyy";
            this.dtpBorrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBorrow.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBorrow.Location = new System.Drawing.Point(282, 265);
            this.dtpBorrow.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.dtpBorrow.Name = "dtpBorrow";
            this.dtpBorrow.Size = new System.Drawing.Size(162, 35);
            this.dtpBorrow.TabIndex = 32;
            this.dtpBorrow.ValueChanged += new System.EventHandler(this.dtpBorrow_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(549, 272);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 29);
            this.label5.TabIndex = 36;
            this.label5.Text = "Ngày trả:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(52, 272);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 29);
            this.label4.TabIndex = 35;
            this.label4.Text = "Ngày mượn:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(52, 166);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 29);
            this.label3.TabIndex = 34;
            this.label3.Text = "Tên độc giả:";
            // 
            // cbbReaderId
            // 
            this.cbbReaderId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbbReaderId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbReaderId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbbReaderId.FormattingEnabled = true;
            this.cbbReaderId.Location = new System.Drawing.Point(282, 46);
            this.cbbReaderId.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.cbbReaderId.Name = "cbbReaderId";
            this.cbbReaderId.Size = new System.Drawing.Size(373, 37);
            this.cbbReaderId.TabIndex = 31;
            this.cbbReaderId.SelectedIndexChanged += new System.EventHandler(this.cbbReaderId_SelectedIndexChanged);
            this.cbbReaderId.TextChanged += new System.EventHandler(this.cbbReaderId_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(52, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 29);
            this.label2.TabIndex = 33;
            this.label2.Text = "Mã độc giả:";
            // 
            // FormMuonSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1890, 1114);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnBorrowList);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnBorrow);
            this.Controls.Add(this.vbButton2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.vbButton1);
            this.Controls.Add(this.lbAmount);
            this.Controls.Add(this.dtgvChosen);
            this.Controls.Add(this.dtgvStock);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.linkLabelClear);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lbHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMuonSach";
            this.Text = "Mượn Sách";
            this.Load += new System.EventHandler(this.FormMuonSach_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvChosen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvStock)).EndInit();
            this.vbButton2.ResumeLayout(false);
            this.vbButton2.PerformLayout();
            this.vbButton1.ResumeLayout(false);
            this.vbButton1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Diagnostics.EventLog eventLog1;
        private MuonTraSach.VBButton vbButton1;
        private System.Windows.Forms.Label lbWCode;
        private System.Windows.Forms.TextBox txtReaderName;
        private System.Windows.Forms.DateTimePicker dtpReturn;
        private System.Windows.Forms.DateTimePicker dtpBorrow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbReaderId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbAmount;
        private System.Windows.Forms.DataGridView dtgvChosen;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgvStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel linkLabelClear;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbHeader;
        private MuonTraSach.VBButton vbButton2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbBorrowed;
        private System.Windows.Forms.Label lbMaxBorrow;
        private MuonTraSach.But btnRemove;
        private MuonTraSach.But btnAdd;
        private MuonTraSach.But btnBorrowList;
        private MuonTraSach.But btnUpdate;
        private MuonTraSach.But btnBorrow;
        private MuonTraSach.ToggleButton toggleButton2;
        private MuonTraSach.ToggleButton toggleButton1;
    }
}

