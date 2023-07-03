namespace HiLoGame.Business.Tests;

using HiLoGame.Domain;
using System;

public class DummyGame
{
    private Game _game;

    public Game CreateDummyGame()
    {
        _game = Game.NewGame();
        _game.IsActive = true;

        Player playerLower = new(Guid.NewGuid(), "Jorge", 100);
        Player playerHigher = new(Guid.NewGuid(), "Miguel", 900);
        Player playerWin = new(Guid.NewGuid(), "Paula", 500);

        _game.Players.Add(playerLower);
        _game.Players.Add(playerHigher);
        _game.Players.Add(playerWin);

        return _game;
    }
}
