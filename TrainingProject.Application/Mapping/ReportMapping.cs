using AutoMapper;
using TrainingProject.Domain.Dto.Report;
using TrainingProject.Domain.Entity;

namespace TrainingProject.Application.Mapping
{
    public class ReportMapping : Profile
    {
        public ReportMapping()
        {
            CreateMap<Report, ReportDto>().ReverseMap();
        }
    }
}
