using MountainTrip.Data;
using MountainTrip.Data.Enums;
using MountainTrip.Data.Models;

namespace MountainTrip.Services.Trips
{
    public class TripService : ITripService
    {
        private readonly MountainTripDbContext data;

        public TripService(MountainTripDbContext data)
            => this.data = data;

        public TripQueryServiceModel All(
            string name,
            string searching,
            TripSorting sorting,
            int currentPage,
            int tripsPerPage)
        {
            var tripsQuery = data.Trips.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                tripsQuery = tripsQuery.Where(t => t.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searching))
            {
                tripsQuery = tripsQuery.Where(t =>
                    t.Name.ToLower().Contains(searching.ToLower()) ||
                    t.Duration.ToLower().Contains(searching.ToLower()) ||
                    t.Description.ToLower().Contains(searching.ToLower()));
            }

            tripsQuery = sorting switch
            {
                TripSorting.TripDuration => tripsQuery.OrderByDescending(t => t.Duration),
                TripSorting.TripDifficulty => tripsQuery.OrderByDescending(t => t.Difficulty),
                TripSorting.TripName or _ => tripsQuery.OrderByDescending(t => t.Id)
            };

            var totalTrips = tripsQuery.Count();

            var trips = GetTrips(tripsQuery
                .Skip((currentPage - 1) * tripsPerPage)
                .Take(tripsPerPage));

            return new TripQueryServiceModel
            {
                TotalTrips = totalTrips,
                CurrentPage = currentPage,
                TripsPerPage = tripsPerPage,
                Trips = trips
            };
        }

        public IEnumerable<TripServiceModel> ByUser(string userId)
            => GetTrips(data.Trips
                .Where(t => t.Guide.UserId == userId));

        public IEnumerable<string> AllTripNames()
            => data.Trips
                .Select(t => t.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToList();   
        
        private static IEnumerable<TripServiceModel> GetTrips(IQueryable<Trip> tripQuery)
            => tripQuery
            .Select(t => new TripServiceModel
            {
                Id = t.Id,
                Name = t.Name,
                Difficulty = t.Difficulty.ToString(),
                Duration = t.Duration,
                ImageUrl = t.ImageUrl,
                Length = t.Length
            })
            .ToList();
    }
}
