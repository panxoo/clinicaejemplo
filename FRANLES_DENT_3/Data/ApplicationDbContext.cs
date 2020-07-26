using FRANLES_DENT_3.Models.Empresa;
using FRANLES_DENT_3.Models.Empresa.Atributos;
using FRANLES_DENT_3.Models.Historico;
using FRANLES_DENT_3.Models.Localization;
using FRANLES_DENT_3.Models.MedicoDato;
using FRANLES_DENT_3.Models.MedicoDato.Atributo;
using FRANLES_DENT_3.Models.Permisos;
using FRANLES_DENT_3.Models.Personal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FRANLES_DENT_3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Clinica> Clinicas { get; set; }
        public DbSet<Clinica_Rol> Clinica_Rols { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<DatosMedico> DatosMedicos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfils { get; set; }
        public DbSet<Perfil_Rol> Perfil_Rols { get; set; }
        public DbSet<Especialidad_Medico> Especialidad_Medicos { get; set; }
        public DbSet<Sucursal> Sucursals { get; set; }
        public DbSet<PerfilRolRemove> PerfilRolRemoves { get; set; }
        public DbSet<DatosEmergenciaUsuario> DatosEmergenciaUsuarios { get; set; }
        public DbSet<AtributoRol> AtributoRols { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<Area_Atencion> Area_Atencions { get; set; }
        public DbSet<Sucursal_Area_Atencion> Sucursal_Area_Atencions { get; set; }
        public DbSet<Sucursal_Usuario> Sucursal_Usuarios { get; set; }
        public DbSet<Area_Medico> Area_Medicos { get; set; }
        public DbSet<Tipo_Horario> Tipo_Horarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Especialidad>().HasQueryFilter(p => p.Activo);
            builder.Entity<Clinica_Rol>().HasQueryFilter(p => p.Active);
            builder.Entity<Perfil>().HasQueryFilter(p => p.Activo);
            builder.Entity<Usuario>().HasQueryFilter(p => p.Activo);
            builder.Entity<Area_Atencion>().HasQueryFilter(p => p.Activo);
            builder.Entity<Sucursal_Area_Atencion>().HasQueryFilter(p => p.Activo);
            builder.Entity<Area_Medico>().HasQueryFilter(p => p.Activo);

            builder.Entity<Perfil_Rol>().HasKey(x => new { x.PerfilId, x.RolId });

            builder.Entity<Perfil_Rol>()
                    .HasOne(sc => sc.Perfil)
                    .WithMany(s => s.Perfil_Rols)
                    .HasForeignKey(sc => sc.PerfilId);

            builder.Entity<Perfil_Rol>()
                    .HasOne(sc => sc.Role)
                    .WithMany(s => s.Perfil_Rols)
                    .HasForeignKey(sc => sc.RolId);

            builder.Entity<Clinica_Rol>().HasKey(x => new { x.ClinicaId, x.RolId });

            builder.Entity<Clinica_Rol>()
                   .HasOne<Clinica>(sc => sc.Clinica)
                   .WithMany(s => s.Clinica_Rols)
                   .HasForeignKey(sc => sc.ClinicaId);

            builder.Entity<Clinica_Rol>()
                    .HasOne<AtributoRol>(sc => sc.Rol)
                    .WithMany(s => s.Clinica_Rols)
                    .HasForeignKey(sc => sc.RolId);

            builder.Entity<Especialidad_Medico>().HasKey(x => new { x.DatosMedicoId, x.EspecialidadId });

            builder.Entity<Especialidad_Medico>()
                    .HasOne<Especialidad>(sc => sc.Especialidad)
                    .WithMany(s => s.Especialidad_Medicos)
                    .HasForeignKey(sc => sc.EspecialidadId);

            builder.Entity<Especialidad_Medico>()
                    .HasOne<DatosMedico>(sc => sc.DatosMedico)
                    .WithMany(s => s.Especialidad_Medicos)
                    .HasForeignKey(sc => sc.DatosMedicoId);

            builder.Entity<Sucursal>()
                   .HasMany<Sucursal_Area_Atencion>(sc => sc.Sucursal_Area_Atencions).WithOne(wo => wo.Sucursal).HasForeignKey(sc => sc.SucursalId);

            builder.Entity<Area_Atencion>()
                   .HasMany(sc => sc.Sucursal_Area_Atencions).WithOne(wo => wo.Area_Atencion).HasForeignKey(sc => sc.Area_AtencionId);

            builder.Entity<Sucursal_Area_Atencion>()
                   .HasOne<Sucursal>(sc => sc.Sucursal)
                   .WithMany(s => s.Sucursal_Area_Atencions)
                   .HasForeignKey(sc => sc.SucursalId);

            builder.Entity<Sucursal_Area_Atencion>()
                   .HasOne<Area_Atencion>(sc => sc.Area_Atencion)
                   .WithMany(s => s.Sucursal_Area_Atencions)
                   .HasForeignKey(sc => sc.Area_AtencionId);

            builder.Entity<Sucursal_Usuario>().HasKey(x => new { x.SucursalId, x.UsuarioId });

            builder.Entity<Sucursal>()
                   .HasMany<Sucursal_Usuario>(sc => sc.Sucursal_Usuarios).WithOne(wo => wo.Sucursal).HasForeignKey(sc => sc.SucursalId);

            builder.Entity<Usuario>()
                   .HasMany(sc => sc.Sucursal_Usuarios).WithOne(wo => wo.Usuario).HasForeignKey(sc => sc.UsuarioId);

            builder.Entity<Sucursal_Usuario>()
                   .HasOne<Sucursal>(sc => sc.Sucursal)
                   .WithMany(s => s.Sucursal_Usuarios)
                   .HasForeignKey(sc => sc.SucursalId);

            builder.Entity<Sucursal_Usuario>()
                   .HasOne<Usuario>(sc => sc.Usuario)
                   .WithMany(s => s.Sucursal_Usuarios)
                   .HasForeignKey(sc => sc.UsuarioId);

            builder.Entity<Sucursal_Area_Atencion>()
                   .HasMany<Area_Medico>(sc => sc.Area_Medicos).WithOne(wo => wo.Sucursal_Area_Atencion).HasForeignKey(sc => sc.Sucursal_Area_AtencionId);

            builder.Entity<Usuario>()
                   .HasMany(sc => sc.Area_Medicos).WithOne(wo => wo.Usuario).HasForeignKey(sc => sc.UsuarioId);

            builder.Entity<Area_Medico>()
                   .HasOne<Sucursal_Area_Atencion>(sc => sc.Sucursal_Area_Atencion)
                   .WithMany(s => s.Area_Medicos)
                   .HasForeignKey(sc => sc.Sucursal_Area_AtencionId);

            builder.Entity<Area_Medico>()
                   .HasOne<Usuario>(sc => sc.Usuario)
                   .WithMany(s => s.Area_Medicos)
                   .HasForeignKey(sc => sc.UsuarioId);

            builder.Entity<Perfil>()
                   .HasOne<Clinica>(s => s.Clinica)
                   .WithMany(g => g.Perfils)
                   .HasForeignKey(s => s.ClinicaId);

            builder.Entity<Usuario>()
                .HasOne<DatosMedico>(s => s.DatosMedico)
                .WithOne(ad => ad.Usuario)
                .HasForeignKey<DatosMedico>(ad => ad.UsuarioId);

            builder.Entity<Usuario>()
                   .HasOne<Clinica>(s => s.Clinica)
                   .WithMany(g => g.Usuarios)
                   .HasForeignKey(s => s.ClinicaId);

            builder.Entity<Usuario>()
                   .HasOne<Perfil>(s => s.Perfil)
                   .WithMany(g => g.Usuarios)
                   .HasForeignKey(s => s.PerfilId);

            builder.Entity<Especialidad>()
                   .HasOne<Clinica>(s => s.Clinica)
                   .WithMany(g => g.Especialidads)
                   .HasForeignKey(s => s.ClinicaId);

            builder.Entity<Sucursal>()
                   .HasOne<Clinica>(s => s.Clinica)
                   .WithMany(g => g.Sucursals)
                   .HasForeignKey(s => s.ClinicaId);

            builder.Entity<AtributoRol>()
                   .HasOne(s => s.RolFather)
                   .WithMany(g => g.RolesHijos)
                   .HasForeignKey(s => s.FatherId);

            builder.Entity<Usuario>()
                   .HasOne(a => a.DatosEmergenciaUsuario)
                   .WithOne(b => b.Usuario)
                   .HasForeignKey<DatosEmergenciaUsuario>(s => s.UsuarioId);

            builder.Entity<Distrito>()
                   .HasOne(a => a.Provincia)
                   .WithMany(b => b.Distritos)
                   .HasForeignKey(s => s.ProvinciaId);

            builder.Entity<Provincia>()
                   .HasOne(a => a.Departamento)
                   .WithMany(b => b.Provincias)
                   .HasForeignKey(s => s.DepartamentoId);

            builder.Entity<Departamento>()
                   .HasOne(a => a.Pais)
                   .WithMany(b => b.Departamentos)
                   .HasForeignKey(s => s.PaisId);

            builder.Entity<Clinica>()
                   .HasOne(a => a.Distrito)
                   .WithMany(b => b.Clinicas)
                   .HasForeignKey(s => s.DistritoId);

            builder.Entity<Usuario>()
                   .HasOne(a => a.Distrito)
                   .WithMany(b => b.Usuarios)
                   .HasForeignKey(s => s.DistritoId);

            builder.Entity<Tipo_Horario>()
                   .HasOne(a => a.Clinica)
                   .WithMany(b => b.Tipo_Horarios)
                   .HasForeignKey(s => s.ClinicaId);

            base.OnModelCreating(builder);
        }
    }
}