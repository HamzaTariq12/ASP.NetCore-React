using ReactWebApp.Dtos;

namespace ReactWebApp.Endpoints
{
    public static class GameEndpoints
    {
        // Games Data
        public static readonly List<GameDto> games = [
            new (1, "Metro 1", "Shooting", 19.99M, new DateOnly(1990, 2, 23)),
            new (2, "Metro 2", "Shooting", 29.99M, new DateOnly(1994, 3, 15)),
            new (3, "Tekken 3", "Fighting", 10.99M, new DateOnly(2000, 5, 16)),
        ];

        public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/games")
                           .WithParameterValidation();

            // GET ALL
            group.MapGet("/", () => games);

            // GET SINGLE
            group.MapGet("/{id}", (int id) =>
            {
                GameDto? game = games.Find((game) => game.Id == id);

                return game is null ? Results.NotFound() : Results.Ok(game);
            }).WithName("GetGame");

            // CREATE
            group.MapPost("/", (CreateGameDto newGame) =>
            {
                GameDto game = new(
                    games.Count + 1,
                    newGame.Name,
                    newGame.Genre,
                    newGame.Price,
                    newGame.ReleaseDate);

                games.Add(game);
                //return game;
                return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game);
            });

            // UPDATE
            group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
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
                    updatedGame.ReleaseDate);

                //return games[index];
                return Results.NoContent();
            });

            // DELETE
            group.MapDelete("/{id}", (int id) =>
            {
                games.RemoveAll(game => game.Id == id);

                return Results.NoContent();
            });

            return group;
        }
    }
}
