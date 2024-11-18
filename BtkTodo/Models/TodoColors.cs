using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtkTodo.Models
{
    [NotMapped]
    public static class TodoColors
    {
        public static TodoColorItem[] ColorList =
        {
            new TodoColorItem(){ Id = 0, Color = Color.Black },
            new TodoColorItem(){ Id = 1, Color = Color.Red },
             new TodoColorItem(){ Id = 2, Color = Color.Green },
             new TodoColorItem(){ Id = 3, Color = Color.Blue },
        };
    }

    public class TodoColorItem
    {
        public int Id { get; set; }
        public Color Color { get; set; }
    }

}
