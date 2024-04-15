using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_toDoLists",
                table: "toDoLists");

            migrationBuilder.RenameTable(
                name: "toDoLists",
                newName: "ToDoLists");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoLists",
                table: "ToDoLists",
                column: "ToDoListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoLists",
                table: "ToDoLists");

            migrationBuilder.RenameTable(
                name: "ToDoLists",
                newName: "toDoLists");

            migrationBuilder.AddPrimaryKey(
                name: "PK_toDoLists",
                table: "toDoLists",
                column: "ToDoListId");
        }
    }
}
