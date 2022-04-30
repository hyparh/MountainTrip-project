using AutoMapper;
using AutoMapper.QueryableExtensions;
using DevExpress.Data.Browsing;
using MountainTrip.Data;
using MountainTrip.Data.Enums;
using MountainTrip.Data.Models;
using MountainTrip.Models.Bookings;
using MountainTrip.Services.Bookings;
using System.Data;

namespace MountainTrip.Services.Trips
{
    public class TripService : ITripService
    {
        private readonly MountainTripDbContext data;
        private readonly IMapper mapper;

        public TripService(MountainTripDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public TripQueryServiceModel All(
            string name = null,
            string searching = null,
            TripSorting sorting = TripSorting.TripName,
            int currentPage = 1,
            int tripsPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var tripsQuery = data.Trips.Where(t => !publicOnly || t.IsPublic);

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

            int totalTrips = tripsQuery.Count();

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

        public IEnumerable<LatestTripServiceModel> Latest()
            => data.Trips
                .Where(t => t.IsPublic)
                .OrderByDescending(t => t.Id)
                .ProjectTo<LatestTripServiceModel>(mapper.ConfigurationProvider)
                .Take(3)
                .ToList();

        public TripDetailsServiceModel Details(int id)
            => data.Trips
            .Where(t => t.Id == id)
            .ProjectTo<TripDetailsServiceModel>(mapper.ConfigurationProvider)            
            .FirstOrDefault();

        public TripDetailsServiceModel AddBooking(int id)
            => data.Trips
            .Where(t => t.Id == id)
            .ProjectTo<TripDetailsServiceModel>(mapper.ConfigurationProvider)
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
            bool IsDifficultyValid = Enum.TryParse(typeof(DifficultyTypes), 
                trip.Difficulty, out object parsedDifficulty);

            Trip tripData = new Trip
            {
                Name = name,
                Description = description,
                Length = length,
                Difficulty = (DifficultyTypes)parsedDifficulty,
                Duration = duration,
                ImageUrl = imageUrl,
                MountainId = mountainId,
                GuideId = guideId,
                IsPublic = false
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
            TripFormModel trip,
            bool isPublic)
        {
            bool IsDifficultyValid = Enum.TryParse(typeof(DifficultyTypes),
                trip.Difficulty, out object parsedDifficulty);
            
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
            tripData.IsPublic = isPublic;

            data.SaveChanges();

            return true;
        }

        public IEnumerable<TripServiceModel> ByUser(string userId)
            => GetTrips(data.Trips
                .Where(t => t.Guide.UserId == userId));

        public bool IsByGuide(int tripId, int guideId)
            => data.Trips
                   .Any(t => t.Id == tripId && t.GuideId == guideId);

        public void ChangeVisibility(int tripId)
        {
            var trip = data.Trips.Find(tripId);

            trip.IsPublic = !trip.IsPublic;

            data.SaveChanges();
        }

        public IEnumerable<string> AllNames()
            => data.Trips
                .Select(t => t.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

        public IEnumerable<TripMountainServiceModel> AllMountains()
           => data.Mountains
              .ProjectTo<TripMountainServiceModel>(mapper.ConfigurationProvider)
              .ToList();

        public bool MountainExists(int mountainId)
            => data.Mountains.Any(m => m.Id == mountainId);

        private IEnumerable<TripServiceModel> GetTrips(IQueryable<Trip> tripQuery)
            => tripQuery
            .ProjectTo<TripServiceModel>(mapper.ConfigurationProvider)
            .ToList();      

        public IEnumerable<BookingServiceModel> MyBookings()
            => data.Bookings
              .ProjectTo<BookingServiceModel>(mapper.ConfigurationProvider)
              .ToList();

        public void Delete(int id)
        {
            var tripToRemove = this.data.Trips
                .Where(c => c.Id == id)
                .FirstOrDefault();

            var tripIdsToRemoveFromMappingTable = this.data.TripsBookings
                .Where(t => t.TripId == id)
                .ToList();

            var tripIdsToRemoveFromBookings = this.data.Bookings
                .Where(t => t.TripId == id)
                .ToList();

            if (tripIdsToRemoveFromMappingTable.Any())
            {
                foreach (var tripId in tripIdsToRemoveFromMappingTable)
                {
                    data.TripsBookings.Remove(tripId);
                    data.SaveChanges();
                }

                foreach (var tripId in tripIdsToRemoveFromBookings)
                {
                    data.Bookings.Remove(tripId);
                    data.SaveChanges();
                }
            }

            data.Trips.Remove(tripToRemove);
            data.SaveChanges();
        }
    }
}
