using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "717f4b88-7769-4a5a-9679-8595c72336da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b556cfb-cbdd-4cf8-ab71-37a4441933c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d783fc1a-c6aa-48b5-b56f-9695aa438dfe");

            migrationBuilder.AddColumn<string>(
                name: "RefleshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefleshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "91fd185c-4482-4060-9630-59b7881ed7b6", null, "Admin", "ADMIN" },
                    { "c65f07b2-043d-49b8-a311-d58858ca00f3", null, "Person", "PERSON" },
                    { "daaf0aa7-0bd9-4f7b-b859-3e9a00225d56", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91fd185c-4482-4060-9630-59b7881ed7b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c65f07b2-043d-49b8-a311-d58858ca00f3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "daaf0aa7-0bd9-4f7b-b859-3e9a00225d56");

            migrationBuilder.DropColumn(
                name: "RefleshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefleshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "717f4b88-7769-4a5a-9679-8595c72336da", null, "Admin", "ADMIN" },
                    { "9b556cfb-cbdd-4cf8-ab71-37a4441933c3", null, "User", "USER" },
                    { "d783fc1a-c6aa-48b5-b56f-9695aa438dfe", null, "Person", "PERSON" }
                });
        }
    }
}
