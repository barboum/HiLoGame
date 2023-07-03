namespace HiLoGame.Domain.Exceptions;

public abstract class GenericGameException : Exception
{
    public Guid EntityId { get; }
    public GenericGameException(string message) : base(message)
    {
    }
    public GenericGameException(Guid entityId, string message) : base(message)
    {
        EntityId = entityId;
    }
}