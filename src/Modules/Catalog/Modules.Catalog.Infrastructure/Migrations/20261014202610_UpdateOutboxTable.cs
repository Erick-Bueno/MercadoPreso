using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOutboxTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AggregateId",
                schema: "catalog",
                table: "Outbox",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AggregateType",
                schema: "catalog",
                table: "Outbox",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AggregateId",
                schema: "catalog",
                table: "Outbox");

            migrationBuilder.DropColumn(
                name: "AggregateType",
                schema: "catalog",
                table: "Outbox");
        }
    }
}
