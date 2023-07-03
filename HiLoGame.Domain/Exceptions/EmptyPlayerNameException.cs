namespace HiLoGame.Domain.Exceptions;

public class EmptyPlayerNameException : GenericGameException
{
    internal EmptyPlayerNameException() : base("The current player name is empty") { }
}