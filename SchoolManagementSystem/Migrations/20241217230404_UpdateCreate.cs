using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHSuZvM/Of0qsMcoGsR6r7nftJECshvR+Sz3F4jdX2VoTIqm08+CvWc8BDwfEiPSSA==");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMqtDZ4b/WwAZL0GB8zi2NGulhIxjSdVFdwQQXT6nG2UBF1scKSSomyLjANlksKSkg==");

            migrationBuilder.UpdateData(
                table: "TuitionBillings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BillingDate",
                value: new DateTime(2024, 12, 17, 16, 4, 2, 280, DateTimeKind.Local).AddTicks(9873));

            migrationBuilder.UpdateData(
                table: "TuitionPayments",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 12, 17, 16, 4, 2, 284, DateTimeKind.Local).AddTicks(6094));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENijYwMPFVsj0fzCWMiD1wDUE55SDijqGJdr7UbLDPjNqIjULoUPlnGDhI5Ex6ZoRQ==");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENQ2mLUMEEzkr1uHQPWXpt0x43cvcgLWP4/B+lINirQdPvrBZ5tV29loH2IA8IxbCw==");

            migrationBuilder.UpdateData(
                table: "TuitionBillings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BillingDate",
                value: new DateTime(2024, 12, 10, 21, 10, 57, 749, DateTimeKind.Local).AddTicks(3352));

            migrationBuilder.UpdateData(
                table: "TuitionPayments",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 12, 10, 21, 10, 57, 751, DateTimeKind.Local).AddTicks(9049));
        }
    }
}
