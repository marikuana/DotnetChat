using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetChat.Migrations
{
    public partial class InitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "Discriminator", "Name" },
                values: new object[] { 1, "Group", "Test Group" });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "Discriminator" },
                values: new object[] { 2, "Private" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Password" },
                values: new object[,]
                {
                    { 1, "Marikuana", "123" },
                    { 2, "Bob", "123" },
                    { 3, "Tom", "123" }
                });

            migrationBuilder.InsertData(
                table: "ChatUser",
                columns: new[] { "ChatsId", "MembersId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChatUser",
                keyColumns: new[] { "ChatsId", "MembersId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ChatUser",
                keyColumns: new[] { "ChatsId", "MembersId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ChatUser",
                keyColumns: new[] { "ChatsId", "MembersId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ChatUser",
                keyColumns: new[] { "ChatsId", "MembersId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ChatUser",
                keyColumns: new[] { "ChatsId", "MembersId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
