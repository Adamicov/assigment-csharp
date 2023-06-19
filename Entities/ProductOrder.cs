using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace better.Entities
{
	[Table("Product_Order"), PrimaryKey("ProductId", "OrderId")]
	public class ProductOrder
	{
		[ForeignKey("Product"), Column("ProductID")]
		public int ProductId { get; set; }


        [ForeignKey("OrderId"), Column("OrderID")]
        public int OrderId { get; set; }

		public int Amount { get; set; }

        public virtual Product Product { get; set; }
		public virtual Order Order { get; set; }
	}
}

