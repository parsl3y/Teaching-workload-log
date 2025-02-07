using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassRepository _classRepository;
        private readonly IClassQuery _classQuery;


        public ClassController(IClassRepository classRepository, IClassQuery classQuery)
        {
            _classRepository = classRepository;
            _classQuery = classQuery;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classes>>> Get()
        {
            var classes = await _classQuery.GetAllAsync();
            return Ok(classes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Classes>> GetById(string id)
        {
            try
            {
                var classes = await _classQuery.GetByIdAsync(id);
                return Ok(classes);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Classes>> Post(Classes classes)
        {
            await _classRepository.CreateAsync(classes);
            return CreatedAtAction(nameof(GetById), new { id = classes.Id }, classes);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, Classes classes)
        {
            try
            {
                await _classRepository.UpdateAsync(id, classes);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _classRepository.DeleteAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}