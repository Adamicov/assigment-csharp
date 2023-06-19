using System;
namespace better
{

	public class ProductDto {
		public string Name { get; set; }
		public int Amount { get; set; }
		public double Price { get; set; }
	}

	public class ClientWithProjectDto
	{
		public int OrderID { get; set; }

		public string clientsLastName { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? FullfiledAt { get; set; }

		public string Status { get; set; }

		public List<ProductDto> Products { get; set; }
	}
}

