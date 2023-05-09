using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class InsertingProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Products(Name,Description,Price,UrlImage,Stock,RegisterDate,CategoryId) Values('Coca-Cola Diet','Coca-Cola 350ml',5.45,'cocacola.jpg',50,now(),1)");

            mb.Sql("Insert into Products(Name,Description,Price,UrlImage,Stock,RegisterDate,CategoryId) Values('Tuna Snack','Tuna Snack with mayonnaise',8.50,'tunalunch.jpg',10,now(),2)");

            mb.Sql("Insert into Products(Name,Description,Price,UrlImage,Stock,RegisterDate,CategoryId) Values('Pudding','Condensed Milk Pudding 100g',6.75,'pudding.jpg',20,now(),3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Products");
        }
    }
}
