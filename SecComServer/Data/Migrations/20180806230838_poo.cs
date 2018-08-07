using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SecComServer.Data.Migrations
{
    public partial class poo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncryptedMessages",
                columns: table => new
                {
                    EncryptedMessageId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    ConversationId = table.Column<int>(nullable: true),
                    FirstUser = table.Column<string>(nullable: true),
                    SecondUser = table.Column<string>(nullable: true),
                    Sent = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncryptedMessages", x => x.EncryptedMessageId);
                    table.ForeignKey(
                        name: "FK_EncryptedMessages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "ConversationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncryptedMessages_ConversationId",
                table: "EncryptedMessages",
                column: "ConversationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncryptedMessages");
        }
    }
}
