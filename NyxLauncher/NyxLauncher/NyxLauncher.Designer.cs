
namespace NyxLauncher
{
    partial class NyxLauncher
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NyxLauncher));
            this.Character = new System.Windows.Forms.PictureBox();
            this.AppTitle = new System.Windows.Forms.Label();
            this.Play = new System.Windows.Forms.Button();
            this.ModList = new System.Windows.Forms.GroupBox();
            this.ModPanel = new System.Windows.Forms.Panel();
            this.ToggleBox = new System.Windows.Forms.Button();
            this.ModName = new System.Windows.Forms.Label();
            this.ScrollPanel = new System.Windows.Forms.Panel();
            this.PriUp = new System.Windows.Forms.Button();
            this.PriDown = new System.Windows.Forms.Button();
            this.PathButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Character)).BeginInit();
            this.ModList.SuspendLayout();
            this.ModPanel.SuspendLayout();
            this.ScrollPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Character
            // 
            this.Character.BackColor = System.Drawing.Color.Transparent;
            this.Character.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Character.Image = ((System.Drawing.Image)(resources.GetObject("Character.Image")));
            this.Character.Location = new System.Drawing.Point(505, 163);
            this.Character.Name = "Character";
            this.Character.Size = new System.Drawing.Size(390, 373);
            this.Character.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Character.TabIndex = 0;
            this.Character.TabStop = false;
            // 
            // AppTitle
            // 
            this.AppTitle.AutoSize = true;
            this.AppTitle.BackColor = System.Drawing.Color.Transparent;
            this.AppTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppTitle.ForeColor = System.Drawing.Color.HotPink;
            this.AppTitle.Location = new System.Drawing.Point(22, 20);
            this.AppTitle.Name = "AppTitle";
            this.AppTitle.Size = new System.Drawing.Size(363, 37);
            this.AppTitle.TabIndex = 3;
            this.AppTitle.Text = "LonaRPG Nyx Launcher";
            this.AppTitle.Click += new System.EventHandler(this.label1_Click);
            // 
            // Play
            // 
            this.Play.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Play.ForeColor = System.Drawing.SystemColors.Info;
            this.Play.Image = ((System.Drawing.Image)(resources.GetObject("Play.Image")));
            this.Play.Location = new System.Drawing.Point(29, 443);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(101, 59);
            this.Play.TabIndex = 5;
            this.Play.UseVisualStyleBackColor = false;
            this.Play.Click += new System.EventHandler(this.OnPlayGameExe);
            // 
            // ModList
            // 
            this.ModList.BackColor = System.Drawing.Color.Transparent;
            this.ModList.Controls.Add(this.ModPanel);
            this.ModList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ModList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ModList.ForeColor = System.Drawing.SystemColors.Info;
            this.ModList.Location = new System.Drawing.Point(29, 60);
            this.ModList.Name = "ModList";
            this.ModList.Size = new System.Drawing.Size(271, 364);
            this.ModList.TabIndex = 6;
            this.ModList.TabStop = false;
            this.ModList.Text = "Mod list:";
            this.ModList.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // ModPanel
            // 
            this.ModPanel.Controls.Add(this.ToggleBox);
            this.ModPanel.Controls.Add(this.ModName);
            this.ModPanel.Controls.Add(this.ScrollPanel);
            this.ModPanel.Location = new System.Drawing.Point(6, 25);
            this.ModPanel.Name = "ModPanel";
            this.ModPanel.Size = new System.Drawing.Size(259, 30);
            this.ModPanel.TabIndex = 7;
            this.ModPanel.Click += new System.EventHandler(this.OnLabelSelect);
            this.ModPanel.MouseEnter += new System.EventHandler(this.OnLabelMouseEnter);
            this.ModPanel.MouseLeave += new System.EventHandler(this.OnLabelMouseLeave);
            // 
            // ToggleBox
            // 
            this.ToggleBox.FlatAppearance.BorderColor = System.Drawing.SystemColors.Info;
            this.ToggleBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ToggleBox.Image = global::NyxLauncher.Properties.Resources.istoggle3;
            this.ToggleBox.Location = new System.Drawing.Point(3, 7);
            this.ToggleBox.Name = "ToggleBox";
            this.ToggleBox.Size = new System.Drawing.Size(16, 16);
            this.ToggleBox.TabIndex = 9;
            this.ToggleBox.Tag = true;
            this.ToggleBox.UseVisualStyleBackColor = true;
            this.ToggleBox.Click += new System.EventHandler(this.OnToggleBoxChanged);
            // 
            // ModName
            // 
            this.ModName.AutoSize = true;
            this.ModName.Location = new System.Drawing.Point(25, 5);
            this.ModName.MaximumSize = new System.Drawing.Size(170, 0);
            this.ModName.MinimumSize = new System.Drawing.Size(170, 0);
            this.ModName.Name = "ModName";
            this.ModName.Size = new System.Drawing.Size(170, 20);
            this.ModName.TabIndex = 8;
            this.ModName.Text = "MyRuby.rb";
            this.ModName.Click += new System.EventHandler(this.OnLabelTextSelect);
            // 
            // ScrollPanel
            // 
            this.ScrollPanel.BackColor = System.Drawing.Color.Transparent;
            this.ScrollPanel.Controls.Add(this.PriUp);
            this.ScrollPanel.Controls.Add(this.PriDown);
            this.ScrollPanel.Location = new System.Drawing.Point(208, 3);
            this.ScrollPanel.Name = "ScrollPanel";
            this.ScrollPanel.Size = new System.Drawing.Size(72, 24);
            this.ScrollPanel.TabIndex = 7;
            this.ScrollPanel.Visible = false;
            // 
            // PriUp
            // 
            this.PriUp.BackColor = System.Drawing.Color.Transparent;
            this.PriUp.FlatAppearance.BorderColor = System.Drawing.SystemColors.Info;
            this.PriUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.PriUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriUp.ForeColor = System.Drawing.SystemColors.Info;
            this.PriUp.Image = ((System.Drawing.Image)(resources.GetObject("PriUp.Image")));
            this.PriUp.Location = new System.Drawing.Point(25, 0);
            this.PriUp.Name = "PriUp";
            this.PriUp.Size = new System.Drawing.Size(24, 24);
            this.PriUp.TabIndex = 7;
            this.PriUp.UseVisualStyleBackColor = false;
            this.PriUp.Click += new System.EventHandler(this.button2_Click);
            // 
            // PriDown
            // 
            this.PriDown.BackColor = System.Drawing.Color.Transparent;
            this.PriDown.FlatAppearance.BorderColor = System.Drawing.SystemColors.Info;
            this.PriDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.PriDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriDown.ForeColor = System.Drawing.SystemColors.Info;
            this.PriDown.Image = ((System.Drawing.Image)(resources.GetObject("PriDown.Image")));
            this.PriDown.Location = new System.Drawing.Point(0, 0);
            this.PriDown.Name = "PriDown";
            this.PriDown.Size = new System.Drawing.Size(24, 24);
            this.PriDown.TabIndex = 8;
            this.PriDown.UseVisualStyleBackColor = false;
            this.PriDown.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // PathButton
            // 
            this.PathButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.PathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PathButton.ForeColor = System.Drawing.SystemColors.Info;
            this.PathButton.Location = new System.Drawing.Point(136, 478);
            this.PathButton.Name = "PathButton";
            this.PathButton.Size = new System.Drawing.Size(287, 24);
            this.PathButton.TabIndex = 10;
            this.PathButton.Text = "Press this text to set path";
            this.PathButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PathButton.UseVisualStyleBackColor = false;
            this.PathButton.Click += new System.EventHandler(this.OnChangeFolder);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(136, 455);
            this.label1.MaximumSize = new System.Drawing.Size(170, 0);
            this.label1.MinimumSize = new System.Drawing.Size(170, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Game path:";
            // 
            // NyxLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(890, 532);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PathButton);
            this.Controls.Add(this.ModList);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.AppTitle);
            this.Controls.Add(this.Character);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NyxLauncher";
            this.Text = "Nyx Launcher";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Load += new System.EventHandler(this.NyxLauncher_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Character)).EndInit();
            this.ModList.ResumeLayout(false);
            this.ModPanel.ResumeLayout(false);
            this.ModPanel.PerformLayout();
            this.ScrollPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Character;
        private System.Windows.Forms.Label AppTitle;
        private System.Windows.Forms.Button Play;
        public System.Windows.Forms.GroupBox ModList;
        private System.Windows.Forms.Button PriUp;
        private System.Windows.Forms.Button PriDown;
        private System.Windows.Forms.Panel ScrollPanel;
        private System.Windows.Forms.Panel ModPanel;
        private System.Windows.Forms.Button ToggleBox;
        private System.Windows.Forms.Label ModName;
        private System.Windows.Forms.Button PathButton;
        private System.Windows.Forms.Label label1;
    }
}

