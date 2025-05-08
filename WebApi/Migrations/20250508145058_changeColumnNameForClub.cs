using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class changeColumnNameForClub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16613be4-c3f2-4244-9c04-76dca6046810");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d27a15e-4900-43be-93c2-a6b870f835ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e0d454c-2827-472e-ab08-e484122f5165");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdd4082a-e194-43e2-b81f-8b923166d091");

            migrationBuilder.DropColumn(
                name: "Created_by",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "Responsible_teacher_id",
                table: "Clubs");

            migrationBuilder.RenameColumn(
                name: "PublishedById",
                table: "Events",
                newName: "PublishedByUserName");

            migrationBuilder.RenameColumn(
                name: "ApprovedById",
                table: "Events",
                newName: "ApprovedByUserName");

            migrationBuilder.AddColumn<string>(
                name: "ClubManager",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Responsible_teacher",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "577e4297-5fc8-4264-8bc9-193e7ed6f4cb", "82d1a11c-2c67-457c-a397-c4b3069d5359", "User", "USER" },
                    { "626ffa0a-b717-4f73-b741-35cabe33a497", "f8fc3442-b12b-4967-8e7c-812680ee3171", "Admin", "ADMIN" },
                    { "910ec266-5407-42c4-b6a4-3bcb51862ffc", "cd526f72-0ee1-47c2-8085-b503091ed747", "Academician", "ACADEMICIAN" },
                    { "b2d3ed31-37ff-4466-9f33-17eeb1f057b8", "b0d64498-1d62-4c09-bfc9-8f81e7edde18", "Club Manager", "CLUB MANAGER" }
                });

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 8, 17, 50, 58, 283, DateTimeKind.Local).AddTicks(225));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 8, 17, 50, 58, 283, DateTimeKind.Local).AddTicks(233));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 3,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 8, 17, 50, 58, 283, DateTimeKind.Local).AddTicks(234));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 4,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 8, 17, 50, 58, 283, DateTimeKind.Local).AddTicks(235));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 50, 58, 283, DateTimeKind.Local).AddTicks(419), new DateTime(2025, 5, 13, 17, 50, 58, 283, DateTimeKind.Local).AddTicks(416) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 50, 58, 283, DateTimeKind.Local).AddTicks(421), new DateTime(2025, 5, 14, 17, 50, 58, 283, DateTimeKind.Local).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 50, 58, 283, DateTimeKind.Local).AddTicks(423), new DateTime(2025, 5, 13, 17, 50, 58, 283, DateTimeKind.Local).AddTicks(422) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 50, 58, 283, DateTimeKind.Local).AddTicks(424), new DateTime(2025, 5, 13, 17, 50, 58, 283, DateTimeKind.Local).AddTicks(423) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "577e4297-5fc8-4264-8bc9-193e7ed6f4cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "626ffa0a-b717-4f73-b741-35cabe33a497");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "910ec266-5407-42c4-b6a4-3bcb51862ffc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2d3ed31-37ff-4466-9f33-17eeb1f057b8");

            migrationBuilder.DropColumn(
                name: "ClubManager",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "Responsible_teacher",
                table: "Clubs");

            migrationBuilder.RenameColumn(
                name: "PublishedByUserName",
                table: "Events",
                newName: "PublishedById");

            migrationBuilder.RenameColumn(
                name: "ApprovedByUserName",
                table: "Events",
                newName: "ApprovedById");

            migrationBuilder.AddColumn<int>(
                name: "Created_by",
                table: "Clubs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Responsible_teacher_id",
                table: "Clubs",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16613be4-c3f2-4244-9c04-76dca6046810", "2a6d9f8e-479e-4398-9ad5-3d788af8b2ef", "User", "USER" },
                    { "5d27a15e-4900-43be-93c2-a6b870f835ba", "9853cdb3-a05c-4326-9771-c8283953b505", "Club Manager", "CLUB MANAGER" },
                    { "9e0d454c-2827-472e-ab08-e484122f5165", "fb57e699-7759-4403-9c39-aae5d4f3fb79", "Admin", "ADMIN" },
                    { "bdd4082a-e194-43e2-b81f-8b923166d091", "fa7fbf20-accb-4175-94f9-463bb49178c5", "Academician", "ACADEMICIAN" }
                });

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 4, 15, 58, 5, 67, DateTimeKind.Local).AddTicks(9878));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 4, 15, 58, 5, 67, DateTimeKind.Local).AddTicks(9887));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 3,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 4, 15, 58, 5, 67, DateTimeKind.Local).AddTicks(9888));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 4,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 4, 15, 58, 5, 67, DateTimeKind.Local).AddTicks(9889));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 4, 15, 58, 5, 68, DateTimeKind.Local).AddTicks(81), new DateTime(2025, 5, 9, 15, 58, 5, 68, DateTimeKind.Local).AddTicks(77) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 4, 15, 58, 5, 68, DateTimeKind.Local).AddTicks(83), new DateTime(2025, 5, 10, 15, 58, 5, 68, DateTimeKind.Local).AddTicks(82) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 4, 15, 58, 5, 68, DateTimeKind.Local).AddTicks(85), new DateTime(2025, 5, 9, 15, 58, 5, 68, DateTimeKind.Local).AddTicks(84) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 4, 15, 58, 5, 68, DateTimeKind.Local).AddTicks(86), new DateTime(2025, 5, 9, 15, 58, 5, 68, DateTimeKind.Local).AddTicks(86) });
        }
    }
}
