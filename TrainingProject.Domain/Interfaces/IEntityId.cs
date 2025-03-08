namespace TrainingProject.Domain.Interfaces
{
    public interface IEntityId<T> where T : struct // Чтобы нельзя было ID присвоить string
    {
        public T Id { get; set; }
    }
}
