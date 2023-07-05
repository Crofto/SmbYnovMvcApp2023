using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmbYnovMvcApp2023.Migrations
{
    /// <inheritdoc />
    public partial class dbinitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FonctionalId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastConnected = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeviceType = table.Column<int>(type: "int", nullable: false),
                    ConnectivityType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastConnected = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UplinkMessages",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId1 = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecieveIdAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RawPayLoad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeFrequency = table.Column<int>(type: "int", nullable: false),
                    ConnectionConfig = table.Column<int>(type: "int", nullable: false),
                    ConnectionFrequency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UplinkMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UplinkMessages_Devices_DeviceId1",
                        column: x => x.DeviceId1,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSeries",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UplinkMessageId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DeviceId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeSeriesName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSeries_UplinkMessages_UplinkMessageId",
                        column: x => x.UplinkMessageId,
                        principalTable: "UplinkMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeSeries_UplinkMessageId",
                table: "TimeSeries",
                column: "UplinkMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UplinkMessages_DeviceId1",
                table: "UplinkMessages",
                column: "DeviceId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerSettings");

            migrationBuilder.DropTable(
                name: "TimeSeries");

            migrationBuilder.DropTable(
                name: "UplinkMessages");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
