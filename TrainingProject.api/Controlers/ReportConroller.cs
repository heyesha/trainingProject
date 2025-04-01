using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingProject.Domain.Dto.Report;
using TrainingProject.Domain.Interfaces.Services;
using TrainingProject.Domain.Result;

namespace TrainingProject.api.Controlers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ReportConroller : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportConroller(IReportService reportService)
    {
        _reportService = reportService;
    }

    /// <summary>
    /// Получение всех отчётов пользователя по его ID
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <remarks>
    /// Request for get all user's report
    /// 
    ///     GET
    ///     {
    ///         "userId": 1
    ///     {
    /// </remarks>
    /// <response code="200:">Если отчёты пользователя были найдены</response>
    /// <response code="400:">Если отчёты пользователя не были найдены</response>
    [HttpGet("reports/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetUserReports(long userId)
    {
        var response = await _reportService.GetReportsAsync(userId);

        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    /// <summary>
    /// Получение одного отчёта по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <remarks>
    /// Request for get report
    /// 
    ///     GET
    ///     {
    ///         "id": 1
    ///     {
    /// </remarks>
    /// <response code="200:">Если отчёт был найден</response>
    /// <response code="400:">Если отчёт не был найден</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetReport(long id)
    {
        var response = await _reportService.GetReportByIdAsync(id);

        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    /// <summary>
    /// Удаление отчёта
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <remarks>
    /// Request for delete report
    /// 
    ///     DELETE
    ///     {
    ///         "id": 1
    ///     {
    /// </remarks>
    /// <response code="200:">Если отчёт был удалён</response>
    /// <response code="400:">Если отчёт не был удалён</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> DeleteReport(long id)
    {
        var response = await _reportService.DeleteReportAsync(id);

        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    /// <summary>
    /// Создание отчёта
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <remarks>
    /// Request for create report
    /// 
    ///     POST
    ///     {
    ///         "name": "Report #1",
    ///         "description": "Test report",
    ///         "userId": 1
    ///     {
    /// </remarks>
    /// <response code="200:">Если отчёт создался</response>
    /// <response code="400:">Если отчёт не был создан</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> CreateReport([FromBody] CreateReportDto dto)
    {
        var response = await _reportService.CreateReportAsync(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    /// <summary>
    /// Обновление отчёта
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <remarks>
    /// Request for update report
    /// 
    ///     PUT
    ///     {
    ///         "id": 1,
    ///         "name": "Report name",
    ///         "description": "Report description"
    ///     {
    /// </remarks>
    /// <response code="200:">Если отчёт был обновлён</response>
    /// <response code="400:">Если отчёт не был обновлён</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> UpdateReport([FromBody] UpdateReportDto dto)
    {
        var response = await _reportService.UpdateReportAsync(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}
