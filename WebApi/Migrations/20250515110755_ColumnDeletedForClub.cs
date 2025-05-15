using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class ColumnDeletedForClub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a294df3-17b8-42d6-b989-2ed28b7deeb0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0915b43-d830-4e05-a4e2-278b8962632c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de6b4fdc-205d-4a0e-a4e5-d349b2db6fdd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbeea87b-cece-4e5b-9946-fa94dc0d62e2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedTime",
                table: "Clubs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Clubs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2f55bcdd-789c-46fe-a500-e168cd033fc5", "49c45194-f351-4c17-b20d-12c302dc28c6", "Club Manager", "CLUB MANAGER" },
                    { "747f036a-f3b2-4b1c-94e4-309683e932c7", "6ff64eb0-9249-4fd6-b7b5-a8a0c3c3890d", "User", "USER" },
                    { "86d63cf8-2a2c-4dc8-b079-d94da6ec7911", "659e0c42-b28c-4dfb-9f95-00b3582c8963", "Academician", "ACADEMICIAN" },
                    { "90f68d32-d612-49f1-8640-81a967135f35", "d798f81c-6637-4155-b444-f75f58bca75b", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 15, 14, 7, 55, 47, DateTimeKind.Local).AddTicks(5937));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 15, 14, 7, 55, 47, DateTimeKind.Local).AddTicks(5945));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 3,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 15, 14, 7, 55, 47, DateTimeKind.Local).AddTicks(5946));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 4,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 15, 14, 7, 55, 47, DateTimeKind.Local).AddTicks(5947));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 15, 14, 7, 55, 47, DateTimeKind.Local).AddTicks(6100), new DateTime(2025, 5, 20, 14, 7, 55, 47, DateTimeKind.Local).AddTicks(6097) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 15, 14, 7, 55, 47, DateTimeKind.Local).AddTicks(6130), new DateTime(2025, 5, 21, 14, 7, 55, 47, DateTimeKind.Local).AddTicks(6129) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 15, 14, 7, 55, 47, DateTimeKind.Local).AddTicks(6132), new DateTime(2025, 5, 20, 14, 7, 55, 47, DateTimeKind.Local).AddTicks(6131) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 15, 14, 7, 55, 47, DateTimeKind.Local).AddTicks(6133), new DateTime(2025, 5, 20, 14, 7, 55, 47, DateTimeKind.Local).AddTicks(6133) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f55bcdd-789c-46fe-a500-e168cd033fc5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "747f036a-f3b2-4b1c-94e4-309683e932c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86d63cf8-2a2c-4dc8-b079-d94da6ec7911");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90f68d32-d612-49f1-8640-81a967135f35");

            migrationBuilder.DropColumn(
                name: "DeletedTime",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Clubs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5a294df3-17b8-42d6-b989-2ed28b7deeb0", "04ea2290-5b81-46fb-9e38-6d17b55f8ff1", "User", "USER" },
                    { "c0915b43-d830-4e05-a4e2-278b8962632c", "0b7e4796-f550-4013-b177-e1a59b5053a2", "Admin", "ADMIN" },
                    { "de6b4fdc-205d-4a0e-a4e5-d349b2db6fdd", "ab4434f5-561a-4567-9a61-9f7637314d5f", "Club Manager", "CLUB MANAGER" },
                    { "fbeea87b-cece-4e5b-9946-fa94dc0d62e2", "93885a90-dca5-4af7-8e55-2c03e0b8b882", "Academician", "ACADEMICIAN" }
                });

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 14, 16, 12, 31, 408, DateTimeKind.Local).AddTicks(4616));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 14, 16, 12, 31, 408, DateTimeKind.Local).AddTicks(4625));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 3,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 14, 16, 12, 31, 408, DateTimeKind.Local).AddTicks(4625));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 4,
                column: "CreatedTime",
                value: new DateTime(2025, 5, 14, 16, 12, 31, 408, DateTimeKind.Local).AddTicks(4626));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 14, 16, 12, 31, 408, DateTimeKind.Local).AddTicks(4772), new DateTime(2025, 5, 19, 16, 12, 31, 408, DateTimeKind.Local).AddTicks(4768) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 14, 16, 12, 31, 408, DateTimeKind.Local).AddTicks(4774), new DateTime(2025, 5, 20, 16, 12, 31, 408, DateTimeKind.Local).AddTicks(4774) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 14, 16, 12, 31, 408, DateTimeKind.Local).AddTicks(4776), new DateTime(2025, 5, 19, 16, 12, 31, 408, DateTimeKind.Local).AddTicks(4775) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "EventDate" },
                values: new object[] { new DateTime(2025, 5, 14, 16, 12, 31, 408, DateTimeKind.Local).AddTicks(4777), new DateTime(2025, 5, 19, 16, 12, 31, 408, DateTimeKind.Local).AddTicks(4777) });
        }
    }
}
