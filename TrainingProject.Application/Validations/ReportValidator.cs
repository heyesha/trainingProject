using TrainingProject.Application.Resources;
using TrainingProject.Domain.Entity;
using TrainingProject.Domain.Enum;
using TrainingProject.Domain.Interfaces.Validations;
using TrainingProject.Domain.Result;

namespace TrainingProject.Application.Validations
{
    public class ReportValidator : IReportValidator
    {
        public BaseResult CreateValidator(Report report, User user)
        {
            if (report != null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.ReportAlreadyExists,
                    ErrorCode = (int)ErrorCodes.ReportAlreadyExists
                };
            }

            if (user == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }

            return new BaseResult();
        }

        public BaseResult ValidateOnNull(Report model)
        {
            if (model == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.ReportNotFound,
                    ErrorCode = (int)ErrorCodes.ReportNotFound
                };
            }

            return new BaseResult();
        }
    }
}
