using Microsoft.EntityFrameworkCore;
using ReactWebApp.Data;
using ReactWebApp.Dtos;
using ReactWebApp.Entities;
using ReactWebApp.Mapping;

namespace ReactWebApp.Endpoints
{
    public static class GameEndpoints
    {
        public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/games")
                           .WithParameterValidation();

            // GET ALL
            group.MapGet("/", async (GameStoreContext dbContext) => 
                await dbContext.Games
                         .Include(game => game.Genre)
                         .Select(game => game.ToGameSummaryDto())
                         .AsNoTracking()
                         .ToListAsync());

            // GET SINGLE
            group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                Game? game = await dbContext.Games.FindAsync(id);

                return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
            }).WithName("GetGame");

            // CREATE
            group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                Game game = newGame.ToEntity();

                dbContext.Games.Add(game);
                await dbContext.SaveChangesAsync();

                return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game.ToGameDetailsDto());
            }); 

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
            {
                var existingGame = await dbContext.Games.FindAsync(id);
                
                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                dbContext.Entry(existingGame)
                    .CurrentValues
                    .SetValues(updatedGame.ToEntity(id));

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                await dbContext.Games
                         .Where(game => game.Id == id)
                         .ExecuteDeleteAsync();

                return Results.NoContent();
            });

            return group;
        }
    }
}
