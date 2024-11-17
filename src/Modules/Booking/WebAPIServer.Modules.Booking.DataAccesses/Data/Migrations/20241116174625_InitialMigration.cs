using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIServer.Modules.Booking.DataAccesses.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "booking");

            migrationBuilder.CreateTable(
                name: "OrderCombos",
                schema: "booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    ComboId = table.Column<Guid>(type: "uuid", nullable: false),
                    ComboName = table.Column<string>(type: "text", nullable: false),
                    ComboTypeName = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCombos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderMovies",
                schema: "booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShowId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    SeatId = table.Column<Guid>(type: "uuid", nullable: false),
                    SeatName = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeName = table.Column<string>(type: "text", nullable: false),
                    HallId = table.Column<Guid>(type: "uuid", nullable: false),
                    HallName = table.Column<string>(type: "text", nullable: false),
                    ShowStartAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ShowEndAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ShowStartEndTime = table.Column<string>(type: "text", nullable: true),
                    MovieTitle = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMovies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                schema: "booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    NetAmount = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    VoucherId = table.Column<Guid>(type: "uuid", nullable: true),
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublised = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "booking",
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                schema: "booking",
                table: "Orders",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderCombos",
                schema: "booking");

            migrationBuilder.DropTable(
                name: "OrderMovies",
                schema: "booking");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "booking");

            migrationBuilder.DropTable(
                name: "OrderStatuses",
                schema: "booking");
        }
    }
}
