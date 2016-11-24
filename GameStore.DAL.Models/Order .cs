using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Models
{
    public class Order : BaseModel<int>
    {
        public int? CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
