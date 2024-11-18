using MediatR;
using Microsoft.Extensions.Caching.Memory;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIServer.Modules.Booking.Businesses.Contracts.Apis;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Commands
{
    public class CheckStatusSeatCommandHandler : IRequestHandler<CheckStatusSeatCommand, bool>
    {
        private IMemoryCache _cache;
        private readonly IMovieManagementModuleApi _movieManagementModuleApi;

        public CheckStatusSeatCommandHandler(IMemoryCache cache, IMovieManagementModuleApi movieManagementModuleApi)
        {
            _cache = cache;
            _movieManagementModuleApi = movieManagementModuleApi;
        }

        public async Task<bool> Handle(CheckStatusSeatCommand request, CancellationToken cancellationToken)
        {
            //var check = _showRepository.GetAll().Where(x => x.MovieId == movie.Id).GroupBy(x => x.CinemaHallId).ToList();
            var cacheKey = new { request.model.Time, request.model.HallId, request.model.SeatName };
            if (!_cache.TryGetValue(cacheKey, out _))
            {
                _cache.Set(cacheKey, true, TimeSpan.FromMinutes(5));
                return true;
            }
            return false;
        }
    }
}
