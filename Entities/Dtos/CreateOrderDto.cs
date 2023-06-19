using System;
namespace better.Entities.Dtos
{
	public class CreateOrderProductDto {
		public int Id { get; set; }

		public int Amount { get; set; }
	}

	public class CreateOrderDto
	{
		public DateTime CreatedAt { get; set; }

		public DateTime? FullfiledAt { get; set; }

		public List<CreateOrderProductDto> Products { get; set; } 

	}
}

