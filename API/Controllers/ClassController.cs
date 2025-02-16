using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using API.Errors;
using Application.Classes.Commands;
using Application.Common.Interfaces.Queries;
using MediatR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassQuery _classQuery;
        private readonly ISender _sender;

        public ClassController(ISender sender, IClassQuery classQuery)
        {
            _classQuery = classQuery;
            _sender = sender;
        }

        [HttpGet("ClassList")]
        public async Task<ActionResult<IReadOnlyList<FullClassDto>>> GetAll(CancellationToken cancellationToken)
        {
            var entities = await _classQuery.GetAll(cancellationToken);
            return entities.Select(FullClassDto.FromDomainModel).ToList();
        }

        [HttpPost("ClassCreate")]
        public async Task<ActionResult<CreateClassDto>> Create([FromBody] CreateClassDto request,
            CancellationToken cancellationToken)
        {
            var input = new CreateClassCommand
            {
                
                ClassName = request.ClassName,
                CLassNumberToday = request.ClassNumberToday,
                TotalClassNumber = request.TotalClassNumber,
                TeacherId = request.TeacherId,
                Date = request.ClassDate,
            
            };
            
            var result = await _sender.Send(input, cancellationToken);
            return result.Match<ActionResult<CreateClassDto>>(
                c => CreateClassDto.FromDomainModel(c),
                e => e.ToObjectResult());
        }
        
    }
}