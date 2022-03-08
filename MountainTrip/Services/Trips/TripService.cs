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

        public TripDetailsServiceModel Details(int id)
            => data.Trips
            .Where(t => t.Id == id)
            .Select(t => new TripDetailsServiceModel 
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Difficulty = t.Difficulty.ToString(),
                Duration = t.Duration,
                ImageUrl = t.ImageUrl,
                Length = t.Length,
                GuideId = t.GuideId,
                GuideFullName = t.Guide.FullName,
                UserId = t.Guide.UserId
            })
            .FirstOrDefault();

        public int Create(
            string name,
            string description,
            double length,
            string difficulty,
            string duration,
            string imageUrl,
            int mountainId,
            int guideId,
            TripFormModel trip)
        {
            bool IsDifficultyValid = Enum.TryParse(typeof(DifficultyTypes), trip.Difficulty, out object parsedDifficulty);

            if (!IsDifficultyValid) //TODO: this probably is not necessary
            {
                throw new InvalidDataException("Please select difficulty.");
            }

            var tripData = new Trip
            {
                Name = name,
                Description = description,
                Length = length,
                Difficulty = (DifficultyTypes)parsedDifficulty,
                Duration = duration,
                ImageUrl = imageUrl,
                MountainId = mountainId,
                GuideId = guideId
            };

            data.Trips.Add(tripData);
            data.SaveChanges();

            return tripData.Id;
        }

        public bool Edit(
            int id,
            string name,
            string description,
            double length,
            string difficulty,
            string duration,
            string imageUrl,
            int mountainId,
            TripFormModel trip)
        {
            bool IsDifficultyValid = Enum.TryParse(typeof(DifficultyTypes), trip.Difficulty, out object parsedDifficulty);

            if (!IsDifficultyValid) //TODO: this probably is not necessary
            {
                throw new InvalidDataException("Please select difficulty.");
            }

            var tripData = data.Trips.Find(id);

            //here we check if we have the right to edit
            if (tripData == null)
            {
                return false;
            }

            tripData.Name = name;
            tripData.Description = description;
            tripData.Length = length;
            tripData.Difficulty = (DifficultyTypes)parsedDifficulty;
            tripData.Duration = duration;
            tripData.ImageUrl = imageUrl;
            tripData.MountainId = mountainId;

            data.SaveChanges();

            return true;
        }

        public IEnumerable<TripServiceModel> ByUser(string userId)
            => GetTrips(data.Trips
                .Where(t => t.Guide.UserId == userId));

        public bool IsByGuide(int tripId, int guideId)
            => data.Trips
                   .Any(t => t.Id == tripId && t.GuideId == guideId);

        public IEnumerable<string> AllNames()
            => data.Trips
                .Select(t => t.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

        public IEnumerable<TripMountainServiceModel> AllMountains()
           => data.Mountains
              .Select(m => new TripMountainServiceModel
              {
                  Id = m.Id,
                  Name = m.Name
              })
              .ToList();

        public bool MountainExists(int mountainId)
            => data.Mountains.Any(m => m.Id == mountainId);

        private static IEnumerable<TripServiceModel> GetTrips(IQueryable<Trip> tripQuery)
            => tripQuery
            .Select(t => new TripServiceModel
            {
                Id = t.Id,
                Name = t.Name,
                Difficulty = t.Difficulty.ToString(),
                Duration = t.Duration,
                ImageUrl = t.ImageUrl,
                Length = t.Length,
                MountainName = t.Mountain.Name
            })
            .ToList();        
    }
}
