using System;
using GameStore.Data;
using GameStore.Dtos;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEnpointName = "GetGame";

    // private static readonly List<GameSummaryDto> games =
    // [
    //     new(1, "Super Mario Galaxy", "Platformer", 49.99M, new DateOnly(2007, 11, 1)),
    //     new(2, "Dark souls 2", "RPG", 60.00M, new DateOnly(2014, 3, 11)),
    //     new(
    //         3,
    //         "Metal Gear Solid 4: Guns of the Patriots",
    //         "Stealth Action",
    //         39.99M,
    //         new DateOnly(2007, 11, 1)
    //     ),
    // ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        // GET /games
        group.MapGet(
            "/",
            (GameStoreContext dbContext) =>
                dbContext
                    .Games.Include(game => game.Genre)
                    .Select(game => game.ToGameSummaryDto())
                    .AsNoTracking()
        );

        // GET /games/{id}
        group
            .MapGet(
                "/{id}",
                (int id, GameStoreContext dbContext) =>
                {
                    Game? game = dbContext.Games.Find(id);
                    return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
                }
            )
            .WithName(GetGameEnpointName);

        // POST /games
        group.MapPost(
            "/",
            (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                Game game = newGame.ToEntity();

                dbContext.Games.Add(game);
                dbContext.SaveChanges();

                return Results.CreatedAtRoute(
                    GetGameEnpointName,
                    new { id = game.Id },
                    game.ToGameDetailsDto()
                );
            }
        );

        // PUT /games/{id}
        group.MapPut(
            "/{id}",
            (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
            {
                var existingGame = dbContext.Games.Find(id);
                if (existingGame is null)
                {
                    return Results.NotFound();
                }
                dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
                dbContext.SaveChanges();
                return Results.NoContent();
            }
        );

        // DELETE /games/{id}
        group.MapDelete(
            "/{id}",
            (GameStoreContext dbContext, int id) =>
            {
                dbContext.Games.Where(game => game.Id == id).ExecuteDelete();
                return Results.NoContent();
            }
        );
        return group;
    }
}
