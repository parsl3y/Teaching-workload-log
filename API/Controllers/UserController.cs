using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class UserConroller : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserQuery _userQuery;
        
        public UserConroller(IUserRepository userRepository, IUserQuery userQuery)
        {
            _userRepository = userRepository;
            _userQuery = userQuery;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll(CancellationToken cancellationToken)
        {
            var users = await _userQuery.GetAll(cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(UserId id, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userQuery.GetById(id, cancellationToken);
                return Ok(users);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user, CancellationToken cancellationToken)
        {
            await _userRepository.Create(user, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(User user, CancellationToken cancellationToken)
        {
            try
            {
                await _userRepository.Update(user, cancellationToken);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(User user, CancellationToken cancellationToken)
        {
            try
            {
                await _userRepository.Delete(user, cancellationToken);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}