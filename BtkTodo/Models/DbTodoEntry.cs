using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtkTodo.Models
{
    public class DbTodoEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Info { get; set; }
        public int? ColorIndex { get; set; }
    }
}
