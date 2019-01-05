namespace HeroesONE_R_GUI.Dialogs
{
    partial class RenameDialog
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
            Reloaded_GUI.Styles.Animation.AnimProperties animProperties1 = new Reloaded_GUI.Styles.Animation.AnimProperties();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage1 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage2 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimProperties animProperties2 = new Reloaded_GUI.Styles.Animation.AnimProperties();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage3 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage4 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimProperties animProperties3 = new Reloaded_GUI.Styles.Animation.AnimProperties();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage5 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage6 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            this.titleBar_Title = new Reloaded_GUI.Styles.Controls.Animated.AnimatedButton();
            this.borderless_FileName = new Reloaded_GUI.Styles.Controls.Animated.AnimatedTextbox();
            this.item_Close = new Reloaded_GUI.Styles.Controls.Animated.AnimatedButton();
            this.titleBar_Panel = new System.Windows.Forms.Panel();
            this.titleBar_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleBar_Title
            // 
            animMessage1.Control = this.titleBar_Title;
            animMessage1.PlayAnimation = true;
            animProperties1.BackColorMessage = animMessage1;
            animMessage2.Control = this.titleBar_Title;
            animMessage2.PlayAnimation = true;
            animProperties1.ForeColorMessage = animMessage2;
            animProperties1.MouseEnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            animProperties1.MouseEnterDuration = 200F;
            animProperties1.MouseEnterForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(163)))), ((int)(((byte)(244)))));
            animProperties1.MouseEnterFramerate = 144F;
            animProperties1.MouseEnterOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseEnterOverride.None;
            animProperties1.MouseLeaveBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            animProperties1.MouseLeaveDuration = 200F;
            animProperties1.MouseLeaveForeColor = System.Drawing.Color.White;
            animProperties1.MouseLeaveFramerate = 144F;
            animProperties1.MouseLeaveOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseLeaveOverride.None;
            this.titleBar_Title.AnimProperties = animProperties1;
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
            this.titleBar_Title.Size = new System.Drawing.Size(373, 42);
            this.titleBar_Title.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.titleBar_Title.TabIndex = 9999;
            this.titleBar_Title.Text = "Rename File";
            this.titleBar_Title.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.titleBar_Title.UseVisualStyleBackColor = true;
            // 
            // borderless_FileName
            // 
            animMessage3.Control = this.borderless_FileName;
            animMessage3.PlayAnimation = true;
            animProperties2.BackColorMessage = animMessage3;
            animMessage4.Control = this.borderless_FileName;
            animMessage4.PlayAnimation = true;
            animProperties2.ForeColorMessage = animMessage4;
            animProperties2.MouseEnterBackColor = System.Drawing.Color.Empty;
            animProperties2.MouseEnterDuration = 0F;
            animProperties2.MouseEnterForeColor = System.Drawing.Color.Empty;
            animProperties2.MouseEnterFramerate = 0F;
            animProperties2.MouseEnterOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseEnterOverride.None;
            animProperties2.MouseLeaveBackColor = System.Drawing.Color.Empty;
            animProperties2.MouseLeaveDuration = 0F;
            animProperties2.MouseLeaveForeColor = System.Drawing.Color.Empty;
            animProperties2.MouseLeaveFramerate = 0F;
            animProperties2.MouseLeaveOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseLeaveOverride.None;
            this.borderless_FileName.AnimProperties = animProperties2;
            this.borderless_FileName.BackColor = System.Drawing.Color.Gray;
            this.borderless_FileName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.borderless_FileName.BottomBorderColour = System.Drawing.SystemColors.WindowText;
            this.borderless_FileName.BottomBorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.borderless_FileName.BottomBorderWidth = 55;
            this.borderless_FileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.borderless_FileName.LeftBorderColour = System.Drawing.SystemColors.WindowText;
            this.borderless_FileName.LeftBorderStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.borderless_FileName.LeftBorderWidth = 0;
            this.borderless_FileName.Location = new System.Drawing.Point(54, 75);
            this.borderless_FileName.Name = "borderless_FileName";
            this.borderless_FileName.RightBorderColour = System.Drawing.SystemColors.WindowText;
            this.borderless_FileName.RightBorderStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.borderless_FileName.RightBorderWidth = 0;
            this.borderless_FileName.Size = new System.Drawing.Size(264, 23);
            this.borderless_FileName.TabIndex = 5;
            this.borderless_FileName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.borderless_FileName.TopBorderColour = System.Drawing.SystemColors.WindowText;
            this.borderless_FileName.TopBorderStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.borderless_FileName.TopBorderWidth = 0;
            this.borderless_FileName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.borderless_FileName_KeyDown);
            // 
            // item_Close
            // 
            animMessage5.Control = this.item_Close;
            animMessage5.PlayAnimation = true;
            animProperties3.BackColorMessage = animMessage5;
            animMessage6.Control = this.item_Close;
            animMessage6.PlayAnimation = true;
            animProperties3.ForeColorMessage = animMessage6;
            animProperties3.MouseEnterBackColor = System.Drawing.Color.Empty;
            animProperties3.MouseEnterDuration = 0F;
            animProperties3.MouseEnterForeColor = System.Drawing.Color.Empty;
            animProperties3.MouseEnterFramerate = 0F;
            animProperties3.MouseEnterOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseEnterOverride.None;
            animProperties3.MouseLeaveBackColor = System.Drawing.Color.Empty;
            animProperties3.MouseLeaveDuration = 0F;
            animProperties3.MouseLeaveForeColor = System.Drawing.Color.Empty;
            animProperties3.MouseLeaveFramerate = 0F;
            animProperties3.MouseLeaveOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseLeaveOverride.None;
            this.item_Close.AnimProperties = animProperties3;
            this.item_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            this.item_Close.CaptureChildren = true;
            this.item_Close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.item_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.item_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.item_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.item_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.item_Close.ForeColor = System.Drawing.Color.White;
            this.item_Close.IgnoreMouse = false;
            this.item_Close.IgnoreMouseClicks = false;
            this.item_Close.Location = new System.Drawing.Point(146, 133);
            this.item_Close.Name = "item_Close";
            this.item_Close.Size = new System.Drawing.Size(81, 32);
            this.item_Close.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.item_Close.TabIndex = 6;
            this.item_Close.Text = "Ok";
            this.item_Close.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.item_Close.UseVisualStyleBackColor = false;
            this.item_Close.Click += new System.EventHandler(this.item_OpenConfigDirectory_Click);
            // 
            // titleBar_Panel
            // 
            this.titleBar_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            this.titleBar_Panel.Controls.Add(this.titleBar_Title);
            this.titleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar_Panel.Location = new System.Drawing.Point(0, 0);
            this.titleBar_Panel.Name = "titleBar_Panel";
            this.titleBar_Panel.Size = new System.Drawing.Size(373, 42);
            this.titleBar_Panel.TabIndex = 9999;
            // 
            // RenameDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(373, 192);
            this.Controls.Add(this.item_Close);
            this.Controls.Add(this.borderless_FileName);
            this.Controls.Add(this.titleBar_Panel);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RenameDialog";
            this.Text = "RWVersionDialog";
            this.titleBar_Panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel titleBar_Panel;
        private Reloaded_GUI.Styles.Controls.Animated.AnimatedButton titleBar_Title;
        private Reloaded_GUI.Styles.Controls.Animated.AnimatedTextbox borderless_FileName;
        private Reloaded_GUI.Styles.Controls.Animated.AnimatedButton item_Close;
    }
}