﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TrainingProject.Application.Resources;
using TrainingProject.Domain.Dto.Report;
using TrainingProject.Domain.Entity;
using TrainingProject.Domain.Enum;
using TrainingProject.Domain.Interfaces.Repositories;
using TrainingProject.Domain.Interfaces.Services;
using TrainingProject.Domain.Interfaces.Validations;
using TrainingProject.Domain.Result;

namespace TrainingProject.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportValidator _reportValidator;
        private readonly IBaseRepository<Report> _reportRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ReportService(IBaseRepository<Report> reportRepository, ILogger logger)
        {
            _reportRepository = reportRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.UserId);
                var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
                var result = _reportValidator.CreateValidator(report, user);

                if (!result.IsSuccess)
                {
                    return new BaseResult<ReportDto>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode
                    };
                }

                report = new Report()
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    UserId = user.Id
                };
                await _reportRepository.CreateAsync(report);
                return new BaseResult<ReportDto>()
                {
                    Data =_mapper.Map<ReportDto>(report),
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        /// <inheritdoc />
        public async Task<BaseResult<ReportDto>> DeleteReportAsync(long id)
        {
            try
            {
                var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                var result = _reportValidator.ValidateOnNull(report);

                if (!result.IsSuccess)
                {
                    return new BaseResult<ReportDto>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode
                    };
                }

                await _reportRepository.RemoveAsync(report);
                return new BaseResult<ReportDto>()
                {
                    Data = _mapper.Map<ReportDto>(report)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        /// <inheritdoc />
        public Task<BaseResult<ReportDto>> GetReportByIdAsync(long id)
        {
            ReportDto? report;
            try
            {
                report = _reportRepository.GetAll()
                    .AsEnumerable()
                    .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                    .FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return Task.FromResult(new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                }); 
            }

            if (report == null)
            {
                _logger.Warning($"Отчёт с {id} не найден");
                return Task.FromResult(new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.ReportNotFound,
                    ErrorCode = (int)ErrorCodes.ReportNotFound
                });
            }
            return Task.FromResult(new BaseResult<ReportDto>()
            {
                Data = report
            });
        }

        /// <inheritdoc />
        public async Task<CollectionResult<ReportDto>> GetReportsAsync(long userId)
        {
            ReportDto[] reports;

            try
            {
                reports = await _reportRepository.GetAll()
                    .Where(x => x.UserId == userId)
                    .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }

            if (!reports.Any())
            {
                _logger.Warning(ErrorMessage.ReportsNotFound, reports.Length);
                return new CollectionResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.ReportsNotFound,
                    ErrorCode = (int)ErrorCodes.ReportsNotFound
                };
            }

            return new CollectionResult<ReportDto>()
            {
                Data = reports,
                Count = reports.Length
            };
        }

        /// <inheritdoc />
        public async Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto)
        {
            try
            {
                var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
                var result = _reportValidator.ValidateOnNull(report);

                if (!result.IsSuccess)
                {
                    return new BaseResult<ReportDto>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode
                    };
                }

                report.Name = dto.Name;
                report.Description = dto.Description;

                await _reportRepository.UpdateAsync(report);

                return new BaseResult<ReportDto>()
                {
                    Data = _mapper.Map<ReportDto>(report)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }
    }
}
