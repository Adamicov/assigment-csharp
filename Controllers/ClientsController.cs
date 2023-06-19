using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using better.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using better.Entities.Dtos;



namespace better.Controllers
{
    [Route("api/clients")]
    public class ClientsController : Controller
    {
        private IClientService _clientService;
        public ClientsController(IClientService clientService) {
            this._clientService = clientService;
        }

 

        [HttpGet("{id}/orders")]
        public async Task<IActionResult> Get(int id)
        {
            if (!await this._clientService.CheckIfClientExists(id)) {
                return NotFound("Client with this id doesnt exist");
            }

            return Ok(await this._clientService.GetClientOrders(id));
        }

        [HttpPost("{clientId}/orders")]
        public async Task<IActionResult> Post(int clientId, [FromBody] CreateOrderDto dto)
        {
            if (dto.CreatedAt == null) {
                return BadRequest("createdAt field is required");
            }

            if (dto.Products == null || dto.Products.Count == 0) {
                return BadRequest("products must be non empty array");
            }

            if (!await this._clientService.CheckIfClientExists(clientId)) {
                return NotFound($"Client with {clientId} not found");
            }

            if (!await this._clientService.CheckIfEveryProductExists(dto.Products)) {
                return NotFound("Some products may not exists in database");
            }

            if (!await this._clientService.CheckIfStatusCreatedExist()) {
                return NotFound("Created status doesnt exist - server cant proceed");
            }

            await this._clientService.AddOrderForClient(dto, clientId);

            return Created("OK", "OK");
        }

     
    }
}

