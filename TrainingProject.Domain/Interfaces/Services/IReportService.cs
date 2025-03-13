using System;
using System.Threading.Tasks;
using TrainingProject.Domain.Dto;
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
    }
}
