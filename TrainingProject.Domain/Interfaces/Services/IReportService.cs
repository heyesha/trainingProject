using System;
using System.Threading.Tasks;
using TrainingProject.Domain.Dto.Report;
using TrainingProject.Domain.Entity;
using TrainingProject.Domain.Result;

namespace TrainingProject.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис, отвечающий за работу с доменной частью отчёта (Report)
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Получение всех отчётов пользователя (Reports)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<CollectionResult<ReportDto>> GetReportsAsync(long userId);

        /// <summary>
        /// Получение отчёта по идентификатору
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDto>> GetReportByIdAsync(long userId);
        
        /// <summary>
        /// Создание отчёта с базовыми параметрами
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto);

        /// <summary>
        /// Удаление отчёта по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDto>> DeleteReportAsync(long id);

        /// <summary>
        /// Обновление отчёта
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto);
    }
}
