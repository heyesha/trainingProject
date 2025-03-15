using Microsoft.AspNetCore.Mvc;
using TrainingProject.Domain.Dto.Report;
using TrainingProject.Domain.Interfaces.Services;
using TrainingProject.Domain.Result;

namespace TrainingProject.api.Controlers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportConroller : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportConroller(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<BaseResult<ReportDto>>> GetReport(long id)
        {
            var response = await _reportService.GetReportByIdAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
