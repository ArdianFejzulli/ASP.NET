using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagment.Data.Migrations
{
    public partial class AddPhonee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "RegisterStudents",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber2",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber2",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "RegisterStudents",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
