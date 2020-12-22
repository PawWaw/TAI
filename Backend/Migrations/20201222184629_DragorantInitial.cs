using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class DragorantInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deliverer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cityId = table.Column<long>(type: "bigint", nullable: false),
                    address = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    username = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    email = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    firstName = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    dragonCoinBalance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliverer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliverer_City",
                        column: x => x.cityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cityId = table.Column<long>(type: "bigint", nullable: false),
                    address = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    username = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    firstName = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Owner_City",
                        column: x => x.cityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cityId = table.Column<long>(type: "bigint", nullable: false),
                    address = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    username = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    email = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    firstName = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_City",
                        column: x => x.cityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FoodIngredient",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    foodId = table.Column<long>(type: "bigint", nullable: false),
                    ingredientId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodIngredient_Food",
                        column: x => x.foodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoodIngredient_Ingredient",
                        column: x => x.ingredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DelivererRate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    delivererId = table.Column<long>(type: "bigint", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    value = table.Column<double>(type: "float", nullable: false),
                    userId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelivererRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DelivererRate_Deliverer",
                        column: x => x.delivererId,
                        principalTable: "Deliverer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ownerId = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.id);
                    table.ForeignKey(
                        name: "FK_Restaurant_Owner",
                        column: x => x.ownerId,
                        principalTable: "Owner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FoodRate",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<long>(type: "bigint", nullable: false),
                    foodId = table.Column<long>(type: "bigint", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodRate", x => x.id);
                    table.ForeignKey(
                        name: "FK_FoodRate_Food",
                        column: x => x.foodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoodRate_User",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderStation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    resteurantId = table.Column<long>(type: "bigint", nullable: false),
                    cityId = table.Column<long>(type: "bigint", nullable: false),
                    address = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    username = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderStation_City",
                        column: x => x.cityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderStation_Restaurant",
                        column: x => x.resteurantId,
                        principalTable: "Restaurant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderStationId = table.Column<long>(type: "bigint", nullable: false),
                    delivererId = table.Column<long>(type: "bigint", nullable: false),
                    userId = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Deliverer",
                        column: x => x.delivererId,
                        principalTable: "Deliverer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_OrderStation",
                        column: x => x.orderStationId,
                        principalTable: "OrderStation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_User",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FoodOrder",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    foodId = table.Column<long>(type: "bigint", nullable: false),
                    orderId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodOrder_Food",
                        column: x => x.foodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoodOrder_Order",
                        column: x => x.orderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliverer_cityId",
                table: "Deliverer",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_DelivererRate_delivererId",
                table: "DelivererRate",
                column: "delivererId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredient_foodId",
                table: "FoodIngredient",
                column: "foodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredient_ingredientId",
                table: "FoodIngredient",
                column: "ingredientId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodOrder_foodId",
                table: "FoodOrder",
                column: "foodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodOrder_orderId",
                table: "FoodOrder",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodRate_foodId",
                table: "FoodRate",
                column: "foodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodRate_userId",
                table: "FoodRate",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_delivererId",
                table: "Order",
                column: "delivererId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_orderStationId",
                table: "Order",
                column: "orderStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_userId",
                table: "Order",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStation_cityId",
                table: "OrderStation",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStation_resteurantId",
                table: "OrderStation",
                column: "resteurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_cityId",
                table: "Owner",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_ownerId",
                table: "Restaurant",
                column: "ownerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_cityId",
                table: "User",
                column: "cityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DelivererRate");

            migrationBuilder.DropTable(
                name: "FoodIngredient");

            migrationBuilder.DropTable(
                name: "FoodOrder");

            migrationBuilder.DropTable(
                name: "FoodRate");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "Deliverer");

            migrationBuilder.DropTable(
                name: "OrderStation");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Restaurant");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
