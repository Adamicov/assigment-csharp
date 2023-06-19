using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace better.Entities
{
	[Table("Client")]
	public class Client
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(50)]
		public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }
	}
}

