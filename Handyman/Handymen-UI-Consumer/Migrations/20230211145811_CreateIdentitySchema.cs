using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandymenUIConsumer.Migrations
{
    /// <inheritdoc />
    public partial class CreateIdentitySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "TaskModel",
                newName: "tas_title");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "TaskModel",
                newName: "tas_status");

            migrationBuilder.RenameColumn(
                name: "serviceId",
                table: "TaskModel",
                newName: "tas_service_id");

            migrationBuilder.RenameColumn(
                name: "duration",
                table: "TaskModel",
                newName: "tas_duration");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "TaskModel",
                newName: "tas_description");

            migrationBuilder.RenameColumn(
                name: "dateStarted",
                table: "TaskModel",
                newName: "tas_date_started");

            migrationBuilder.RenameColumn(
                name: "dateFinished",
                table: "TaskModel",
                newName: "tas_date_finished");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tas_title",
                table: "TaskModel",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "tas_status",
                table: "TaskModel",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "tas_service_id",
                table: "TaskModel",
                newName: "serviceId");

            migrationBuilder.RenameColumn(
                name: "tas_duration",
                table: "TaskModel",
                newName: "duration");

            migrationBuilder.RenameColumn(
                name: "tas_description",
                table: "TaskModel",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "tas_date_started",
                table: "TaskModel",
                newName: "dateStarted");

            migrationBuilder.RenameColumn(
                name: "tas_date_finished",
                table: "TaskModel",
                newName: "dateFinished");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
