using Microsoft.AspNetCore.Mvc;
using UWS.Shared;
using ChinookService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ChinookService.Controllers
{
    // base address: api/customers
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private IAlbumRepository repo;
        // constructor injects repository registered in Startup
        public AlbumsController(IAlbumRepository repo)
        {
            this.repo = repo;
        }
        // GET: api/customers
        // GET: api/customers/?country=[country]
        // this will always return a list of customers even if its empty
        [HttpGet]
        [ProducesResponseType(200,
        Type = typeof(IEnumerable<Album>))]
        public async Task<IEnumerable<Album>> GetAlbums(
        string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return await repo.RetrieveAllAsync();
            }
            else
            {
                return (await repo.RetrieveAllAsync())
                .Where(artist => artist.Title == name);
            }
        }
        // GET: api/customers/[id]
        [HttpGet("{id}", Name = nameof(GetAlbum))] // named route
        [ProducesResponseType(200, Type = typeof(Album))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAlbum(string id)
        {
            Album c = await repo.RetrieveAsync(id);
            if (c == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return Ok(c); // 200 OK with customer in body
        }
        // POST: api/customers
        // BODY: Customer (JSON, XML)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Album))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Album c)
        {
            if (c == null)
            {
                return BadRequest(); // 400 Bad request
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }

            Album added = await repo.CreateAsync(c);
            return CreatedAtRoute( // 201 Created
            routeName: nameof(GetAlbum),
            routeValues: new { id = added.AlbumId.ToLower() },
            value: added);
        }
        // PUT: api/customers/[id]
        // BODY: Customer (JSON, XML)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(
        string id, [FromBody] Album c)
        {
            id = id.ToUpper();
            c.AlbumId = c.AlbumId.ToUpper();
            if (c == null || c.AlbumId != id)
            {
                return BadRequest(); // 400 Bad request
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }
            var existing = await repo.RetrieveAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }
            await repo.UpdateAsync(id, c);
            return new NoContentResult(); // 204 No content
        }
        // DELETE: api/customers/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await repo.RetrieveAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }
            bool? deleted = await repo.DeleteAsync(id);
            if (deleted.HasValue && deleted.Value) // short circuit AND
            {
                return new NoContentResult(); // 204 No content
            }
            else
            {
                return BadRequest( // 400 Bad request
                $"Album {id} was found but failed to delete.");
            }
        }
    }
}
