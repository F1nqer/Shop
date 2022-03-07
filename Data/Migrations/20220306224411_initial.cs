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
                                   VALUES ('Product1', 2000, N'https://api.technodom.kz/f3/api/v1/images/272/272/9505291599902.webp'),
                                          ('Product2', 2001, N'https://api.technodom.kz/f3/api/v1/images/272/272/136539_1z.webp'),
                                          ('Product3', 2002, N'https://api.technodom.kz/f3/api/v1/images/272/272/226869_1z.webp'),
                                          ('Product4', 2003, N'https://api.technodom.kz/f3/api/v1/images/272/272/242414_1.webp'),
                                          ('Product5', 2004, N'https://api.technodom.kz/f3/api/v1/images/272/272/12005672648734.webp'),
                                          ('Product6', 2005, N'https://api.technodom.kz/f3/api/v1/images/272/272/234311_1.webp'),
                                          ('Product6', 2006, N'https://api.technodom.kz/f3/api/v1/images/8282a88b-79e7-11ec-abd6-02420a01346a'),
                                          ('Product7', 2007, N'https://api.technodom.kz/f3/api/v1/images/126e7520-79de-11ec-abd6-02420a01346a'),
                                          ('Product8', 2008, N'https://api.technodom.kz/f3/api/v1/images/2698f636-7a99-11ec-abd6-02420a01346a'),
                                          ('Product9', 2009, N'https://api.technodom.kz/f3/api/v1/images/b969dcc5-7a9a-11ec-abd6-02420a01346a'),
                                          ('Product10', 2010, N'https://api.technodom.kz/f3/api/v1/images/3d2d4ca0-7a72-11ec-abd6-02420a01346a'),
                                          ('Product11', 2011, N'https://api.technodom.kz/f3/api/v1/images/3fe4314d-7aa8-11ec-bafd-02420a00008f'),
                                          ('Product12', 2012, N'https://api.technodom.kz/f3/api/v1/images/d73dd835-7a9d-11ec-abd6-02420a01346a'),
                                          ('Product13', 2013, N'https://api.technodom.kz/f3/api/v1/images/079cd6c2-7aab-11ec-abd6-02420a01346a'),
                                          ('Product14', 2014, N'https://api.technodom.kz/f3/api/v1/images/44f8ac50-7cdd-11ec-abd6-02420a01346a'),
                                          ('Product15', 2015, N'https://api.technodom.kz/f3/api/v1/images/4d44aaee-7ab0-11ec-9574-02420a000099')");
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
