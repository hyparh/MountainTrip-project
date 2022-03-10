namespace MountainTrip.Services.Guides
{
    public interface IGuideService
    {
        public bool IsGuide(string userId);

        public int IdByUser(string userId);
    }
}
