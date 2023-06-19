using System;
using System.Transactions;
using better.Entities;
using better.Entities.Dtos;
using kolos2.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace better.Services
{
	public interface IClientService {
		public Task<bool> CheckIfClientExists(int clientId);
		public Task<List<ClientWithProjectDto>> GetClientOrders(int clientId);
        public Task AddOrderForClient(CreateOrderDto createOrderDto, int clientId);
        public Task<bool> CheckIfEveryProductExists(List<CreateOrderProductDto> products);
        public Task<bool> CheckIfStatusCreatedExist();

    }

	public class ClientService : IClientService
	{
        private AppDbContext _dbContext;
        private readonly string _createdStatusName = "Created";
		public ClientService(AppDbContext appDbContext)
		{
            this._dbContext = appDbContext;
		}

        public async Task<bool> CheckIfClientExists(int clientId)
        {
            return await this._dbContext.Clients.AnyAsync(c => c.Id == clientId);
        }

        public async Task AddOrderForClient(CreateOrderDto createOrderDto,int clientId) {
            int? statusCreatedId = await this.GetStatusCreatedId();

            if (createOrderDto == null || clientId == null) {
                throw new ArgumentNullException("dto data and clientId must be satisfied");
            }

            if (!await this.CheckIfClientExists(clientId)) {
                throw new Exception("Client not present in database");
            }

            if (statusCreatedId == null) {
                throw new Exception("Status doesnt exist");
            }

            if (!await this.CheckIfEveryProductExists(createOrderDto.Products)) {
                throw new Exception("Products not in database");
            }


            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Order order = new Order { ClientId = clientId, CreatedAt = createOrderDto.CreatedAt, FullfiledAt = createOrderDto.FullfiledAt, StatusId = (int)statusCreatedId };

                await this._dbContext.Orders.AddAsync(order);
                await this._dbContext.SaveChangesAsync();


                var productOrders = createOrderDto.Products.Select(p => new ProductOrder
                {
                    Amount = p.Amount,
                    OrderId = order.Id,
                    ProductId = p.Id
                }
                ).ToList();


                await this._dbContext.ProductOrders.AddRangeAsync(productOrders);
                await this._dbContext.SaveChangesAsync();
                scope.Complete();
            }
        }

        public async Task<List<ClientWithProjectDto>> GetClientOrders(int clientId)
        {
            if (!await this.CheckIfClientExists(clientId)) {
                throw new ArgumentException("Client with this id doesnt exist");
            }


            List<Order> clientOrders = await this._dbContext.Orders.Where(o => o.ClientId == clientId).Include(o => o.Client).Include(o => o.Status).ToListAsync();
            List<ProductOrder> clientProductOrders = await this._dbContext.ProductOrders.Include(p => p.Product).
                ToListAsync();

            List<ClientWithProjectDto> clientWithProjectDtos = clientOrders.Select(order =>
            {
                List<ProductDto> productDtos = clientProductOrders.
                    Where(productOrder => productOrder.OrderId == order.Id).
                    Select(productOrder => new ProductDto { Amount = productOrder.Amount,
                        Name = productOrder.Product.Name,
                        Price = Math.Round(productOrder.Product.Price * productOrder.Amount, 2, MidpointRounding.AwayFromZero) }).
                    ToList();

                return new ClientWithProjectDto
                {
                    clientsLastName = order.Client.LastName,
                    OrderID = order.Id,
                    CreatedAt = order.CreatedAt,
                    FullfiledAt = order.FullfiledAt,
                    Status = order.Status.Name,
                    Products = productDtos
                };
            }).ToList();

            return clientWithProjectDtos; 
        }

        public async Task<bool> CheckIfStatusCreatedExist() {
            return await this._dbContext.Statuses.AnyAsync(s => s.Name == this._createdStatusName);
        }

        public async Task<bool> CheckIfEveryProductExists(List<CreateOrderProductDto> products)
        {
            List<int> requestedProductsIds = products.Select(s => s.Id).ToList();
            List<int> availableProductsIds = await this._dbContext.Products.Select(p => p.Id).ToListAsync();
            return requestedProductsIds.All(id => availableProductsIds.Contains(id));
        }

        private async Task<int?> GetStatusCreatedId()
        {
            var status = await this._dbContext.Statuses.Where(s => s.Name == this._createdStatusName).Select(s => s.Id).FirstOrDefaultAsync();
            return status;
        }
    }
}

