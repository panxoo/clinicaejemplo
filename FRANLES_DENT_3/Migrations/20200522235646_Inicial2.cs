using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRANLES_DENT_3.Migrations
{
    public partial class Inicial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AtributoRols",
                columns: table => new
                {
                    AtributoRolId = table.Column<string>(nullable: false),
                    NameRol = table.Column<string>(nullable: true),
                    IsMedic = table.Column<bool>(nullable: false),
                    IsAsistente = table.Column<bool>(nullable: false),
                    Hijos = table.Column<bool>(nullable: false),
                    FatherId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtributoRols", x => x.AtributoRolId);
                    table.ForeignKey(
                        name: "FK_AtributoRols_AtributoRols_FatherId",
                        column: x => x.FatherId,
                        principalTable: "AtributoRols",
                        principalColumn: "AtributoRolId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    PaisId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false),
                    Codigo = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.PaisId);
                });

            migrationBuilder.CreateTable(
                name: "PerfilRolRemoves",
                columns: table => new
                {
                    PerfilRolRemoveId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Clinica = table.Column<string>(nullable: true),
                    Rol = table.Column<string>(nullable: true),
                    Perfil = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilRolRemoves", x => x.PerfilRolRemoveId);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    DepartamentoId = table.Column<string>(maxLength: 2, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    PaisId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.DepartamentoId);
                    table.ForeignKey(
                        name: "FK_Departamentos_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "PaisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    ProvinciaId = table.Column<string>(maxLength: 4, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    DepartamentoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.ProvinciaId);
                    table.ForeignKey(
                        name: "FK_Provincias_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Distritos",
                columns: table => new
                {
                    DistritoId = table.Column<string>(maxLength: 6, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    ProvinciaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distritos", x => x.DistritoId);
                    table.ForeignKey(
                        name: "FK_Distritos_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "ProvinciaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clinicas",
                columns: table => new
                {
                    ClinicaID = table.Column<string>(maxLength: 255, nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Codigo = table.Column<string>(nullable: true),
                    MultiSucursal = table.Column<bool>(nullable: false),
                    Dominio = table.Column<string>(maxLength: 50, nullable: false),
                    Extencion = table.Column<string>(maxLength: 5, nullable: false),
                    DistritoId = table.Column<string>(maxLength: 6, nullable: true),
                    Direccion = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinicas", x => x.ClinicaID);
                    table.ForeignKey(
                        name: "FK_Clinicas_Distritos_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distritos",
                        principalColumn: "DistritoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Area_Atencions",
                columns: table => new
                {
                    Area_AtencionId = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 500, nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    ClinicaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area_Atencions", x => x.Area_AtencionId);
                    table.ForeignKey(
                        name: "FK_Area_Atencions_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "ClinicaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clinica_Rols",
                columns: table => new
                {
                    ClinicaId = table.Column<string>(nullable: false),
                    RolId = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinica_Rols", x => new { x.ClinicaId, x.RolId });
                    table.ForeignKey(
                        name: "FK_Clinica_Rols_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "ClinicaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clinica_Rols_AtributoRols_RolId",
                        column: x => x.RolId,
                        principalTable: "AtributoRols",
                        principalColumn: "AtributoRolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Especialidades",
                columns: table => new
                {
                    EspecialidadId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 500, nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    Fecha_Upd = table.Column<DateTime>(nullable: false),
                    Fecha_Add = table.Column<DateTime>(nullable: false),
                    ClinicaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.EspecialidadId);
                    table.ForeignKey(
                        name: "FK_Especialidades_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "ClinicaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Perfils",
                columns: table => new
                {
                    PerfilId = table.Column<string>(maxLength: 255, nullable: false),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    IsMedic = table.Column<bool>(nullable: false),
                    IsAsistente = table.Column<bool>(nullable: false),
                    ClinicaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfils", x => x.PerfilId);
                    table.ForeignKey(
                        name: "FK_Perfils_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "ClinicaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sucursals",
                columns: table => new
                {
                    SucursalId = table.Column<string>(maxLength: 255, nullable: false),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false),
                    Principal = table.Column<bool>(nullable: false),
                    Direccion = table.Column<string>(maxLength: 500, nullable: false),
                    DistritoId = table.Column<string>(nullable: false),
                    Telefono = table.Column<string>(nullable: false),
                    Telefono2 = table.Column<string>(nullable: true),
                    CorreoSucursal = table.Column<string>(nullable: true),
                    CorreoSucursal2 = table.Column<string>(nullable: true),
                    ClinicaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursals", x => x.SucursalId);
                    table.ForeignKey(
                        name: "FK_Sucursals_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "ClinicaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sucursals_Distritos_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distritos",
                        principalColumn: "DistritoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Perfil_Rols",
                columns: table => new
                {
                    PerfilId = table.Column<string>(nullable: false),
                    RolId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil_Rols", x => new { x.PerfilId, x.RolId });
                    table.ForeignKey(
                        name: "FK_Perfil_Rols_Perfils_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfils",
                        principalColumn: "PerfilId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perfil_Rols_AtributoRols_RolId",
                        column: x => x.RolId,
                        principalTable: "AtributoRols",
                        principalColumn: "AtributoRolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(maxLength: 255, nullable: false),
                    TipoDocumento = table.Column<int>(nullable: false),
                    NumDocumento = table.Column<string>(maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false),
                    Apellido_Paterno = table.Column<string>(maxLength: 255, nullable: false),
                    Apellido_Materno = table.Column<string>(maxLength: 255, nullable: true),
                    Sexo = table.Column<string>(maxLength: 1, nullable: false),
                    Fecha_Nac = table.Column<DateTime>(type: "Date", nullable: true),
                    DistritoId = table.Column<string>(maxLength: 6, nullable: true),
                    Direccion = table.Column<string>(maxLength: 500, nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Movil = table.Column<string>(nullable: false),
                    Nombre_Cuenta = table.Column<string>(maxLength: 250, nullable: true),
                    Correo = table.Column<string>(nullable: false),
                    Avatar = table.Column<string>(nullable: true),
                    Acceso = table.Column<bool>(nullable: false),
                    IsMedic = table.Column<bool>(nullable: false),
                    IsAsist = table.Column<bool>(nullable: false),
                    Activo = table.Column<bool>(nullable: false),
                    FechaCreacio = table.Column<DateTime>(nullable: false),
                    FechaEliminacion = table.Column<DateTime>(nullable: false),
                    FechaActuali = table.Column<DateTime>(nullable: false),
                    ClinicaId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    PerfilId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "ClinicaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuarios_Distritos_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distritos",
                        principalColumn: "DistritoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuarios_Perfils_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfils",
                        principalColumn: "PerfilId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuarios_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sucursal_Area_Atencions",
                columns: table => new
                {
                    Sucursal_Area_AtencionId = table.Column<string>(nullable: false),
                    SucursalId = table.Column<string>(nullable: true),
                    Area_AtencionId = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal_Area_Atencions", x => x.Sucursal_Area_AtencionId);
                    table.ForeignKey(
                        name: "FK_Sucursal_Area_Atencions_Area_Atencions_Area_AtencionId",
                        column: x => x.Area_AtencionId,
                        principalTable: "Area_Atencions",
                        principalColumn: "Area_AtencionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sucursal_Area_Atencions_Sucursals_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursals",
                        principalColumn: "SucursalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DatosEmergenciaUsuarios",
                columns: table => new
                {
                    DatosEmergenciaUsuarioId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 255, nullable: true),
                    Apellido_Paterno = table.Column<string>(maxLength: 255, nullable: true),
                    Apellido_Materno = table.Column<string>(maxLength: 255, nullable: true),
                    Parentesco = table.Column<int>(nullable: false),
                    Movil = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosEmergenciaUsuarios", x => x.DatosEmergenciaUsuarioId);
                    table.ForeignKey(
                        name: "FK_DatosEmergenciaUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DatosMedicos",
                columns: table => new
                {
                    DatosMedicoId = table.Column<string>(maxLength: 255, nullable: false),
                    COP = table.Column<int>(nullable: false),
                    RNE = table.Column<string>(nullable: true),
                    Porcentaje_ganancia_asegurada = table.Column<int>(nullable: false),
                    Porcentaje_ganancia_interno = table.Column<int>(nullable: false),
                    OpcRemFija = table.Column<bool>(nullable: false),
                    RemFijaMonto = table.Column<int>(nullable: false),
                    OpcPorc_Ganancia_REM_fj = table.Column<bool>(nullable: false),
                    Porc_Ganancia_REM_FJ = table.Column<int>(nullable: false),
                    Habilidad = table.Column<bool>(nullable: false),
                    FechaRegistroHab = table.Column<DateTime>(nullable: false),
                    FechaIniHab = table.Column<DateTime>(type: "Date", nullable: false),
                    FechaTermHab = table.Column<DateTime>(type: "Date", nullable: false),
                    ClinicaId = table.Column<string>(maxLength: 255, nullable: true),
                    UsuarioId = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosMedicos", x => x.DatosMedicoId);
                    table.ForeignKey(
                        name: "FK_DatosMedicos_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "ClinicaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DatosMedicos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sucursal_Usuarios",
                columns: table => new
                {
                    SucursalId = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal_Usuarios", x => new { x.SucursalId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_Sucursal_Usuarios_Sucursals_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursals",
                        principalColumn: "SucursalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sucursal_Usuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Area_Medicos",
                columns: table => new
                {
                    Area_MedicoId = table.Column<string>(nullable: false),
                    Sucursal_Area_AtencionId = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area_Medicos", x => x.Area_MedicoId);
                    table.ForeignKey(
                        name: "FK_Area_Medicos_Sucursal_Area_Atencions_Sucursal_Area_AtencionId",
                        column: x => x.Sucursal_Area_AtencionId,
                        principalTable: "Sucursal_Area_Atencions",
                        principalColumn: "Sucursal_Area_AtencionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Area_Medicos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Especialidad_Medicos",
                columns: table => new
                {
                    DatosMedicoId = table.Column<string>(maxLength: 255, nullable: false),
                    EspecialidadId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidad_Medicos", x => new { x.DatosMedicoId, x.EspecialidadId });
                    table.ForeignKey(
                        name: "FK_Especialidad_Medicos_DatosMedicos_DatosMedicoId",
                        column: x => x.DatosMedicoId,
                        principalTable: "DatosMedicos",
                        principalColumn: "DatosMedicoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Especialidad_Medicos_Especialidades_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "Especialidades",
                        principalColumn: "EspecialidadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Area_Atencions_ClinicaId",
                table: "Area_Atencions",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Area_Medicos_Sucursal_Area_AtencionId",
                table: "Area_Medicos",
                column: "Sucursal_Area_AtencionId");

            migrationBuilder.CreateIndex(
                name: "IX_Area_Medicos_UsuarioId",
                table: "Area_Medicos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AtributoRols_FatherId",
                table: "AtributoRols",
                column: "FatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Clinica_Rols_RolId",
                table: "Clinica_Rols",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Clinicas_DistritoId",
                table: "Clinicas",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosEmergenciaUsuarios_UsuarioId",
                table: "DatosEmergenciaUsuarios",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DatosMedicos_ClinicaId",
                table: "DatosMedicos",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosMedicos_UsuarioId",
                table: "DatosMedicos",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_PaisId",
                table: "Departamentos",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Distritos_ProvinciaId",
                table: "Distritos",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Especialidad_Medicos_EspecialidadId",
                table: "Especialidad_Medicos",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Especialidades_ClinicaId",
                table: "Especialidades",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Perfil_Rols_RolId",
                table: "Perfil_Rols",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Perfils_ClinicaId",
                table: "Perfils",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Provincias_DepartamentoId",
                table: "Provincias",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_Area_Atencions_Area_AtencionId",
                table: "Sucursal_Area_Atencions",
                column: "Area_AtencionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_Area_Atencions_SucursalId",
                table: "Sucursal_Area_Atencions",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_Usuarios_UsuarioId",
                table: "Sucursal_Usuarios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursals_ClinicaId",
                table: "Sucursals",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursals_DistritoId",
                table: "Sucursals",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ClinicaId",
                table: "Usuarios",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_DistritoId",
                table: "Usuarios",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PerfilId",
                table: "Usuarios",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UserId",
                table: "Usuarios",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Area_Medicos");

            migrationBuilder.DropTable(
                name: "Clinica_Rols");

            migrationBuilder.DropTable(
                name: "DatosEmergenciaUsuarios");

            migrationBuilder.DropTable(
                name: "Especialidad_Medicos");

            migrationBuilder.DropTable(
                name: "Perfil_Rols");

            migrationBuilder.DropTable(
                name: "PerfilRolRemoves");

            migrationBuilder.DropTable(
                name: "Sucursal_Usuarios");

            migrationBuilder.DropTable(
                name: "Sucursal_Area_Atencions");

            migrationBuilder.DropTable(
                name: "DatosMedicos");

            migrationBuilder.DropTable(
                name: "Especialidades");

            migrationBuilder.DropTable(
                name: "AtributoRols");

            migrationBuilder.DropTable(
                name: "Area_Atencions");

            migrationBuilder.DropTable(
                name: "Sucursals");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Perfils");

            migrationBuilder.DropTable(
                name: "Clinicas");

            migrationBuilder.DropTable(
                name: "Distritos");

            migrationBuilder.DropTable(
                name: "Provincias");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Paises");
        }
    }
}
