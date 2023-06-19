using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace better.Entities
{
	[Table("Order")]
	public class Order
	{
		[Key]
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? FullfiledAt { get; set; }

		[ForeignKey("Client"), Column("ClientID")]
		public int ClientId { get; set; }

		[ForeignKey("Status"), Column("StatusID")]
		public int StatusId { get; set; }

		public virtual Client Client { get; set; }
		public virtual Status Status { get; set; }
	}
}

