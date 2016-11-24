using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Models
{
    public class OrderDetails : BaseModel<int>
    {
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public int GameId { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public virtual Game Game { get; set; }
    }
}
