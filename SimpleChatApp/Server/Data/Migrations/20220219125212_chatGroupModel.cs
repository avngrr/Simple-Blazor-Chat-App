using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleChatApp.Server.Data.Migrations
{
    public partial class chatGroupModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ChatGroup_ChatGroupId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ChatGroup_ToId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatGroup",
                table: "ChatGroup");

            migrationBuilder.RenameTable(
                name: "ChatGroup",
                newName: "ChatGroups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatGroups",
                table: "ChatGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ChatGroups_ChatGroupId",
                table: "AspNetUsers",
                column: "ChatGroupId",
                principalTable: "ChatGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ChatGroups_ToId",
                table: "Messages",
                column: "ToId",
                principalTable: "ChatGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ChatGroups_ChatGroupId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ChatGroups_ToId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatGroups",
                table: "ChatGroups");

            migrationBuilder.RenameTable(
                name: "ChatGroups",
                newName: "ChatGroup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatGroup",
                table: "ChatGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ChatGroup_ChatGroupId",
                table: "AspNetUsers",
                column: "ChatGroupId",
                principalTable: "ChatGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ChatGroup_ToId",
                table: "Messages",
                column: "ToId",
                principalTable: "ChatGroup",
                principalColumn: "Id");
        }
    }
}
