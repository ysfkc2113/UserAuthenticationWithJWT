using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class UpdateForClub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_AspNetUsers_ClubManagerId",
                table: "Clubs");

            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_AspNetUsers_Responsible_teacherId",
                table: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_ClubManagerId",
                table: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_Responsible_teacherId",
                table: "Clubs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "148ff3a3-a993-4069-884e-2c35ef890527");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f2987c1-0fea-4b77-8c74-78ebde2a68ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d801897-bea9-496e-9885-2b9038a30b33");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdc74106-9818-41c3-8e73-9e6f11021bb7");

            migrationBuilder.DropColumn(
                name: "ClubManagerId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "Responsible_teacherId",
                table: "Clubs");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ClubManagerId",
                table: "Clubs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Responsible_teacherId",
                table: "Clubs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "148ff3a3-a993-4069-884e-2c35ef890527", "bf3f5a24-a665-48cc-9316-536eeff38850", "Academician", "ACADEMICIAN" },
                    { "4f2987c1-0fea-4b77-8c74-78ebde2a68ee", "04ca5421-195f-4a6b-ad98-30162b3f5016", "Admin", "ADMIN" },
                    { "6d801897-bea9-496e-9885-2b9038a30b33", "121cf5b4-74c3-456b-a012-f4fef39d75f9", "User", "USER" },
                    { "bdc74106-9818-41c3-8e73-9e6f11021bb7", "5041264f-b9a8-4fc3-a754-fe38310894f3", "Club Manager", "CLUBMANAGER" }
                });

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 1,
                column: "Created_at",
                value: new DateTime(2025, 4, 29, 17, 39, 42, 897, DateTimeKind.Local).AddTicks(1561));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 2,
                column: "Created_at",
                value: new DateTime(2025, 4, 29, 17, 39, 42, 897, DateTimeKind.Local).AddTicks(1572));

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "ClubId",
                keyValue: 3,
                column: "Created_at",
                value: new DateTime(2025, 4, 29, 17, 39, 42, 897, DateTimeKind.Local).AddTicks(1573));

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_ClubManagerId",
                table: "Clubs",
                column: "ClubManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_Responsible_teacherId",
                table: "Clubs",
                column: "Responsible_teacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_AspNetUsers_ClubManagerId",
                table: "Clubs",
                column: "ClubManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_AspNetUsers_Responsible_teacherId",
                table: "Clubs",
                column: "Responsible_teacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
