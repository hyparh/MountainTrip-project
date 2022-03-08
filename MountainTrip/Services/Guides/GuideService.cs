using MountainTrip.Data;

namespace MountainTrip.Services.Guides
{
    public class GuideService : IGuideService
    {
        private readonly MountainTripDbContext data;

        public GuideService(MountainTripDbContext data)
            => this.data = data;        

        public bool IsGuide(string userId)
            => data.Guides
            .Any(g => g.UserId == userId);

        public int GetIdByUser(string userId)
            => data.Guides
                .Where(g => g.UserId == userId)
                .Select(g => g.Id)
                .FirstOrDefault();
    }
}
