using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafeWaves.Data.Migrations
{
    /// <inheritdoc />
    public partial class ResolvendoProblemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContatosEmergencia_Usuarios_UsuarioId",
                table: "ContatosEmergencia");

            migrationBuilder.DropForeignKey(
                name: "FK_Leituras_Sensores_SensorId",
                table: "Leituras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leituras",
                table: "Leituras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContatosEmergencia",
                table: "ContatosEmergencia");

            migrationBuilder.RenameTable(
                name: "Leituras",
                newName: "LeituraSensores");

            migrationBuilder.RenameTable(
                name: "ContatosEmergencia",
                newName: "ContatosEmergencias");

            migrationBuilder.RenameIndex(
                name: "IX_Leituras_SensorId",
                table: "LeituraSensores",
                newName: "IX_LeituraSensores_SensorId");

            migrationBuilder.RenameIndex(
                name: "IX_ContatosEmergencia_UsuarioId",
                table: "ContatosEmergencias",
                newName: "IX_ContatosEmergencias_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeituraSensores",
                table: "LeituraSensores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContatosEmergencias",
                table: "ContatosEmergencias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContatosEmergencias_Usuarios_UsuarioId",
                table: "ContatosEmergencias",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeituraSensores_Sensores_SensorId",
                table: "LeituraSensores",
                column: "SensorId",
                principalTable: "Sensores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContatosEmergencias_Usuarios_UsuarioId",
                table: "ContatosEmergencias");

            migrationBuilder.DropForeignKey(
                name: "FK_LeituraSensores_Sensores_SensorId",
                table: "LeituraSensores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeituraSensores",
                table: "LeituraSensores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContatosEmergencias",
                table: "ContatosEmergencias");

            migrationBuilder.RenameTable(
                name: "LeituraSensores",
                newName: "Leituras");

            migrationBuilder.RenameTable(
                name: "ContatosEmergencias",
                newName: "ContatosEmergencia");

            migrationBuilder.RenameIndex(
                name: "IX_LeituraSensores_SensorId",
                table: "Leituras",
                newName: "IX_Leituras_SensorId");

            migrationBuilder.RenameIndex(
                name: "IX_ContatosEmergencias_UsuarioId",
                table: "ContatosEmergencia",
                newName: "IX_ContatosEmergencia_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leituras",
                table: "Leituras",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContatosEmergencia",
                table: "ContatosEmergencia",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContatosEmergencia_Usuarios_UsuarioId",
                table: "ContatosEmergencia",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leituras_Sensores_SensorId",
                table: "Leituras",
                column: "SensorId",
                principalTable: "Sensores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
