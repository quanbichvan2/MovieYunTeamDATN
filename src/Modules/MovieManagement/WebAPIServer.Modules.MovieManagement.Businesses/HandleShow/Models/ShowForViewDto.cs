﻿using System.ComponentModel.DataAnnotations.Schema;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models.Base;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Modules.MovieManagement.Domain.Enums;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models
{
	public class ShowForViewDto : ShowBaseDto
	{
		public Guid Id { get; set; }
		public string MovieTitle { get; set; }
		public List<GenreMovieDto> Genres { get; set; } = new List<GenreMovieDto>();
		public List<ListHall> ListHall { get; set; } = new List<ListHall>();
        public byte MovieRuntimeMinutes { get; set; }
		public string MoviePosterImage { get; set; } = default!;
        public AgeRating AgeRating { get; set; }
		
    }

    public class ShowForViewApiDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public Guid CinemaHallId { get; set; }
        public string HallName { get; set; } = default!;
        //public string CinemaName { get; set; } = default!;
        public Guid MovieId { get; set; }
        public string MovieTitle { get; set; } = default!;
        public byte MovieRuntimeMinutes { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }

    public class ListHall
	{
		public Guid HallId { get; set; }
		public string HallName { get; set; } = default!;
        public List<ListTime> ListTime { get; set; } = new List<ListTime>();
    }

    public class ListTime
    {
        public string StartTime { get; set; }
        
        public List<ShowTime> ShowTimes { get; set; } = new List<ShowTime>();
    }

	public class GenreMovieDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
	public class ShowTime
	{
        public Guid ShowId { get; set; }
        public string Time { get; set; }
	}
	//public class ShowSeatForViewDto
	//{
	//	public Guid Id { get; set; }
	//	public Guid SeatId { get; set; }
	//	public string SeatType { get; set; } = default!;
	//	public string SeatPosition { get; set; } = default!;
	//	public bool IsReserved { get; set; }
	//}
}
