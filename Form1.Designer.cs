namespace EcgChart
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
        	this.components = new System.ComponentModel.Container();
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        	this.button1 = new System.Windows.Forms.Button();
        	this.listBox1 = new System.Windows.Forms.ListBox();
        	this.comboBox1 = new System.Windows.Forms.ComboBox();
        	this.button2 = new System.Windows.Forms.Button();
        	this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        	this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.fileOpen = new System.Windows.Forms.ToolStripMenuItem();
        	this.AddFromFile = new System.Windows.Forms.ToolStripMenuItem();
        	this.SaveFile = new System.Windows.Forms.ToolStripMenuItem();
        	this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
        	this.menuStrip1.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// button1
        	// 
        	this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.button1.Location = new System.Drawing.Point(12, 26);
        	this.button1.Name = "button1";
        	this.button1.Size = new System.Drawing.Size(75, 25);
        	this.button1.TabIndex = 1;
        	this.button1.Text = "Открыть";
        	this.button1.UseVisualStyleBackColor = true;
        	this.button1.Click += new System.EventHandler(this.button1_Click);
        	// 
        	// listBox1
        	// 
        	this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        	        	        	| System.Windows.Forms.AnchorStyles.Left)));
        	this.listBox1.FormattingEnabled = true;
        	this.listBox1.ItemHeight = 14;
        	this.listBox1.Location = new System.Drawing.Point(12, 62);
        	this.listBox1.Name = "listBox1";
        	this.listBox1.Size = new System.Drawing.Size(281, 270);
        	this.listBox1.TabIndex = 4;
        	// 
        	// comboBox1
        	// 
        	this.comboBox1.FormattingEnabled = true;
        	this.comboBox1.Location = new System.Drawing.Point(94, 26);
        	this.comboBox1.Name = "comboBox1";
        	this.comboBox1.Size = new System.Drawing.Size(150, 22);
        	this.comboBox1.TabIndex = 5;
        	this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1SelectedIndexChanged);
        	// 
        	// button2
        	// 
        	this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
        	this.button2.Location = new System.Drawing.Point(260, 27);
        	this.button2.Name = "button2";
        	this.button2.Size = new System.Drawing.Size(33, 31);
        	this.button2.TabIndex = 7;
        	this.button2.UseVisualStyleBackColor = true;
        	this.button2.Click += new System.EventHandler(this.Button2Click);
        	// 
        	// menuStrip1
        	// 
        	this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.файлToolStripMenuItem});
        	this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        	this.menuStrip1.Name = "menuStrip1";
        	this.menuStrip1.Size = new System.Drawing.Size(801, 24);
        	this.menuStrip1.TabIndex = 8;
        	this.menuStrip1.Text = "menuStrip1";
        	// 
        	// файлToolStripMenuItem
        	// 
        	this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.fileOpen,
        	        	        	this.AddFromFile,
        	        	        	this.SaveFile});
        	this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
        	this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
        	this.файлToolStripMenuItem.Text = "Файл";
        	// 
        	// fileOpen
        	// 
        	this.fileOpen.Name = "fileOpen";
        	this.fileOpen.Size = new System.Drawing.Size(132, 22);
        	this.fileOpen.Text = "Открыть";
        	this.fileOpen.Click += new System.EventHandler(this.fileOpenClick);
        	// 
        	// AddFromFile
        	// 
        	this.AddFromFile.Name = "AddFromFile";
        	this.AddFromFile.Size = new System.Drawing.Size(132, 22);
        	this.AddFromFile.Text = "Добавить";
        	this.AddFromFile.Click += new System.EventHandler(this.AddFromFileClick);
        	// 
        	// SaveFile
        	// 
        	this.SaveFile.Name = "SaveFile";
        	this.SaveFile.Size = new System.Drawing.Size(132, 22);
        	this.SaveFile.Text = "Сохранить";
        	this.SaveFile.Click += new System.EventHandler(this.SaveFileClick);
        	// 
        	// zedGraphControl1
        	// 
        	this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        	        	        	| System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.zedGraphControl1.Location = new System.Drawing.Point(299, 29);
        	this.zedGraphControl1.Name = "zedGraphControl1";
        	this.zedGraphControl1.ScrollGrace = 0D;
        	this.zedGraphControl1.ScrollMaxX = 0D;
        	this.zedGraphControl1.ScrollMaxY = 0D;
        	this.zedGraphControl1.ScrollMaxY2 = 0D;
        	this.zedGraphControl1.ScrollMinX = 0D;
        	this.zedGraphControl1.ScrollMinY = 0D;
        	this.zedGraphControl1.ScrollMinY2 = 0D;
        	this.zedGraphControl1.Size = new System.Drawing.Size(502, 322);
        	this.zedGraphControl1.TabIndex = 9;
        	// 
        	// Form1
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.BackColor = System.Drawing.Color.White;
        	this.ClientSize = new System.Drawing.Size(801, 352);
        	this.Controls.Add(this.zedGraphControl1);
        	this.Controls.Add(this.button2);
        	this.Controls.Add(this.comboBox1);
        	this.Controls.Add(this.listBox1);
        	this.Controls.Add(this.button1);
        	this.Controls.Add(this.menuStrip1);
        	this.MainMenuStrip = this.menuStrip1;
        	this.Name = "Form1";
        	this.Text = "ECG: BMD101";
        	this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
        	this.menuStrip1.ResumeLayout(false);
        	this.menuStrip1.PerformLayout();
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        private System.Windows.Forms.ToolStripMenuItem AddFromFile;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.ToolStripMenuItem SaveFile;
        private System.Windows.Forms.ToolStripMenuItem fileOpen;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;

        
       
    }
}

