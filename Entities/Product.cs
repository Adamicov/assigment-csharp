using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace better.Entities
{
	[Table("Product")]
	public class Product
	{
		[Key, Column("ID")]
		public int Id { get; set; }

		[MaxLength(50)]
		public string Name { get; set; }

		public double Price { get; set; }		
	}
}

