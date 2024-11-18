namespace BtkTodo
{
    partial class FrmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            panel1 = new Panel();
            label4 = new Label();
            label3 = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            panel2 = new Panel();
            btnOk = new Button();
            panel3 = new Panel();
            colorDialog1 = new ColorDialog();
            label2 = new Label();
            chkRunAtStartup = new CheckBox();
            groupBox1 = new GroupBox();
            pnlPreview = new Panel();
            btnColor = new Button();
            chkAutoHide = new CheckBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(434, 66);
            panel1.TabIndex = 0;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 16F, FontStyle.Bold | FontStyle.Italic);
            label4.ForeColor = Color.FromArgb(192, 64, 0);
            label4.Image = Properties.Resources.calender32;
            label4.ImageAlign = ContentAlignment.MiddleLeft;
            label4.Location = new Point(249, 9);
            label4.Name = "label4";
            label4.Size = new Size(105, 34);
            label4.TabIndex = 3;
            label4.Text = "ToDo";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Italic);
            label3.ForeColor = SystemColors.ControlDarkDark;
            label3.Location = new Point(249, 43);
            label3.Name = "label3";
            label3.Size = new Size(179, 19);
            label3.TabIndex = 2;
            label3.Text = "BtkAkademi © 2024 İzmir";
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Left;
            pictureBox1.Image = Properties.Resources.settings128;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(56, 66);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            label1.Location = new Point(62, 9);
            label1.Name = "label1";
            label1.Size = new Size(128, 45);
            label1.TabIndex = 0;
            label1.Text = "Ayarlar";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ButtonHighlight;
            panel2.Controls.Add(btnOk);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 286);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(434, 61);
            panel2.TabIndex = 1;
            // 
            // btnOk
            // 
            btnOk.BackColor = Color.SkyBlue;
            btnOk.Font = new Font("Segoe UI", 12F);
            btnOk.Location = new Point(144, 12);
            btnOk.Margin = new Padding(3, 2, 3, 2);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(136, 40);
            btnOk.TabIndex = 0;
            btnOk.Text = "Tamam";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += btnOk_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Orange;
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 66);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(434, 3);
            panel3.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(12, 153);
            label2.Name = "label2";
            label2.Size = new Size(207, 21);
            label2.TabIndex = 4;
            label2.Text = "Yıl ve Ay İçin Arkaplan Rengi";
            // 
            // chkRunAtStartup
            // 
            chkRunAtStartup.AutoSize = true;
            chkRunAtStartup.Font = new Font("Segoe UI", 12F);
            chkRunAtStartup.Location = new Point(12, 86);
            chkRunAtStartup.Margin = new Padding(3, 2, 3, 2);
            chkRunAtStartup.Name = "chkRunAtStartup";
            chkRunAtStartup.Size = new Size(236, 25);
            chkRunAtStartup.TabIndex = 6;
            chkRunAtStartup.Text = "Uygulama başlangıçta çalışsın";
            chkRunAtStartup.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pnlPreview);
            groupBox1.Location = new Point(305, 79);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(123, 196);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Önizleme";
            // 
            // pnlPreview
            // 
            pnlPreview.Location = new Point(12, 22);
            pnlPreview.Name = "pnlPreview";
            pnlPreview.Size = new Size(100, 168);
            pnlPreview.TabIndex = 9;
            pnlPreview.Paint += pnlPreview_Paint;
            // 
            // btnColor
            // 
            btnColor.Location = new Point(225, 145);
            btnColor.Name = "btnColor";
            btnColor.Size = new Size(52, 41);
            btnColor.TabIndex = 8;
            btnColor.UseVisualStyleBackColor = true;
            btnColor.Click += btnColor_Click;
            // 
            // chkAutoHide
            // 
            chkAutoHide.AutoSize = true;
            chkAutoHide.Font = new Font("Segoe UI", 12F);
            chkAutoHide.Location = new Point(12, 115);
            chkAutoHide.Margin = new Padding(3, 2, 3, 2);
            chkAutoHide.Name = "chkAutoHide";
            chkAutoHide.Size = new Size(132, 25);
            chkAutoHide.TabIndex = 9;
            chkAutoHide.Text = "Otomatik Gizle";
            chkAutoHide.UseVisualStyleBackColor = true;
            // 
            // FrmSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 347);
            Controls.Add(chkAutoHide);
            Controls.Add(btnColor);
            Controls.Add(groupBox1);
            Controls.Add(chkRunAtStartup);
            Controls.Add(label2);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSettings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ayarlar";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Label label1;
        private Panel panel2;
        private Button btnOk;
        private Panel panel3;
        private ColorDialog colorDialog1;
        private Label label2;
        private CheckBox chkRunAtStartup;
        private GroupBox groupBox1;
        private Button btnColor;
        private Panel pnlPreview;
        private Label label4;
        private Label label3;
        private CheckBox chkAutoHide;
    }
}