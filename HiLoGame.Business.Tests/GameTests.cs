namespace HiLoGame.Business.Tests;

using HiLoGame.Domain;
using HiLoGame.Domain.Exceptions;
using System;

public class GameTests
{
    private Game _game;

    public GameTests()
    {
        DummyGame dummyGame = new();
        _game = dummyGame.CreateDummyGame();
    }

    [Fact]
    public async void When_InitsGame_And_No_Players_Should_Throw_NoPlayersException()
    {
        //Arrange
        _game.Players.Clear();

        //Assert
        Assert.Throws<NoPlayersException>(_game.InitGame);
    }

    [Fact]
    public async void When_PlayerName_Is_Empty_Should_Throw_EmptyPlayerNameException()
    {
        //Assert
        Assert.Throws<EmptyPlayerNameException>(()=>_game.AddPlayer(string.Empty));
    }

    [Fact]
    public async void When_Try_GuessNumber_And_Game_Is_Not_Active_Should_Throw_GameNotStartedException()
    {
        //Arrange
        _game.IsActive = false;
        //Assert
        Assert.Throws<GameNotCreatedOrStartedException>(() => _game.GuessNumber(Guid.NewGuid(), 200));
    }
}
