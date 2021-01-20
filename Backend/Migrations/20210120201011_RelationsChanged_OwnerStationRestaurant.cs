using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class RelationsChanged_OwnerStationRestaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderStation_Restaurant",
                table: "OrderStation");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_ownerId",
                table: "Restaurant");

            migrationBuilder.DropIndex(
                name: "IX_OrderStation_resteurantId",
                table: "OrderStation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "endTime",
                table: "Order",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "isActive",
                table: "Food",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<long>(
                name: "idRestaurant",
                table: "Food",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_ownerId",
                table: "Restaurant",
                column: "ownerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderStation_resteurantId",
                table: "OrderStation",
                column: "resteurantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Food_idRestaurant",
                table: "Food",
                column: "idRestaurant");

            migrationBuilder.CreateIndex(
                name: "IX_DelivererRate_userId",
                table: "DelivererRate",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_DelivererRate_User",
                table: "DelivererRate",
                column: "userId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Restaurant",
                table: "Food",
                column: "idRestaurant",
                principalTable: "Restaurant",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStation_Restaurant_resteurantId",
                table: "OrderStation",
                column: "resteurantId",
                principalTable: "Restaurant",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DelivererRate_User",
                table: "DelivererRate");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Restaurant",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderStation_Restaurant_resteurantId",
                table: "OrderStation");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_ownerId",
                table: "Restaurant");

            migrationBuilder.DropIndex(
                name: "IX_OrderStation_resteurantId",
                table: "OrderStation");

            migrationBuilder.DropIndex(
                name: "IX_Food_idRestaurant",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_DelivererRate_userId",
                table: "DelivererRate");

            migrationBuilder.DropColumn(
                name: "idRestaurant",
                table: "Food");

            migrationBuilder.AlterColumn<DateTime>(
                name: "endTime",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "isActive",
                table: "Food",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_ownerId",
                table: "Restaurant",
                column: "ownerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStation_resteurantId",
                table: "OrderStation",
                column: "resteurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStation_Restaurant",
                table: "OrderStation",
                column: "resteurantId",
                principalTable: "Restaurant",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
