using Microsoft.OpenApi.Models;
using WebAPIServer.Bootstrapper.Extensions;
using WebAPIServer.Modules.Booking.Api;
using WebAPIServer.Modules.Catalog.Api;
using WebAPIServer.Modules.Identity.Api;
using WebAPIServer.Modules.MovieManagement.Api;
using WebAPIServer.Modules.Payment.Api;
using WebAPIServer.Modules.Tickets.Api;
using WebAPIServer.Modules.Users.Api;
using WebAPIServer.Modules.Vouchers.Api;
using WebAPIServer.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCatalogModule();
builder.Services.AddMovieManagementModule();
builder.Services.AddTicketsModule();
builder.Services.AddBookingModule();
builder.Services.AddPaymentModule();
builder.Services.AddUsersModule();
builder.Services.AddVoucherModule();

// Default
builder.Services.AddIdentityModule();
builder.Services.AddInfrastructure();
builder.Services.AddMapperIntialize();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
	opt.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
	opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "bearer"
	});
	opt.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Id = "Bearer",
					Type = ReferenceType.SecurityScheme
				}
			},
			new string[]{}
		}
	});
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();

// Add middlewares module
app.UseInfrastructure();
app.UseMovieManagementModule();
app.UseCatalogModule();
app.UseTicketsModule();
app.UseBookingModule();
app.UsePaymentModule();
app.UseUsersModule();
app.UseVoucherModule();
// Default
app.UseIdentityModule();
app.MapControllers();

app.Run();