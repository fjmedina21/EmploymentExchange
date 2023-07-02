using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmploymentExchange.Migrations
{
    /// <inheritdoc />
    public partial class companylocationsettontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobTypes",
                keyColumn: "Id",
                keyValue: new Guid("0b422690-2508-4e4a-9938-227a320d0301"));

            migrationBuilder.DeleteData(
                table: "JobTypes",
                keyColumn: "Id",
                keyValue: new Guid("3359b02a-779e-4ed8-a4f6-8af06fecc132"));

            migrationBuilder.DeleteData(
                table: "JobTypes",
                keyColumn: "Id",
                keyValue: new Guid("ad9748fe-5220-45ed-b8f2-1d16f1cb9b6d"));

            migrationBuilder.DeleteData(
                table: "JobTypes",
                keyColumn: "Id",
                keyValue: new Guid("b159f5a1-1b92-498e-841e-f890e442a1ed"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d5017b7-693d-4938-96ef-7ca82b01b721"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("874a9195-c738-4c27-b36a-c34a83d9d7ea"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d88053c-a6a9-41ea-8a25-745618623b6d"));

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Companies",
                type: "ntext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                table: "JobTypes",
                columns: new[] { "Id", "CreatedAt", "Name", "State", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3bb18c14-99c8-4469-aa09-8fb4648f336d"), new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8054), "Internship", true, new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8055) },
                    { new Guid("50151c78-2079-4db5-bee8-e422a53dae5c"), new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8050), "Contract", true, new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8051) },
                    { new Guid("8402369e-0b1f-4cb7-9157-d93989f80856"), new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8045), "Part-time", true, new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8046) },
                    { new Guid("b908c4e9-c250-4bec-976a-a13c930f572a"), new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8012), "Full-time", true, new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8032) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "State", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("259470d3-b08f-4a84-9ac2-1625fdbaa5ee"), new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8073), "Employee, looking for a job", "User", true, new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8074) },
                    { new Guid("2ec4f022-3209-40d5-a4e3-3f8243048d08"), new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8061), "Site owner", "Admin", true, new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8062) },
                    { new Guid("9a59725c-a3ae-453c-93b2-e9924a427058"), new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8068), "Recruiter, looking for employ personal", "Poster", true, new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8069) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobTypes",
                keyColumn: "Id",
                keyValue: new Guid("3bb18c14-99c8-4469-aa09-8fb4648f336d"));

            migrationBuilder.DeleteData(
                table: "JobTypes",
                keyColumn: "Id",
                keyValue: new Guid("50151c78-2079-4db5-bee8-e422a53dae5c"));

            migrationBuilder.DeleteData(
                table: "JobTypes",
                keyColumn: "Id",
                keyValue: new Guid("8402369e-0b1f-4cb7-9157-d93989f80856"));

            migrationBuilder.DeleteData(
                table: "JobTypes",
                keyColumn: "Id",
                keyValue: new Guid("b908c4e9-c250-4bec-976a-a13c930f572a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("259470d3-b08f-4a84-9ac2-1625fdbaa5ee"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2ec4f022-3209-40d5-a4e3-3f8243048d08"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9a59725c-a3ae-453c-93b2-e9924a427058"));

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Companies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "ntext");

            migrationBuilder.InsertData(
                table: "JobTypes",
                columns: new[] { "Id", "CreatedAt", "Name", "State", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0b422690-2508-4e4a-9938-227a320d0301"), new DateTime(2023, 7, 1, 15, 39, 5, 574, DateTimeKind.Local).AddTicks(1072), "Internship", true, new DateTime(2023, 7, 1, 15, 39, 5, 574, DateTimeKind.Local).AddTicks(1072) },
                    { new Guid("3359b02a-779e-4ed8-a4f6-8af06fecc132"), new DateTime(2023, 7, 1, 15, 39, 5, 574, DateTimeKind.Local).AddTicks(1067), "Part-time", true, new DateTime(2023, 7, 1, 15, 39, 5, 574, DateTimeKind.Local).AddTicks(1068) },
                    { new Guid("ad9748fe-5220-45ed-b8f2-1d16f1cb9b6d"), new DateTime(2023, 7, 1, 15, 39, 5, 574, DateTimeKind.Local).AddTicks(1070), "Contract", true, new DateTime(2023, 7, 1, 15, 39, 5, 574, DateTimeKind.Local).AddTicks(1070) },
                    { new Guid("b159f5a1-1b92-498e-841e-f890e442a1ed"), new DateTime(2023, 7, 1, 15, 39, 5, 574, DateTimeKind.Local).AddTicks(1048), "Full-time", true, new DateTime(2023, 7, 1, 15, 39, 5, 574, DateTimeKind.Local).AddTicks(1061) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "State", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("7d5017b7-693d-4938-96ef-7ca82b01b721"), new DateTime(2023, 7, 1, 15, 39, 5, 574, DateTimeKind.Local).AddTicks(1081), "Recruiter, looking for employ personal", "Poster", true, new DateTime(2023, 7, 1, 15, 39, 5, 574, DateTimeKind.Local).AddTicks(1081) },
                    { new Guid("874a9195-c738-4c27-b36a-c34a83d9d7ea"), new DateTime(2023, 7, 1, 15, 39, 5, 574, DateTimeKind.Local).AddTicks(1076), "Site owner", "Admin", true, new DateTime(2023, 7, 1, 15, 39, 5, 574, DateTimeKind.Local).AddTicks(1076) },
                    { new Guid("8d88053c-a6a9-41ea-8a25-745618623b6d"), new DateTime(2023, 7, 1, 15, 39, 5, 574, DateTimeKind.Local).AddTicks(1083), "Employee, looking for a job", "User", true, new DateTime(2023, 7, 1, 15, 39, 5, 574, DateTimeKind.Local).AddTicks(1083) }
                });
        }
    }
}
