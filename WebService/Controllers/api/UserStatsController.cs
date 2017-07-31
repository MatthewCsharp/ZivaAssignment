using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Models;
using WebService.Models.DatabaseModels;

namespace WebService.Controllers.api {
    [Produces("application/json")]
    [Route("api/userstats")]
    public class UserStatsController : Controller {
        private readonly ZivaContext _context;

        public UserStatsController(ZivaContext context) {
            _context = context;
        }

        [HttpPost]
        public async Task<JsonResponse> PostUserStats([FromBody] UserStats model) {
            var currentStats = await _context.UserStats.SingleOrDefaultAsync(u => u.UserId == model.UserId);

            if (currentStats != null) {
                currentStats.Height = model.Height;
                currentStats.Weight = model.Weight;
                _context.Update(currentStats);
            }
            else {
                _context.Add(model);
            }

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) {
                return new JsonResponse(false, $"Failed to update the database. Error: {e.Message}  ");
            }

            return new JsonResponse(true);
        }
    }
}