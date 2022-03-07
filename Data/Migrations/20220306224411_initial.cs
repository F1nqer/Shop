using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    CardNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new { x.ProductId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProductsHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<bool>(type: "bit", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProductsHistory_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProductsHistory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    CardNumber = table.Column<int>(type: "int", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersHistory_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersHistory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdersHistory_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductsHistory_OrderId",
                table: "OrderProductsHistory",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductsHistory_ProductId",
                table: "OrderProductsHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StateId",
                table: "Orders",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersHistory_OrderId",
                table: "OrdersHistory",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersHistory_ProductId",
                table: "OrdersHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersHistory_StateId",
                table: "OrdersHistory",
                column: "StateId");
            migrationBuilder.Sql(
                    @"CREATE TRIGGER [OrdersHistoryTrigger]
	                    ON [dbo].[Orders]
	                    FOR DELETE, INSERT, UPDATE
	                    AS
	                    BEGIN
		                    SET NOCOUNT ON
			                    INSERT INTO [ShopDb]..OrdersHistory (OrderId, Address, StateId, CardNumber, PeriodStart, PeriodEnd)
				                    SELECT Id, Address, StateId, CardNumber, GETDATE(), GETDATE() FROM deleted
				                    UNION ALL
				                    SELECT Id, Address, StateId, CardNumber, GETDATE(), GETDATE() FROM inserted
		                    SET NOCOUNT OFF
	                    END");
            migrationBuilder.Sql(
                    @"CREATE TRIGGER [OrderProductsHistoryTrigger]
	                    ON [dbo].[OrderProducts]
	                    FOR DELETE, INSERT, UPDATE
	                    AS
	                    BEGIN
		                    SET NOCOUNT ON
			                    INSERT INTO [ShopDb]..OrderProductsHistory (OrderId, ProductId, Action, PeriodStart, PeriodEnd)
				                    SELECT OrderId, ProductId, 0, GETDATE(), GETDATE() FROM deleted
				                    UNION ALL
				                    SELECT OrderId, ProductId, 1, GETDATE(), GETDATE() FROM inserted
		                    SET NOCOUNT OFF
	                    END");
            migrationBuilder.Sql(@"INSERT INTO [ShopDB]..Products
                                   VALUES ('Product1', 2000, '1'),
                                          ('Product2', 2001, '1'),
                                          ('Product3', 2002, '1'),
                                          ('Product4', 2003, '1'),
                                          ('Product5', 2004, '1'),
                                          ('Product6', 2005, '1'),
                                          ('Product6', 2006, '1'),
                                          ('Product7', 2007, '1'),
                                          ('Product8', 2008, '1'),
                                          ('Product9', 2009, '1'),
                                          ('Product10', 2010, '1'),
                                          ('Product11', 2011, '1'),
                                          ('Product12', 2012, '1'),
                                          ('Product13', 2013, '1'),
                                          ('Product14', 2014, '1'),
                                          ('Product15', 2015, '1')");
            migrationBuilder.Sql(@"INSERT INTO [ShopDB]..State 
	                                VALUES (N'Оформляется', 'Active'),
		                                    (N'Оплачен', 'Buyed'),
		                                    (N'Готов', 'Done')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "OrderProductsHistory");

            migrationBuilder.DropTable(
                name: "OrdersHistory");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "State");
        }
    }
}
