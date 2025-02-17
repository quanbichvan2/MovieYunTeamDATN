﻿using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.Booking.Domain.Entities;

namespace WebAPIServer.Modules.Booking.DataAccesses.Data
{
	public class BookingDbContext : DbContext
	{
		public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
		public virtual DbSet<OrderCombo> OrderCombos { get; set; }
		public virtual DbSet<OrderProduct> OrderProducts { get; set; }
		//public virtual DbSet<OrderTicketType> OrderTickets { get; set; }
		public virtual DbSet<OrderShowSeat> OrderShowSeats { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("booking");
			modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
		}
	}
}
