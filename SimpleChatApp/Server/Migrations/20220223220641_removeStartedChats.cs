using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleChatApp.Server.Migrations
{
    public partial class removeStartedChats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_StartedById",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_StartedById",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "StartedById",
                table: "Chats");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StartedById",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_StartedById",
                table: "Chats",
                column: "StartedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_StartedById",
                table: "Chats",
                column: "StartedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
