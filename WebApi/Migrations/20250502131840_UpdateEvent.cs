using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class UpdateEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cdf8408-170e-4ef0-bb29-d751969d7768");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f938748-5629-449a-9c4a-8f741c0de7c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7bd2bc3-9800-48e7-842d-a2a30463f84a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4a0fc64-be3d-48a6-941d-ce0890ed53e0");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "Clubs",
                newName: "CreatedTime");

            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "Club_User",
                newName: "CreatedTime");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedTime",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PublishedById",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Visibility",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "48234570-0df0-4c7a-8d8d-beea21dffba9", "39dbfe30-5902-451f-b2d3-ebe1bc0600dd", "Academician", "ACADEMICIAN" },
                    { "a636ce4f-8bdf-4bae-9d4d-1dd9b29b0599", "b7765845-0801-4e0e-84bc-e18f7f09f348", "Club Manager", "CLUB MANAGER" },
                    { "c07aea72-c411-403c-9fa3-a5239c2b91fa", "d39a4d66-b0ac-452b-9660-ab52ac0fdd75", "User", "USER" },
                    { "fe924581-2464-42df-b8e7-a182f473bdda", "0a83d8a9-66fb-431b-b997-0e1fbb763d4c", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 2, 16, 18, 40, 564, DateTimeKind.Local).AddTicks(5287));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 2, 16, 18, 40, 564, DateTimeKind.Local).AddTicks(5295));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 3,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 2, 16, 18, 40, 564, DateTimeKind.Local).AddTicks(5296));

            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "ClubId", "ClubName", "CreatedTime", "Created_by", "Description", "Faculty", "Logo_url", "Responsible_teacher_id" },
                values: new object[] { 4, "medeniyet Tiyatro kulübü", new DateTime(2025, 5, 2, 16, 18, 40, 564, DateTimeKind.Local).AddTicks(5297), null, "Ölü Ozanlar Derneği Sevenler Kulübü.", "Edebiyat Fakultesi", null, null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "Description", "EventDate", "Location", "PublishedById", "Title", "Visibility" },
                values: new object[] { new DateTime(2025, 5, 2, 16, 18, 40, 564, DateTimeKind.Local).AddTicks(5495), "Gleneksel medeniyet 5.Satranç Müsabakası", new DateTime(2025, 5, 7, 16, 18, 40, 564, DateTimeKind.Local).AddTicks(5492), "medeniyet university", "841f3f0b-5f97-4fe0-954b-0eaa2ddd5fbd", "Quin Gambit  ", "private" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClubId", "CreatedTime", "Description", "EventDate", "Location", "PublishedById", "Title", "Visibility" },
                values: new object[] { 2, new DateTime(2025, 5, 2, 16, 18, 40, 564, DateTimeKind.Local).AddTicks(5499), "Futbol Turnuvası Maç Kura Çekimleri", new DateTime(2025, 5, 7, 16, 18, 40, 564, DateTimeKind.Local).AddTicks(5499), "medeniyet university", "841f3f0b-5f97-4fe0-954b-0eaa2ddd5fbd", "Futbolllll", "private" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "ApprovedById", "ApprovedTime", "ClubId", "CreatedTime", "Description", "EventDate", "ImagePath", "IsApproved", "Location", "PublishedById", "Title", "Visibility" },
                values: new object[] { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 5, 2, 16, 18, 40, 564, DateTimeKind.Local).AddTicks(5500), "Futbol Turnuvası İlk Maçı", new DateTime(2025, 5, 7, 16, 18, 40, 564, DateTimeKind.Local).AddTicks(5500), null, false, "medeniyet university", "841f3f0b-5f97-4fe0-954b-0eaa2ddd5fbd", "Futbol Turnuvası 1.Tur ", "private" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClubId", "CreatedTime", "Description", "EventDate", "Location", "PublishedById", "Title", "Visibility" },
                values: new object[] { 4, new DateTime(2025, 5, 2, 16, 18, 40, 564, DateTimeKind.Local).AddTicks(5498), "Tiyatro etkinlikleri buluşması", new DateTime(2025, 5, 8, 16, 18, 40, 564, DateTimeKind.Local).AddTicks(5497), "medeniyet university", "841f3f0b-5f97-4fe0-954b-0eaa2ddd5fbd", "Mesneviden Tiyatrolar", "private" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48234570-0df0-4c7a-8d8d-beea21dffba9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a636ce4f-8bdf-4bae-9d4d-1dd9b29b0599");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c07aea72-c411-403c-9fa3-a5239c2b91fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe924581-2464-42df-b8e7-a182f473bdda");

            migrationBuilder.DeleteData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ApprovedTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PublishedById",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "Clubs",
                newName: "Created_at");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "Club_User",
                newName: "CreateTime");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Events",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7cdf8408-170e-4ef0-bb29-d751969d7768", "668980a0-cd00-4e74-a197-becf9f6faf97", "Admin", "ADMIN" },
                    { "9f938748-5629-449a-9c4a-8f741c0de7c0", "a085c271-3d70-4bd7-b120-f21f721e4998", "User", "USER" },
                    { "a7bd2bc3-9800-48e7-842d-a2a30463f84a", "e39b2476-3745-4020-aed3-82f392aff585", "Club Manager", "CLUBMANAGER" },
                    { "d4a0fc64-be3d-48a6-941d-ce0890ed53e0", "8b387bc5-f97e-4150-9fca-4a3d02e08d9c", "Academician", "ACADEMICIAN" }
                });

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 1,
                column: "Created_at",
                value: new DateTime(2025, 4, 29, 17, 50, 45, 136, DateTimeKind.Local).AddTicks(8399));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 2,
                column: "Created_at",
                value: new DateTime(2025, 4, 29, 17, 50, 45, 136, DateTimeKind.Local).AddTicks(8407));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 3,
                column: "Created_at",
                value: new DateTime(2025, 4, 29, 17, 50, 45, 136, DateTimeKind.Local).AddTicks(8409));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Price", "Title" },
                values: new object[] { 75m, "Karagöz ve Hacivat" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClubId", "Price", "Title" },
                values: new object[] { 2, 175m, "Mesnevi" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClubId", "Price", "Title" },
                values: new object[] { 1, 375m, "Devlet" });
        }
    }
}
