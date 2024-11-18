using BtkTodo.Db;
using BtkTodo.Models;
using BtkTodo.Utils;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Drawing2D;
using System.Text;

namespace BtkTodo
{
    public partial class Form1 : Form
    {
        DateTime dt = DateTime.Now;
        Font fntYear = new Font("Arial", 22);
        Font fntMonth = new Font("Arial", 18);
        Font fntDay = new Font("Arial", 14);
        Font fntCaption = new Font("Arial", 11);
        Font fntTooltipItem = new Font("Arial", 11);
        Font fntTooltipCaption = new Font("Arial", 12, FontStyle.Bold);

        StringFormat sfCenter = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
        };
        
        StringFormat sfLeft = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center,
        };

        Rectangle rectDays;
        Rectangle rectCloseButton;
        Rectangle rectLeftButton;
        Rectangle rectRightButton;
        Rectangle rectSettingsButton;

        int hoverDay = 0;
        bool hoverClose = false;
        bool hoverLeft = false;
        bool hoverRight = false;
        bool hoverSettings = false;

        List<DbTodoEntry> entries;
        TodoDbContext context = new TodoDbContext();

        public Form1()
        {
            InitializeComponent();

            this.SetStyle(
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.UserPaint |
            ControlStyles.DoubleBuffer,
            true);

            //veritabaný yok ise oluþtur
            context.Database.EnsureCreated();
 
            RefreshEntries();
        }

        //Burasý app bar kayýt için baþ
        private int uCallBack;
        Win32Api.ABEdge edge;
        bool isAppBarRegistered = false;
        int BarSize = 100;
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == uCallBack)
            {
                switch (m.WParam.ToInt32())
                {
                    case (int)Win32Api.ABNotify.ABN_POSCHANGED:
                        Win32Api.ABSetPos(this.Handle, edge, BarSize);
                        break;
                }
            }

            base.WndProc(ref m);
        }

        protected override void OnLoad(EventArgs e)
        {
            uCallBack = Win32Api.RegisterAppBar(this.Handle);
            edge = Win32Api.ABEdge.ABE_RIGHT;

            Win32Api.ABSetPos(this.Handle, edge, BarSize);

            isAppBarRegistered = true;

            if (SettingsManager.AutoHide)
                timerAutoHide.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Win32Api.UnregisterAppBar(this.Handle);
        }
        //burasý appbar son

        void RefreshEntries()
        {
            entries = context.Entries.ToList();
            Invalidate();
        }

        protected override void OnShown(EventArgs e)
        {
            TopMost = true;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rectCaption = new Rectangle(1, 1, Bounds.Width - 2, 25);
            Rectangle rectYear = new Rectangle(1, rectCaption.Bottom, Bounds.Width - 2, (int)fntYear.GetHeight(e.Graphics));
            Rectangle rectMonth = new Rectangle(1, rectYear.Bottom - 10, Bounds.Width - 2, (int)fntYear.GetHeight(e.Graphics));
            rectDays = new Rectangle(1, rectMonth.Bottom + 20, Bounds.Width - 2, Bounds.Height - rectMonth.Bottom);


            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;


            e.Graphics.FillRectangle(new SolidBrush(BackColor), Bounds);
            e.Graphics.DrawRectangle(Pens.Gray, new Rectangle(0, 0, Bounds.Width - 1, Bounds.Height - 1));



            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(236, 112, 99)), new Rectangle(1, 1, Bounds.Width - 2, 25));
            e.Graphics.DrawImage(Properties.Resources.calendar, new Rectangle(2, 2, 21, 21), new Rectangle(0, 0, 24, 24), GraphicsUnit.Pixel);

            e.Graphics.DrawString("ToDo", fntCaption, Brushes.White, new Rectangle(1, 1, Bounds.Width - 2, 25), sfCenter);

            rectCloseButton = new Rectangle(Bounds.Width - 20, 4, 20, 20);

            e.Graphics.DrawImage(Properties.Resources.closeIt, rectCloseButton, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);

            if (hoverClose)
                e.Graphics.DrawRectangle(Pens.White, rectCloseButton);

            e.Graphics.FillRectangle(new SolidBrush(SettingsManager.HeaderColor), rectYear);
            e.Graphics.FillRectangle(new SolidBrush(SettingsManager.HeaderColor), rectMonth);

            e.Graphics.FillRectangle(new SolidBrush(SettingsManager.HeaderColor), new Rectangle(rectMonth.Left, rectMonth.Bottom - 1, rectMonth.Width, 16));

            rectLeftButton = new Rectangle(2, rectMonth.Bottom - 3, 16, 16);
            e.Graphics.DrawImage(Properties.Resources.left_arrow, rectLeftButton, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);

            if (hoverLeft)
                e.Graphics.DrawRectangle(Pens.White, rectLeftButton);

            rectRightButton = new Rectangle(rectMonth.Width - 16, rectMonth.Bottom - 3, 16, 16);
            e.Graphics.DrawImage(Properties.Resources.right_arrow, rectRightButton, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);

            if (hoverRight)
                e.Graphics.DrawRectangle(Pens.White, rectRightButton);

            rectSettingsButton = new Rectangle((Bounds.Width - 16) / 2, rectMonth.Bottom - 3, 16, 16);
            e.Graphics.DrawImage(Properties.Resources.settings16, rectSettingsButton, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);


            if (hoverSettings)
                e.Graphics.DrawRectangle(Pens.White, rectSettingsButton);


            e.Graphics.FillRectangle(Brushes.WhiteSmoke, rectDays);



            e.Graphics.DrawString(dt.ToString("yyyy"), fntYear, Brushes.White, rectYear, sfCenter);
            e.Graphics.DrawString(dt.ToString("MMMM"), fntMonth, Brushes.White, rectMonth, sfCenter);

            int totalDays = DateTime.DaysInMonth(dt.Year, dt.Month);
            int daysRectHeight = rectDays.Height / totalDays;

            for (int d = 1; d <= totalDays; d++)
            {
                Rectangle rectDay =
                    new Rectangle(1, rectDays.Top + (d - 1) * daysRectHeight, Bounds.Width - 1, daysRectHeight);


                if (hoverDay == d)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(250, 219, 216)), rectDay);
                }

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


                List<DbTodoEntry> list = entries.Where(x => x.Date.Year == dt.Year && x.Date.Month == dt.Month && x.Date.Day == d).ToList();

                if (list.Count > 0)
                {
                    for (int k = 0; k < list.Count; k++)
                    {
                        if (k >= 4) break;

                        DbTodoEntry first = list[k];
                        Size dotSize = new Size(12, 12);
                        e.Graphics.FillEllipse(new SolidBrush(TodoColors.ColorList[first.ColorIndex.GetValueOrDefault()].Color),
                            new Rectangle(rectDay.Left + 3 + k * (int)(dotSize.Width * 0.7), rectDay.Top + (rectDay.Height - dotSize.Height) / 2, dotSize.Width, dotSize.Height));

                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            int totalDays = DateTime.DaysInMonth(dt.Year, dt.Month);
            int daysRectHeight = rectDays.Height / totalDays;

            if (rectCloseButton.Contains(e.Location))
            {
                hoverClose = true;
                Invalidate();
                return;
            }
            else if (rectLeftButton.Contains(e.Location))
            {
                hoverLeft = true;
                Invalidate();
                return;
            }
            else if (rectRightButton.Contains(e.Location))
            {
                hoverRight = true;
                Invalidate();
                return;
            }
            else if (rectSettingsButton.Contains(e.Location))//fare imleci içindeyse
            {
                hoverSettings = true;
                Invalidate();
                return;
            }

            if (hoverClose)
            {
                hoverClose = false;
                Invalidate();
            }

            if (hoverLeft)
            {
                hoverLeft = false;
                Invalidate();
            }

            if (hoverRight)
            {
                hoverRight = false;
                Invalidate();
            }

            if (hoverSettings)
            {
                hoverSettings = false;
                Invalidate();
            }

            for (int d = 1; d <= totalDays; d++)
            {
                Rectangle rectDay =
                    new Rectangle(1, rectDays.Top + (d - 1) * daysRectHeight, Bounds.Width - 1, daysRectHeight);

                if (rectDay.Contains(e.Location))
                {
                    if (hoverDay != d)
                    {
                        hoverDay = d;


                        ShowHoverDayInfo(rectDay.Location);

                        Invalidate();

                    }

                    break;

                }
            }

        }

        private void ShowHoverDayInfo(Point pt)
        {
            List<DbTodoEntry> list = entries.Where(x => x.Date.Year == dt.Year && x.Date.Month == dt.Month && x.Date.Day == hoverDay).ToList();

            if (list.Count > 0)
            {
                string str = "";

                foreach (DbTodoEntry entry in list)
                {
                    str += entry.Info + " \n";

                }
                //toolTip1.ToolTipIcon = ToolTipIcon.Info;
                toolTip1.ToolTipTitle = $"{hoverDay} {dt.ToString("MMMM yyyy")}";

                toolTip1.Tag = list;

                toolTip1.Show(str, this, pt.X - 200, pt.Y, 8000);

            }
            else
            {
                toolTip1.Hide(this);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            BarSize = 100;

            Win32Api.ABSetPos(this.Handle, edge, BarSize);
            timerAutoHide.Stop();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            toolTip1.Hide(this);

            hoverDay = 0;
            Invalidate();

            if(SettingsManager.AutoHide)
                timerAutoHide.Start();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            int totalDays = DateTime.DaysInMonth(dt.Year, dt.Month);
            int daysRectHeight = rectDays.Height / totalDays;

            for (int d = 1; d <= totalDays; d++)
            {
                Rectangle rectDay =
                    new Rectangle(1, rectDays.Top + (d - 1) * daysRectHeight, Bounds.Width - 1, daysRectHeight);



                if (rectDay.Contains(e.Location))
                {

                    FrmEditDay form = new FrmEditDay(new DateTime(dt.Year, dt.Month, d), context);
                    form.ShowDialog();

                    RefreshEntries();


                    break;

                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (rectCloseButton.Contains(e.Location) && hoverClose)
            {
                Close();
            }
            else if (rectLeftButton.Contains(e.Location))
            {
                dt = dt.AddMonths(-1);
                RefreshEntries();
            }
            else if (rectRightButton.Contains(e.Location))
            {
                dt = dt.AddMonths(1);
                RefreshEntries();
            }
            else if (rectSettingsButton.Contains(e.Location))
            {
                FrmSettings form = new FrmSettings();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Invalidate();
                }
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            e.ToolTipSize = new Size(200, 200);
        }

        private void timerAutoHide_Tick(object sender, EventArgs e)
        {
            BarSize = 2;

            Win32Api.ABSetPos(this.Handle, edge, BarSize);

            timerAutoHide.Stop();
        }

        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.Bounds);
            e.DrawBorder();

            e.Graphics.FillRectangle(new SolidBrush(SettingsManager.HeaderColor), new Rectangle(1,1,e.Bounds.Width-1,30));
            e.Graphics.DrawImage(Properties.Resources.calendar, new Rectangle(2, 5, 20, 20), new Rectangle(0,0,24,24),GraphicsUnit.Pixel);  
            e.Graphics.DrawString(toolTip1.ToolTipTitle, fntTooltipCaption, Brushes.White, new Rectangle(20, 5, e.Bounds.Width-5, 25), sfLeft);

            List<DbTodoEntry> list = toolTip1.Tag as List<DbTodoEntry>;
            
            int itemHeight = (int)fntTooltipItem.GetHeight(e.Graphics) + 10;

            if (list != null) {

                int yPos = 40;

                foreach (DbTodoEntry entry in list) {

                    e.Graphics.DrawString(entry.Info, fntTooltipItem, Brushes.Black, new Rectangle(30, yPos, e.Bounds.Width-33, itemHeight), sfLeft);

                    Size dotSize = new Size(12, 12);
                   
                    e.Graphics.FillEllipse(new SolidBrush(TodoColors.ColorList[entry.ColorIndex.GetValueOrDefault()].Color),
                        new Rectangle(10 , yPos + (itemHeight  - dotSize.Height)/2, dotSize.Width, dotSize.Height));

                    yPos += itemHeight;
                }
            }
        }
    }
}
