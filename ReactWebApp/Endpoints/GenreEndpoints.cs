using Microsoft.EntityFrameworkCore;
using ReactWebApp.Data;
using ReactWebApp.Mapping;

namespace ReactWebApp.Endpoints
{
    public static class GenreEndpoints
    {
        public static RouteGroupBuilder MapGenreEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("genres");

            group.MapGet("/", async (GameStoreContext dbContext) =>
                await dbContext.Genres
                               .Select(genre => genre.ToDto())
                               .AsNoTracking()
                               .ToListAsync());
            
            return group;
        }
    }
}
