using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using GameStore.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEnpointName = "GetGame";

List<GameDto> games =
[
    new(1, "Super Mario Galaxy", "Platformer", 49.99M, new DateOnly(2007, 11, 1)),
    new(2, "Dark souls 2", "RPG", 60.00M, new DateOnly(2014, 3, 11)),
    new(
        3,
        "Metal Gear Solid 4: Guns of the Patriots",
        "Stealth Action",
        39.99M,
        new DateOnly(2007, 11, 1)
    ),
];

// GET /games
app.MapGet("games", () => games);

// GET /games/{id}
app.MapGet(
        "games/{id}",
        (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }
    )
    .WithName(GetGameEnpointName);

// POST /games
app.MapPost(
    "games",
    (CreateGameDto newGame) =>
    {
        GameDto game = new(
            games.Count + 1,
            newGame.Name,
            newGame.Genre,
            newGame.Price,
            newGame.ReleaseDate
        );
        games.Add(game);
        return Results.CreatedAtRoute(GetGameEnpointName, new { id = game.Id }, game);
    }
);

// PUT /games/{id}
app.MapPut(
    "games/{id}",
    (int id, UpdateGameDto updatedGame) =>
    {
        var index = games.FindIndex(game => game.Id == id);
        if (index == -1)
        {
            return Results.NotFound();
        }
        games[index] = new GameDto(
            id,
            updatedGame.Name,
            updatedGame.Genre,
            updatedGame.Price,
            updatedGame.ReleaseDate
        );
        return Results.NoContent();
    }
);

// DELETE /games/{id}
app.MapDelete(
    "games/{id}",
    (int id) =>
    {
        games.RemoveAll(game => game.Id == id);
        return Results.NoContent();
    }
);

app.Run();
