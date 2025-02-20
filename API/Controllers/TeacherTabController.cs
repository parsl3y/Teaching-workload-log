using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using API.DTOs.TeacherTabs;
using API.Errors;
using Application.TeacherTabs.Commands;
using Application.Common.Interfaces.Queries;
using Domain.Entity.TeacherTab;
using MediatR;
using Application.Commands; // Додаємо це для доступу до SaveTeacherTabToExcelCommand

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherTabController : ControllerBase
    {
        private readonly IClassQuery _classQuery;
        private readonly ISender _sender;
        private readonly SaveTeacherTabToExcelCommand _saveTeacherTabToExcelCommand;

        public TeacherTabController(ISender sender, IClassQuery classQuery, SaveTeacherTabToExcelCommand saveTeacherTabToExcelCommand)
        {
            _classQuery = classQuery;
            _sender = sender;
            _saveTeacherTabToExcelCommand = saveTeacherTabToExcelCommand;
        }

        [HttpPost("TeacherTabCreate")]
        public async Task<ActionResult<CreateTeacherTabDto>> Create([FromBody] CreateTeacherTabDto request,
            CancellationToken cancellationToken)
        {
            var input = new CreateTeacherTabCommand
            {
                ClassId = request.ClassId,
                Theme = request.Theme,
                HourCount = request.HourCount
            };
            
            var result = await _sender.Send(input, cancellationToken);
            return result.Match<ActionResult<CreateTeacherTabDto>>(
                t => CreateTeacherTabDto.FromDomainModel(t),
                e => e.ToObjectResult());
        }

        [HttpPost("SaveTeacherTabToExcel")]
        public async Task<ActionResult> SaveToExcel([FromBody] TeacherTabIdDto teacherTabIdDto, CancellationToken cancellationToken)
        {
            try
            {
                await _saveTeacherTabToExcelCommand.ExecuteAsync(
                    teacherTabIdDto.TeacherTabId, 
                    "TeacherTabData.xlsx", 
                    cancellationToken);

                return Ok(new { message = "Data saved to Excel successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
