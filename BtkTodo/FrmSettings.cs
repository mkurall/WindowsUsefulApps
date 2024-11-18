using BtkTodo.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BtkTodo
{
    public partial class FrmSettings : Form
    {
        DateTime dt = DateTime.Now;
        Font fntYear = new Font("Arial", 22);
        Font fntMonth = new Font("Arial", 18);
        Font fntDay = new Font("Arial", 14);
        Font fntCaption = new Font("Arial", 11);

        StringFormat sfCenter = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
        };

        public FrmSettings()
        {
            InitializeComponent();

            btnColor.BackColor = SettingsManager.HeaderColor;
            chkAutoHide.Checked = SettingsManager.AutoHide;
            chkRunAtStartup.Checked = SettingsManager.RunAtStartup;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SettingsManager.HeaderColor = btnColor.BackColor;
            SettingsManager.AutoHide = chkAutoHide.Checked;
            SettingsManager.RunAtStartup = chkRunAtStartup.Checked;

            SettingsManager.Save();
            DialogResult = DialogResult.OK;
        }
        private void pnlPreview_Paint(object sender, PaintEventArgs e)
        {
            Rectangle bounds = pnlPreview.Bounds;
            Color backColor = pnlPreview.BackColor;

            Rectangle rectCaption = new Rectangle(1, 1, bounds.Width - 2, 25);
            Rectangle rectYear = new Rectangle(1, rectCaption.Bottom, bounds.Width - 2, (int)fntYear.GetHeight(e.Graphics));
            Rectangle rectMonth = new Rectangle(1, rectYear.Bottom - 10, bounds.Width - 2, (int)fntYear.GetHeight(e.Graphics));
            Rectangle rectDays = new Rectangle(1, rectMonth.Bottom + 20, bounds.Width - 2, bounds.Height - rectMonth.Bottom);


            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;


            e.Graphics.FillRectangle(new SolidBrush(backColor), bounds);
            e.Graphics.DrawRectangle(Pens.Gray, new Rectangle(0, 0, bounds.Width - 1, bounds.Height - 1));



            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(236, 112, 99)), new Rectangle(1, 1, bounds.Width - 2, 25));
            e.Graphics.DrawImage(Properties.Resources.calendar, new Rectangle(2, 2, 21, 21), new Rectangle(0, 0, 24, 24), GraphicsUnit.Pixel);

            e.Graphics.DrawString("ToDo", fntCaption, Brushes.White, new Rectangle(1, 1, bounds.Width - 2, 25), sfCenter);

            Rectangle rectCloseButton = new Rectangle(bounds.Width - 20, 4, 20, 20);

            e.Graphics.DrawImage(Properties.Resources.closeIt, rectCloseButton, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);

            e.Graphics.FillRectangle(new SolidBrush(btnColor.BackColor), rectYear);
            e.Graphics.FillRectangle(new SolidBrush(btnColor.BackColor), rectMonth);

            e.Graphics.FillRectangle(new SolidBrush(btnColor.BackColor), new Rectangle(rectMonth.Left, rectMonth.Bottom - 1, rectMonth.Width, 16));

            Rectangle rectLeftButton = new Rectangle(2, rectMonth.Bottom - 3, 16, 16);
            e.Graphics.DrawImage(Properties.Resources.left_arrow, rectLeftButton, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);

            Rectangle rectRightButton = new Rectangle(rectMonth.Width - 16, rectMonth.Bottom - 3, 16, 16);
            e.Graphics.DrawImage(Properties.Resources.right_arrow, rectRightButton, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);

            Rectangle rectSettingsButton = new Rectangle((bounds.Width - 16) / 2, rectMonth.Bottom - 3, 16, 16);
            e.Graphics.DrawImage(Properties.Resources.settings16, rectSettingsButton, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);


            e.Graphics.FillRectangle(Brushes.WhiteSmoke, rectDays);


            e.Graphics.DrawString(dt.ToString("yyyy"), fntYear, Brushes.White, rectYear, sfCenter);
            e.Graphics.DrawString(dt.ToString("MMMM"), fntMonth, Brushes.White, rectMonth, sfCenter);

            int totalDays = 3;
            int daysRectHeight = rectDays.Height / totalDays;

            for (int d = 1; d <= totalDays; d++)
            {
                Rectangle rectDay =
                    new Rectangle(1, rectDays.Top + (d - 1) * daysRectHeight, bounds.Width - 1, daysRectHeight);


                if (d == DateTime.Now.Day && dt.Month == DateTime.Now.Month && dt.Year == DateTime.Now.Year)

                    e.Graphics.DrawString(d.ToString(), new Font(fntDay, FontStyle.Bold),
                        Brushes.Tomato,
                        rectDay, sfCenter);
                else
                    e.Graphics.DrawString(d.ToString(), fntDay,
                        Brushes.Black,
                        rectDay, sfCenter);

                if (d != totalDays)
                    e.Graphics.DrawLine(Pens.Gray, rectDay.Left + 3, rectDay.Bottom, rectDay.Right - 3, rectDay.Bottom);
            }

        }
        private void btnColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = btnColor.BackColor;

            if (DialogResult.OK == colorDialog1.ShowDialog())
            {
                btnColor.BackColor = colorDialog1.Color;

                pnlPreview.Invalidate();
            }
        }
    }
}
