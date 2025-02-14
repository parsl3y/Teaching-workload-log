/*using Domain.Entity;
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
        public async Task<ActionResult<IEnumerable<Class>>> GetAll(CancellationToken cancellationToken)
        {
            var classes = await _classQuery.GetAll(cancellationToken);
            return Ok(classes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetById(ClassId id,CancellationToken cancellationToken)
        {
            try
            {
                var classes = await _classQuery.GetById(id,cancellationToken);
                return Ok(classes);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    
        [HttpPost]
        public async Task<ActionResult<Class>> Post(Class @class, CancellationToken cancellationToken)
        {
            await _classRepository.Create(@class, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = @class.Id }, @class);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] ClassId id, Class @class)
        {
            try
            {
                await _classRepository.Update(id, @class);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(ClassId id,CancellationToken cancellationToken)
        {
            try
            {
                await _classRepository.Delete(id, cancellationToken);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}*/