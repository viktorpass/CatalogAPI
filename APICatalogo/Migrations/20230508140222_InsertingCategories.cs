using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class InsertingCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {

            mb.Sql("Insert into Categories(Name,UrlImage) Value('Drinks','drinks.jpg')");
            mb.Sql("Insert into Categories(Name,UrlImage) Value('Snacks','snakcs.jpg')");
            mb.Sql("Insert into Categories(Name,UrlImage) Value('Dessert','dessert.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categories");
        }
    }
}
