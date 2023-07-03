namespace HiLoGame.Domain.Exceptions;

public class GameNotCreatedOrStartedException : GenericGameException
{
    internal GameNotCreatedOrStartedException() : base("The game wasn't created or started yet!") { }
}