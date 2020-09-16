using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerManagement.Models;
using AutoMapper;
using CustomerManagement.Entity;
using System.Net;
using Microsoft.AspNetCore.Routing;
using System.Data;
using CustomerManagement.Repository;
using CustomerManagement.connection;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CustomerController : ControllerBase

    {
        private readonly IMapper _mapper;
        private readonly ICosmosRepository _cosmosRepository;
        private QueryBuilder _queryBuilder = new QueryBuilder();
        private readonly string _containerName;
        public CustomerController(IMapper mapper, ICosmosRepository cosmosRepository, IConfiguration configuration)
        {
            _mapper = mapper;
            _cosmosRepository = cosmosRepository;
            _containerName = configuration["CosmosDb:ContainerName"];
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                string build = _queryBuilder.buildQuery(_containerName, null);
                var result = await _cosmosRepository.GetItemsAsync<Customer>(build);
                return Ok(result);
            }
           
           catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{searchTerm}", Name = "GetItem")]
        public async Task<IActionResult> GetbyId(string searchTerm)
        {
            try
            {
                string query = _queryBuilder.buildQuery(_containerName, searchTerm);
                { 
                var item = await _cosmosRepository.GetItemsAsync<CustomerEntity>(query);
                var result = _mapper.Map<List<Customer>>(item.ToList());
                    if(result != null && result.Count >= 1 )
                    {
                        return Ok(result);

                    }
                    return NotFound($"{searchTerm} does not exist");
                }
            }
            catch
            {
                throw new Exception("There was something wrong");

            }

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Customer customer)
        {
            var queryMapper = _mapper.Map<CustomerEntity>(customer);
            queryMapper.Id = Guid.NewGuid().ToString();
            await _cosmosRepository.AddItemAsync(queryMapper);
            return CreatedAtRoute("GetItem", new { searchTerm = queryMapper.Id }, queryMapper);
        }

        [HttpPut]
        [Route("putdata/{id}")]
        public async Task<ActionResult> UpdateCustomer(string id,  [FromBody] CustomerEntity customerEntity)
        {
            try
            {
                string query = _queryBuilder.buildQuery(_containerName, id);
                {
                    var item = await _cosmosRepository.GetItemsAsync<CustomerEntity>(query);
                    var result = item.ToList();
                    if (result != null && result.Count == 1)
                    {
                        var customer= await _cosmosRepository.UpdateItemAsync<CustomerEntity>(id, customerEntity);
                        var mapresult = _mapper.Map<Customer>(customer);
                        return Accepted(mapresult);

                    }
                    return NotFound($"{id} does not exist");
                }
            }
            catch
            {
                throw new Exception("There was something wrong");
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            try
            {
                string query = _queryBuilder.buildQuery(_containerName, id);
                {
                    var item = await _cosmosRepository.GetItemsAsync<CustomerEntity>(query);
                    var result = item.ToList();
                    if (result != null && result.Count == 1)
                    {
                        await _cosmosRepository.DeleteItemAsync<CustomerEntity>(id);
                        return NoContent();
                    }
                    return NotFound($"{id} does not exist");
                }
            }
            catch
            {
                throw new Exception("There was something wrong");
            }
        }
    }

}