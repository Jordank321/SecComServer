using Microsoft.EntityFrameworkCore.Migrations;

namespace SecComServer.Data.Migrations
{
    public partial class Convos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversationses_AspNetUsers_FirstUserId",
                table: "Conversationses");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversationses_AspNetUsers_SecondUserId",
                table: "Conversationses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversationses",
                table: "Conversationses");

            migrationBuilder.RenameTable(
                name: "Conversationses",
                newName: "Conversations");

            migrationBuilder.RenameIndex(
                name: "IX_Conversationses_SecondUserId",
                table: "Conversations",
                newName: "IX_Conversations_SecondUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversationses_FirstUserId",
                table: "Conversations",
                newName: "IX_Conversations_FirstUserId");

            migrationBuilder.AddColumn<bool>(
                name: "FirstUserAccepted",
                table: "Conversations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SecondUserAccepted",
                table: "Conversations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations",
                column: "ConversationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_FirstUserId",
                table: "Conversations",
                column: "FirstUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_SecondUserId",
                table: "Conversations",
                column: "SecondUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_FirstUserId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_SecondUserId",
                table: "Conversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "FirstUserAccepted",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "SecondUserAccepted",
                table: "Conversations");

            migrationBuilder.RenameTable(
                name: "Conversations",
                newName: "Conversationses");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_SecondUserId",
                table: "Conversationses",
                newName: "IX_Conversationses_SecondUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_FirstUserId",
                table: "Conversationses",
                newName: "IX_Conversationses_FirstUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversationses",
                table: "Conversationses",
                column: "ConversationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversationses_AspNetUsers_FirstUserId",
                table: "Conversationses",
                column: "FirstUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversationses_AspNetUsers_SecondUserId",
                table: "Conversationses",
                column: "SecondUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
