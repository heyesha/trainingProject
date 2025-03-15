using TrainingProject.Domain.Result;

namespace TrainingProject.Domain.Interfaces.Validations
{
    public interface IBaseValidator<in T> where T : class
    {
        BaseResult ValidateOnNull(T model);
    }
}
