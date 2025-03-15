using TrainingProject.Domain.Entity;
using TrainingProject.Domain.Result;

namespace TrainingProject.Domain.Interfaces.Validations
{
    public interface IReportValidator : IBaseValidator<Report>
    {
        /// <summary>
        /// Проверяется наличие отчёта - если отчёт с переданным названием есть в БД, то создать такой же нельзя
        /// Проверяется пользователь, если с UserId пользователь не найден, то такого пользователя нет
        /// </summary>
        /// <param name="report"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        BaseResult CreateValidator(Report report, User user);
    }
}
