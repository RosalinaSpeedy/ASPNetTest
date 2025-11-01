using GameStore.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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

app.MapGet("games", () => games);

app.MapGet("/", () => "Hello World!");

app.Run();
