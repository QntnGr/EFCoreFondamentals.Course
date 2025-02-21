using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PublisherData.Migrations
{
    /// <inheritdoc />
    public partial class AuthorIdChangeAndFeedBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Authors",
                newName: "AuthorId");

            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorId", "BasePrice", "PublishDate", "Title" },
                values: new object[,]
                {
                    { 1, 1, 0m, new DateOnly(1989, 3, 1), "In God's Ear" },
                    { 2, 2, 0m, new DateOnly(2013, 12, 31), "A Tale For the Time Being" },
                    { 3, 3, 0m, new DateOnly(1969, 3, 1), "The Left Hand of Darkness" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Authors",
                newName: "Id");

            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "Books",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
