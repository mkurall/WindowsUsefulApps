using BtkTodo.Db;
using BtkTodo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BtkTodo
{
    public partial class FrmEditDay : Form
    {
        TodoDbContext context;
        BindingList<DbTodoEntry> bList;
        DateTime date;
        public FrmEditDay(DateTime dt, TodoDbContext context)
        {
            InitializeComponent();

            this.context = context;

            date = dt;
            lblDay.Text = date.ToString("dd MMMM yyyy dddd");

            var list = context.Entries.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day).ToList();
            bList = new BindingList<DbTodoEntry>(list);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bList;

            colColor.DataSource = TodoColors.ColorList;
            colColor.DisplayMember = "Color";
            colColor.ValueMember = "Id";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            foreach (DbTodoEntry entry in bList)
            {
                if (entry.Id == 0)
                {
                    entry.Date = date;
                    context.Entries.Add(entry);
                }
            }

            context.SaveChanges();

            Close();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == colColor.Index)
            {
                ComboBox combo = e.Control as ComboBox;

                if (combo != null)
                {
                    combo.DrawMode = DrawMode.OwnerDrawVariable;
                    combo.DrawItem += Combo_DrawItem;
                }
            }

        }

        StringFormat sfLeft = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center,
        };

        private void Combo_DrawItem(object? sender, DrawItemEventArgs e)
        {
            TodoColorItem item = TodoColors.ColorList[e.Index];
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            e.Graphics.FillRectangle(new SolidBrush(item.Color), new Rectangle(e.Bounds.Left + 2, e.Bounds.Top + 2, e.Bounds.Height - 4, e.Bounds.Height - 4));
            e.Graphics.DrawString(item.Color.ToKnownColor().ToString(), dataGridView1.Font, Brushes.Black, new Rectangle(e.Bounds.Left + e.Bounds.Height, e.Bounds.Top, e.Bounds.Width - e.Bounds.Height, e.Bounds.Height), sfLeft);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == colDelete.Index)
            {
                DbTodoEntry entry = dataGridView1.Rows[e.RowIndex].DataBoundItem as DbTodoEntry;

                if(entry != null)
                {
                    var res = MessageBox.Show($"[{entry.Info}]  bu kaydı silmek istediğinize emin misiniz?",
                        "Uyarı", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if(res == DialogResult.Yes)
                    {
                        bList.Remove(entry);
                        context.Remove(entry);
                    }
                }
            }
        }
    }
}
