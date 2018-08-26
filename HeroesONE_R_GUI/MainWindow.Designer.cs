using System.Windows.Forms;

namespace HeroesONE_R_GUI
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            Reloaded_GUI.Styles.Animation.AnimProperties animProperties6 = new Reloaded_GUI.Styles.Animation.AnimProperties();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage11 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage12 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimProperties animProperties7 = new Reloaded_GUI.Styles.Animation.AnimProperties();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage13 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage14 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            Reloaded_GUI.Styles.Animation.AnimProperties animProperties8 = new Reloaded_GUI.Styles.Animation.AnimProperties();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage15 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage16 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimProperties animProperties9 = new Reloaded_GUI.Styles.Animation.AnimProperties();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage17 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage18 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimProperties animProperties10 = new Reloaded_GUI.Styles.Animation.AnimProperties();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage19 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage20 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            this.titleBar_Title = new Reloaded_GUI.Styles.Controls.Animated.AnimatedButton();
            this.box_FileList = new HeroesONE_R_GUI.Controls.CustomDataGridView();
            this.fileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rwVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryBar_Close = new Reloaded_GUI.Styles.Controls.Animated.AnimatedButton();
            this.titleBar_RWVersion = new Reloaded_GUI.Styles.Controls.Animated.AnimatedButton();
            this.titleBar_ItemCount = new Reloaded_GUI.Styles.Controls.Animated.AnimatedButton();
            this.titleBar_Panel = new System.Windows.Forms.Panel();
            this.categoryBar_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.categoryBar_FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveShadow050ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveShadow060ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categoryBar_AddFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.categoryBar_OptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setArchiveRWVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAllFileRWVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compressionSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableAdaptiveCompressionLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manuallySetCompressionLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.box_MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeRWVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.titleBar_StatusBar = new System.Windows.Forms.Panel();
            this.categoryBar_ExtractAll = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.box_FileList)).BeginInit();
            this.titleBar_Panel.SuspendLayout();
            this.categoryBar_MenuStrip.SuspendLayout();
            this.box_MenuStrip.SuspendLayout();
            this.titleBar_StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleBar_Title
            // 
            animMessage11.Control = this.titleBar_Title;
            animMessage11.PlayAnimation = true;
            animProperties6.BackColorMessage = animMessage11;
            animMessage12.Control = this.titleBar_Title;
            animMessage12.PlayAnimation = true;
            animProperties6.ForeColorMessage = animMessage12;
            animProperties6.MouseEnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            animProperties6.MouseEnterDuration = 200F;
            animProperties6.MouseEnterForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(163)))), ((int)(((byte)(244)))));
            animProperties6.MouseEnterFramerate = 144F;
            animProperties6.MouseEnterOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseEnterOverride.None;
            animProperties6.MouseLeaveBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            animProperties6.MouseLeaveDuration = 200F;
            animProperties6.MouseLeaveForeColor = System.Drawing.Color.White;
            animProperties6.MouseLeaveFramerate = 144F;
            animProperties6.MouseLeaveOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseLeaveOverride.None;
            this.titleBar_Title.AnimProperties = animProperties6;
            this.titleBar_Title.CaptureChildren = false;
            this.titleBar_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleBar_Title.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titleBar_Title.FlatAppearance.BorderSize = 0;
            this.titleBar_Title.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titleBar_Title.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titleBar_Title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.titleBar_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.titleBar_Title.ForeColor = System.Drawing.Color.White;
            this.titleBar_Title.IgnoreMouse = false;
            this.titleBar_Title.IgnoreMouseClicks = false;
            this.titleBar_Title.Location = new System.Drawing.Point(0, 0);
            this.titleBar_Title.Name = "titleBar_Title";
            this.titleBar_Title.Size = new System.Drawing.Size(362, 42);
            this.titleBar_Title.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.titleBar_Title.TabIndex = 4;
            this.titleBar_Title.Text = "HeroesONE Reloaded";
            this.titleBar_Title.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.titleBar_Title.UseVisualStyleBackColor = true;
            // 
            // box_FileList
            // 
            this.box_FileList.AllowDrop = true;
            this.box_FileList.AllowUserToAddRows = false;
            this.box_FileList.AllowUserToDeleteRows = false;
            this.box_FileList.AllowUserToResizeColumns = false;
            this.box_FileList.AllowUserToResizeRows = false;
            animMessage13.Control = this.box_FileList;
            animMessage13.PlayAnimation = true;
            animProperties7.BackColorMessage = animMessage13;
            animMessage14.Control = this.box_FileList;
            animMessage14.PlayAnimation = true;
            animProperties7.ForeColorMessage = animMessage14;
            animProperties7.MouseEnterBackColor = System.Drawing.Color.Empty;
            animProperties7.MouseEnterDuration = 0F;
            animProperties7.MouseEnterForeColor = System.Drawing.Color.Empty;
            animProperties7.MouseEnterFramerate = 0F;
            animProperties7.MouseEnterOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseEnterOverride.None;
            animProperties7.MouseLeaveBackColor = System.Drawing.Color.Empty;
            animProperties7.MouseLeaveDuration = 0F;
            animProperties7.MouseLeaveForeColor = System.Drawing.Color.Empty;
            animProperties7.MouseLeaveFramerate = 0F;
            animProperties7.MouseLeaveOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseLeaveOverride.None;
            this.box_FileList.AnimProperties = animProperties7;
            this.box_FileList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.box_FileList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.box_FileList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.box_FileList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.box_FileList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.box_FileList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.box_FileList.ColumnHeadersVisible = false;
            this.box_FileList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileName,
            this.rwVersion});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.box_FileList.DefaultCellStyle = dataGridViewCellStyle6;
            this.box_FileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.box_FileList.DragRowIndex = 0;
            this.box_FileList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.box_FileList.EnableHeadersVisualStyles = false;
            this.box_FileList.GridColor = System.Drawing.Color.White;
            this.box_FileList.Location = new System.Drawing.Point(0, 66);
            this.box_FileList.MultiSelect = false;
            this.box_FileList.Name = "box_FileList";
            this.box_FileList.ReadOnly = true;
            this.box_FileList.ReorderingEnabled = false;
            this.box_FileList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.box_FileList.RowHeadersVisible = false;
            this.box_FileList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.box_FileList.RowTemplate.Height = 20;
            this.box_FileList.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.box_FileList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.box_FileList.ShowCellToolTips = false;
            this.box_FileList.Size = new System.Drawing.Size(362, 426);
            this.box_FileList.StandardTab = true;
            this.box_FileList.TabIndex = 15;
            this.box_FileList.CustomDragDropEvent += new System.EventHandler<System.Windows.Forms.DragEventArgs>(this.FileList_DragDrop);
            this.box_FileList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.box_FileList_CellMouseClick);
            this.box_FileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.FileList_DragEnter);
            this.box_FileList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.box_FileList_KeyUp);
            // 
            // fileName
            // 
            this.fileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.fileName.DefaultCellStyle = dataGridViewCellStyle4;
            this.fileName.FillWeight = 70F;
            this.fileName.HeaderText = "File Name";
            this.fileName.Name = "fileName";
            this.fileName.ReadOnly = true;
            // 
            // rwVersion
            // 
            this.rwVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.rwVersion.DefaultCellStyle = dataGridViewCellStyle5;
            this.rwVersion.FillWeight = 30F;
            this.rwVersion.HeaderText = "RW Version";
            this.rwVersion.Name = "rwVersion";
            this.rwVersion.ReadOnly = true;
            // 
            // categoryBar_Close
            // 
            animMessage15.Control = this.categoryBar_Close;
            animMessage15.PlayAnimation = true;
            animProperties8.BackColorMessage = animMessage15;
            animMessage16.Control = this.categoryBar_Close;
            animMessage16.PlayAnimation = true;
            animProperties8.ForeColorMessage = animMessage16;
            animProperties8.MouseEnterBackColor = System.Drawing.Color.Empty;
            animProperties8.MouseEnterDuration = 0F;
            animProperties8.MouseEnterForeColor = System.Drawing.Color.Empty;
            animProperties8.MouseEnterFramerate = 0F;
            animProperties8.MouseEnterOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseEnterOverride.None;
            animProperties8.MouseLeaveBackColor = System.Drawing.Color.Empty;
            animProperties8.MouseLeaveDuration = 0F;
            animProperties8.MouseLeaveForeColor = System.Drawing.Color.Empty;
            animProperties8.MouseLeaveFramerate = 0F;
            animProperties8.MouseLeaveOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseLeaveOverride.None;
            this.categoryBar_Close.AnimProperties = animProperties8;
            this.categoryBar_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            this.categoryBar_Close.CaptureChildren = false;
            this.categoryBar_Close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.categoryBar_Close.FlatAppearance.BorderSize = 0;
            this.categoryBar_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.categoryBar_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.categoryBar_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.categoryBar_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.categoryBar_Close.ForeColor = System.Drawing.Color.White;
            this.categoryBar_Close.IgnoreMouse = false;
            this.categoryBar_Close.IgnoreMouseClicks = false;
            this.categoryBar_Close.Location = new System.Drawing.Point(332, 0);
            this.categoryBar_Close.Name = "categoryBar_Close";
            this.categoryBar_Close.Size = new System.Drawing.Size(30, 30);
            this.categoryBar_Close.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.categoryBar_Close.TabIndex = 52;
            this.categoryBar_Close.Text = "X";
            this.categoryBar_Close.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.categoryBar_Close.UseVisualStyleBackColor = false;
            this.categoryBar_Close.Click += new System.EventHandler(this.categoryBar_Close_Click);
            // 
            // titleBar_RWVersion
            // 
            animMessage17.Control = this.titleBar_RWVersion;
            animMessage17.PlayAnimation = true;
            animProperties9.BackColorMessage = animMessage17;
            animMessage18.Control = this.titleBar_RWVersion;
            animMessage18.PlayAnimation = true;
            animProperties9.ForeColorMessage = animMessage18;
            animProperties9.MouseEnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            animProperties9.MouseEnterDuration = 200F;
            animProperties9.MouseEnterForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(163)))), ((int)(((byte)(244)))));
            animProperties9.MouseEnterFramerate = 144F;
            animProperties9.MouseEnterOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseEnterOverride.None;
            animProperties9.MouseLeaveBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            animProperties9.MouseLeaveDuration = 200F;
            animProperties9.MouseLeaveForeColor = System.Drawing.Color.White;
            animProperties9.MouseLeaveFramerate = 144F;
            animProperties9.MouseLeaveOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseLeaveOverride.None;
            this.titleBar_RWVersion.AnimProperties = animProperties9;
            this.titleBar_RWVersion.CaptureChildren = false;
            this.titleBar_RWVersion.Dock = System.Windows.Forms.DockStyle.Left;
            this.titleBar_RWVersion.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titleBar_RWVersion.FlatAppearance.BorderSize = 0;
            this.titleBar_RWVersion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titleBar_RWVersion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titleBar_RWVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.titleBar_RWVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBar_RWVersion.ForeColor = System.Drawing.Color.White;
            this.titleBar_RWVersion.IgnoreMouse = false;
            this.titleBar_RWVersion.IgnoreMouseClicks = false;
            this.titleBar_RWVersion.Location = new System.Drawing.Point(0, 0);
            this.titleBar_RWVersion.Name = "titleBar_RWVersion";
            this.titleBar_RWVersion.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.titleBar_RWVersion.Size = new System.Drawing.Size(231, 30);
            this.titleBar_RWVersion.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.titleBar_RWVersion.TabIndex = 5;
            this.titleBar_RWVersion.Text = "RW Version: 3.5.0.0.FFFF";
            this.titleBar_RWVersion.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.titleBar_RWVersion.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.titleBar_RWVersion.UseVisualStyleBackColor = true;
            // 
            // titleBar_ItemCount
            // 
            animMessage19.Control = this.titleBar_ItemCount;
            animMessage19.PlayAnimation = true;
            animProperties10.BackColorMessage = animMessage19;
            animMessage20.Control = this.titleBar_ItemCount;
            animMessage20.PlayAnimation = true;
            animProperties10.ForeColorMessage = animMessage20;
            animProperties10.MouseEnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            animProperties10.MouseEnterDuration = 200F;
            animProperties10.MouseEnterForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(163)))), ((int)(((byte)(244)))));
            animProperties10.MouseEnterFramerate = 144F;
            animProperties10.MouseEnterOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseEnterOverride.None;
            animProperties10.MouseLeaveBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            animProperties10.MouseLeaveDuration = 200F;
            animProperties10.MouseLeaveForeColor = System.Drawing.Color.White;
            animProperties10.MouseLeaveFramerate = 144F;
            animProperties10.MouseLeaveOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseLeaveOverride.None;
            this.titleBar_ItemCount.AnimProperties = animProperties10;
            this.titleBar_ItemCount.AutoSize = true;
            this.titleBar_ItemCount.CaptureChildren = false;
            this.titleBar_ItemCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.titleBar_ItemCount.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titleBar_ItemCount.FlatAppearance.BorderSize = 0;
            this.titleBar_ItemCount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titleBar_ItemCount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titleBar_ItemCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.titleBar_ItemCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBar_ItemCount.ForeColor = System.Drawing.Color.White;
            this.titleBar_ItemCount.IgnoreMouse = false;
            this.titleBar_ItemCount.IgnoreMouseClicks = false;
            this.titleBar_ItemCount.Location = new System.Drawing.Point(226, 0);
            this.titleBar_ItemCount.Name = "titleBar_ItemCount";
            this.titleBar_ItemCount.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.titleBar_ItemCount.Size = new System.Drawing.Size(136, 30);
            this.titleBar_ItemCount.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.titleBar_ItemCount.TabIndex = 6;
            this.titleBar_ItemCount.Text = "Files: 0";
            this.titleBar_ItemCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.titleBar_ItemCount.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.titleBar_ItemCount.UseVisualStyleBackColor = true;
            // 
            // titleBar_Panel
            // 
            this.titleBar_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            this.titleBar_Panel.Controls.Add(this.categoryBar_Close);
            this.titleBar_Panel.Controls.Add(this.titleBar_Title);
            this.titleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar_Panel.Location = new System.Drawing.Point(0, 0);
            this.titleBar_Panel.Name = "titleBar_Panel";
            this.titleBar_Panel.Size = new System.Drawing.Size(362, 42);
            this.titleBar_Panel.TabIndex = 0;
            // 
            // categoryBar_MenuStrip
            // 
            this.categoryBar_MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(41)))), ((int)(((byte)(56)))));
            this.categoryBar_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.categoryBar_FileMenuItem,
            this.categoryBar_ExtractAll,
            this.categoryBar_AddFiles,
            this.categoryBar_OptionsMenuItem});
            this.categoryBar_MenuStrip.Location = new System.Drawing.Point(0, 42);
            this.categoryBar_MenuStrip.Name = "categoryBar_MenuStrip";
            this.categoryBar_MenuStrip.Size = new System.Drawing.Size(362, 24);
            this.categoryBar_MenuStrip.TabIndex = 1;
            // 
            // categoryBar_FileMenuItem
            // 
            this.categoryBar_FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveShadow050ToolStripMenuItem,
            this.saveShadow060ToolStripMenuItem});
            this.categoryBar_FileMenuItem.ForeColor = System.Drawing.Color.Silver;
            this.categoryBar_FileMenuItem.Name = "categoryBar_FileMenuItem";
            this.categoryBar_FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.categoryBar_FileMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Checked = true;
            this.saveToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.saveToolStripMenuItem.Text = "Save (Heroes)";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveShadow050ToolStripMenuItem
            // 
            this.saveShadow050ToolStripMenuItem.Name = "saveShadow050ToolStripMenuItem";
            this.saveShadow050ToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.saveShadow050ToolStripMenuItem.Text = "Save (Shadow 0.50)";
            this.saveShadow050ToolStripMenuItem.Click += new System.EventHandler(this.saveShadow050ToolStripMenuItem_Click);
            // 
            // saveShadow060ToolStripMenuItem
            // 
            this.saveShadow060ToolStripMenuItem.Name = "saveShadow060ToolStripMenuItem";
            this.saveShadow060ToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.saveShadow060ToolStripMenuItem.Text = "Save (Shadow 0.60)";
            this.saveShadow060ToolStripMenuItem.Click += new System.EventHandler(this.saveShadow060ToolStripMenuItem_Click);
            // 
            // categoryBar_AddFiles
            // 
            this.categoryBar_AddFiles.ForeColor = System.Drawing.Color.Silver;
            this.categoryBar_AddFiles.Name = "categoryBar_AddFiles";
            this.categoryBar_AddFiles.Size = new System.Drawing.Size(67, 20);
            this.categoryBar_AddFiles.Text = "Add Files";
            this.categoryBar_AddFiles.Click += new System.EventHandler(this.categoryBar_AddFiles_Click);
            // 
            // categoryBar_OptionsMenuItem
            // 
            this.categoryBar_OptionsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setArchiveRWVersionToolStripMenuItem,
            this.setAllFileRWVersionToolStripMenuItem,
            this.compressionSettingsToolStripMenuItem});
            this.categoryBar_OptionsMenuItem.ForeColor = System.Drawing.Color.Silver;
            this.categoryBar_OptionsMenuItem.Name = "categoryBar_OptionsMenuItem";
            this.categoryBar_OptionsMenuItem.Size = new System.Drawing.Size(61, 20);
            this.categoryBar_OptionsMenuItem.Text = "Options";
            // 
            // setArchiveRWVersionToolStripMenuItem
            // 
            this.setArchiveRWVersionToolStripMenuItem.Name = "setArchiveRWVersionToolStripMenuItem";
            this.setArchiveRWVersionToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.setArchiveRWVersionToolStripMenuItem.Text = "Set Archive RW Version";
            this.setArchiveRWVersionToolStripMenuItem.Click += new System.EventHandler(this.setArchiveRWVersionToolStripMenuItem_Click);
            // 
            // setAllFileRWVersionToolStripMenuItem
            // 
            this.setAllFileRWVersionToolStripMenuItem.Name = "setAllFileRWVersionToolStripMenuItem";
            this.setAllFileRWVersionToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.setAllFileRWVersionToolStripMenuItem.Text = "Set All File RW Version";
            this.setAllFileRWVersionToolStripMenuItem.Click += new System.EventHandler(this.setAllFileRWVersionToolStripMenuItem_Click);
            // 
            // compressionSettingsToolStripMenuItem
            // 
            this.compressionSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableAdaptiveCompressionLevelToolStripMenuItem,
            this.manuallySetCompressionLevelToolStripMenuItem});
            this.compressionSettingsToolStripMenuItem.Name = "compressionSettingsToolStripMenuItem";
            this.compressionSettingsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.compressionSettingsToolStripMenuItem.Text = "Compression Settings";
            // 
            // enableAdaptiveCompressionLevelToolStripMenuItem
            // 
            this.enableAdaptiveCompressionLevelToolStripMenuItem.Checked = true;
            this.enableAdaptiveCompressionLevelToolStripMenuItem.CheckOnClick = true;
            this.enableAdaptiveCompressionLevelToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableAdaptiveCompressionLevelToolStripMenuItem.Name = "enableAdaptiveCompressionLevelToolStripMenuItem";
            this.enableAdaptiveCompressionLevelToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.enableAdaptiveCompressionLevelToolStripMenuItem.Text = "Enable Adaptive Compression Level";
            this.enableAdaptiveCompressionLevelToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.enableAdaptiveCompressionLevelToolStripMenuItem_CheckStateChanged);
            // 
            // manuallySetCompressionLevelToolStripMenuItem
            // 
            this.manuallySetCompressionLevelToolStripMenuItem.Name = "manuallySetCompressionLevelToolStripMenuItem";
            this.manuallySetCompressionLevelToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.manuallySetCompressionLevelToolStripMenuItem.Text = "Manually Set Compression Level";
            this.manuallySetCompressionLevelToolStripMenuItem.Click += new System.EventHandler(this.manuallySetCompressionLevelToolStripMenuItem_Click);
            // 
            // box_MenuStrip
            // 
            this.box_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeRWVersionToolStripMenuItem,
            this.extractToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.replaceToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.box_MenuStrip.Name = "box_MenuStrip";
            this.box_MenuStrip.Size = new System.Drawing.Size(179, 114);
            // 
            // changeRWVersionToolStripMenuItem
            // 
            this.changeRWVersionToolStripMenuItem.Name = "changeRWVersionToolStripMenuItem";
            this.changeRWVersionToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.changeRWVersionToolStripMenuItem.Text = "Change RW Version";
            this.changeRWVersionToolStripMenuItem.Click += new System.EventHandler(this.changeRWVersionToolStripMenuItem_Click);
            // 
            // extractToolStripMenuItem
            // 
            this.extractToolStripMenuItem.Name = "extractToolStripMenuItem";
            this.extractToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.extractToolStripMenuItem.Text = "Extract File";
            this.extractToolStripMenuItem.Click += new System.EventHandler(this.extractToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.replaceToolStripMenuItem.Text = "Replace";
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // titleBar_StatusBar
            // 
            this.titleBar_StatusBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            this.titleBar_StatusBar.Controls.Add(this.titleBar_ItemCount);
            this.titleBar_StatusBar.Controls.Add(this.titleBar_RWVersion);
            this.titleBar_StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.titleBar_StatusBar.Location = new System.Drawing.Point(0, 492);
            this.titleBar_StatusBar.Name = "titleBar_StatusBar";
            this.titleBar_StatusBar.Size = new System.Drawing.Size(362, 30);
            this.titleBar_StatusBar.TabIndex = 16;
            // 
            // categoryBar_ExtractAll
            // 
            this.categoryBar_ExtractAll.ForeColor = System.Drawing.Color.Silver;
            this.categoryBar_ExtractAll.Name = "categoryBar_ExtractAll";
            this.categoryBar_ExtractAll.Size = new System.Drawing.Size(71, 20);
            this.categoryBar_ExtractAll.Text = "Extract All";
            this.categoryBar_ExtractAll.Click += new System.EventHandler(this.categoryBar_ExtractAll_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(362, 522);
            this.Controls.Add(this.box_FileList);
            this.Controls.Add(this.categoryBar_MenuStrip);
            this.Controls.Add(this.titleBar_Panel);
            this.Controls.Add(this.titleBar_StatusBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.categoryBar_MenuStrip;
            this.Name = "MainWindow";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.box_FileList)).EndInit();
            this.titleBar_Panel.ResumeLayout(false);
            this.categoryBar_MenuStrip.ResumeLayout(false);
            this.categoryBar_MenuStrip.PerformLayout();
            this.box_MenuStrip.ResumeLayout(false);
            this.titleBar_StatusBar.ResumeLayout(false);
            this.titleBar_StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Box_FileList_CustomDragDropEvent(object sender, DragEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Panel titleBar_Panel;
        private Reloaded_GUI.Styles.Controls.Animated.AnimatedButton titleBar_Title;
        private System.Windows.Forms.MenuStrip categoryBar_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem categoryBar_FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem categoryBar_OptionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private Controls.CustomDataGridView box_FileList;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn rwVersion;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem categoryBar_AddFiles;
        private System.Windows.Forms.ContextMenuStrip box_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeRWVersionToolStripMenuItem;
        private Reloaded_GUI.Styles.Controls.Animated.AnimatedButton categoryBar_Close;
        private System.Windows.Forms.Panel titleBar_StatusBar;
        private Reloaded_GUI.Styles.Controls.Animated.AnimatedButton titleBar_ItemCount;
        private Reloaded_GUI.Styles.Controls.Animated.AnimatedButton titleBar_RWVersion;
        private System.Windows.Forms.ToolStripMenuItem setArchiveRWVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAllFileRWVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compressionSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableAdaptiveCompressionLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manuallySetCompressionLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveShadow050ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveShadow060ToolStripMenuItem;
        private ToolStripMenuItem categoryBar_ExtractAll;
    }
}

