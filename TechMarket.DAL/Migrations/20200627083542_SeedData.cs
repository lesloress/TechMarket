using Microsoft.EntityFrameworkCore.Migrations;

namespace TechMarket.DAL.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO Categories (Name) Values ('Laptops')");
            migrationBuilder
                .Sql("INSERT INTO Categories (Name) Values ('Tablets')");

            migrationBuilder
               .Sql("INSERT INTO Products (Name, Description, Price, CategoryId) " +
               "Values ('ASUS Adol Laptop Fingerprint Sensor - Rose', " +
               "'13.3 inch Intel Core i5-8265U 8G RAM 256GB SSD GeForce MX150', '1615', " +
               "(SELECT Id FROM Categories WHERE Name = 'Laptops'))");

            migrationBuilder
               .Sql("INSERT INTO Products (Name, Description, Price, CategoryId) " +
               "Values ('Xiaomi RedmiBook 14 inch Notebook Laptop Enhanced Edition', " +
               "'Intel Core i5-10210U 4.2GHz CPU / 8GB DDR4 RAM + 256GB SSD', '1500', " +
               "(SELECT Id FROM Categories WHERE Name = 'Laptops'))");

            migrationBuilder
               .Sql("INSERT INTO Products (Name, Description, Price, CategoryId) " +
               "Values ('Chuwi CoreBook CWI542 2 in 1 Tablet PC with Keyboard and Stylus Pen', " +
               "'13.3 inch Windows 10 Home Version Intel Core m3-7Y30', '936', " +
               "(SELECT Id FROM Categories WHERE Name = 'Tablets'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
               .Sql("DELETE FROM Products");

            migrationBuilder
                .Sql("DELETE FROM Categories");
        }
    }
}
