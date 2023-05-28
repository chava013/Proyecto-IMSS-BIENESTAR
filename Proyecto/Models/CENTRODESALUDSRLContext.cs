using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Proyecto.Models
{
    public partial class CENTRODESALUDSRLContext : DbContext
    {
        public CENTRODESALUDSRLContext()
        {
        }

        public CENTRODESALUDSRLContext(DbContextOptions<CENTRODESALUDSRLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Descrippaciente> Descrippacientes { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Especialidad> Especialidads { get; set; }
        public virtual DbSet<Medicamento> Medicamentos { get; set; }
        public virtual DbSet<Medicreceta> Medicrecetas { get; set; }
        public virtual DbSet<Nommedicamento> Nommedicamentos { get; set; }
        public virtual DbSet<Paciente> Pacientes { get; set; }
        public virtual DbSet<Pacientedoctor> Pacientedoctors { get; set; }
        public virtual DbSet<Recetum> Receta { get; set; }
        public virtual DbSet<Tipomed> Tipomeds { get; set; }
        public virtual DbSet<Turnodoc> Turnodocs { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-BPSUMHH\\MYSSQLSERVER2;DataBase=CENTRODESALUDSRL;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Descrippaciente>(entity =>
            {
                entity.HasKey(e => e.Clvdescripaciente)
                    .HasName("PK__DESCRIPP__80073BD01CDC7C88");

                entity.ToTable("DESCRIPPACIENTE");

                entity.Property(e => e.Clvdescripaciente).HasColumnName("CLVDESCRIPACIENTE");

                entity.Property(e => e.Clvpaciente).HasColumnName("CLVPACIENTE");

                entity.Property(e => e.Descpaciente)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCPACIENTE");

                entity.HasOne(d => d.ClvpacienteNavigation)
                    .WithMany(p => p.Descrippacientes)
                    .HasForeignKey(d => d.Clvpaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DESCRIPPA__CLVPA__1CF15040");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.Clvdoctor)
                    .HasName("PK__DOCTOR__8A655AA0D32F239F");

                entity.ToTable("DOCTOR");

                entity.Property(e => e.Clvdoctor).HasColumnName("CLVDOCTOR");

                entity.Property(e => e.Clvespecialidad).HasColumnName("CLVESPECIALIDAD");

                entity.Property(e => e.Clvturno).HasColumnName("CLVTURNO");

                entity.Property(e => e.Nomdoctor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMDOCTOR");

                entity.HasOne(d => d.ClvespecialidadNavigation)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.Clvespecialidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DOCTOR__CLVESPEC__267ABA7A");

                entity.HasOne(d => d.ClvturnoNavigation)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.Clvturno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DOCTOR__CLVTURNO__25869641");
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasKey(e => e.Clvespecialidad)
                    .HasName("PK__ESPECIAL__E13E9CC1F16CB1EF");

                entity.ToTable("ESPECIALIDAD");

                entity.HasIndex(e => e.Nomespecialidad, "UQ__ESPECIAL__7A60EDEA86706A95")
                    .IsUnique();

                entity.Property(e => e.Clvespecialidad).HasColumnName("CLVESPECIALIDAD");

                entity.Property(e => e.Nomespecialidad)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("NOMESPECIALIDAD");
            });

            modelBuilder.Entity<Medicamento>(entity =>
            {
                entity.HasKey(e => e.Clvmedicamento)
                    .HasName("PK__MEDICAME__E2841D46419474FB");

                entity.ToTable("MEDICAMENTO");

                entity.Property(e => e.Clvmedicamento).HasColumnName("CLVMEDICAMENTO");

                entity.Property(e => e.Clvnommedicamento).HasColumnName("CLVNOMMEDICAMENTO");

                entity.Property(e => e.Clvtipo).HasColumnName("CLVTIPO");

                entity.Property(e => e.Contenido)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CONTENIDO");

                entity.Property(e => e.Existencias).HasColumnName("EXISTENCIAS");

                entity.Property(e => e.Fechacaducidad)
                    .HasColumnType("date")
                    .HasColumnName("FECHACADUCIDAD");

                entity.HasOne(d => d.ClvnommedicamentoNavigation)
                    .WithMany(p => p.Medicamentos)
                    .HasForeignKey(d => d.Clvnommedicamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MEDICAMEN__CLVNO__164452B1");

                entity.HasOne(d => d.ClvtipoNavigation)
                    .WithMany(p => p.Medicamentos)
                    .HasForeignKey(d => d.Clvtipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MEDICAMEN__CLVTI__173876EA");
            });

            modelBuilder.Entity<Medicreceta>(entity =>
            {
                entity.HasKey(e => e.Clvmedicrecetas)
                    .HasName("PK__MEDICREC__F7F8CCBB2DFB5CF0");

                entity.ToTable("MEDICRECETAS");

                entity.Property(e => e.Clvmedicrecetas).HasColumnName("CLVMEDICRECETAS");

                entity.Property(e => e.Clvmedicamento).HasColumnName("CLVMEDICAMENTO");

                entity.Property(e => e.Clvreceta).HasColumnName("CLVRECETA");

                entity.Property(e => e.Descreceta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRECETA");

                entity.HasOne(d => d.ClvmedicamentoNavigation)
                    .WithMany(p => p.Medicreceta)
                    .HasForeignKey(d => d.Clvmedicamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MEDICRECE__CLVME__31EC6D26");

                entity.HasOne(d => d.ClvrecetaNavigation)
                    .WithMany(p => p.Medicreceta)
                    .HasForeignKey(d => d.Clvreceta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MEDICRECE__CLVRE__32E0915F");
            });

            modelBuilder.Entity<Nommedicamento>(entity =>
            {
                entity.HasKey(e => e.Clvnommedicamento)
                    .HasName("PK__NOMMEDIC__2E16469091D0C4A7");

                entity.ToTable("NOMMEDICAMENTO");

                entity.HasIndex(e => e.Nommedicamento1, "UQ__NOMMEDIC__DDB452DCC5FD67EC")
                    .IsUnique();

                entity.Property(e => e.Clvnommedicamento).HasColumnName("CLVNOMMEDICAMENTO");

                entity.Property(e => e.Nommedicamento1)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("NOMMEDICAMENTO");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.Clvpaciente)
                    .HasName("PK__PACIENTE__C223A112079159D9");

                entity.ToTable("PACIENTE");

                entity.Property(e => e.Clvpaciente).HasColumnName("CLVPACIENTE");

                entity.Property(e => e.Nompaciente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMPACIENTE");

                entity.Property(e => e.Procedencia)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PROCEDENCIA");
            });

            modelBuilder.Entity<Pacientedoctor>(entity =>
            {
                entity.HasKey(e => e.Clvpacientedoctor)
                    .HasName("PK__PACIENTE__5BF855C605494EDC");

                entity.ToTable("PACIENTEDOCTOR");

                entity.Property(e => e.Clvpacientedoctor).HasColumnName("CLVPACIENTEDOCTOR");

                entity.Property(e => e.Clvdoctor).HasColumnName("CLVDOCTOR");

                entity.Property(e => e.Clvpaciente).HasColumnName("CLVPACIENTE");

                entity.HasOne(d => d.ClvdoctorNavigation)
                    .WithMany(p => p.Pacientedoctors)
                    .HasForeignKey(d => d.Clvdoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PACIENTED__CLVDO__2A4B4B5E");

                entity.HasOne(d => d.ClvpacienteNavigation)
                    .WithMany(p => p.Pacientedoctors)
                    .HasForeignKey(d => d.Clvpaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PACIENTED__CLVPA__29572725");
            });

            modelBuilder.Entity<Recetum>(entity =>
            {
                entity.HasKey(e => e.Clvreceta)
                    .HasName("PK__RECETA__DB4EA496D810CD62");

                entity.ToTable("RECETA");

                entity.Property(e => e.Clvreceta).HasColumnName("CLVRECETA");

                entity.Property(e => e.Clvdoctor).HasColumnName("CLVDOCTOR");

                entity.Property(e => e.Clvpaciente).HasColumnName("CLVPACIENTE");

                entity.Property(e => e.Fechaelab)
                    .HasColumnType("date")
                    .HasColumnName("FECHAELAB")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ClvdoctorNavigation)
                    .WithMany(p => p.Receta)
                    .HasForeignKey(d => d.Clvdoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RECETA__CLVDOCTO__2E1BDC42");

                entity.HasOne(d => d.ClvpacienteNavigation)
                    .WithMany(p => p.Receta)
                    .HasForeignKey(d => d.Clvpaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RECETA__CLVPACIE__2F10007B");
            });

            modelBuilder.Entity<Tipomed>(entity =>
            {
                entity.HasKey(e => e.Clvtipo)
                    .HasName("PK__TIPOMED__15731153B1984AE0");

                entity.ToTable("TIPOMED");

                entity.HasIndex(e => e.Tipo, "UQ__TIPOMED__B6FCAAA22C39E793")
                    .IsUnique();

                entity.Property(e => e.Clvtipo).HasColumnName("CLVTIPO");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TIPO");
            });

            modelBuilder.Entity<Turnodoc>(entity =>
            {
                entity.HasKey(e => e.Clvturno)
                    .HasName("PK__TURNODOC__84EA0ABDEB257B88");

                entity.ToTable("TURNODOC");

                entity.HasIndex(e => e.Turno, "UQ__TURNODOC__63BCBF38C35FD9C0")
                    .IsUnique();

                entity.Property(e => e.Clvturno).HasColumnName("CLVTURNO");

                entity.Property(e => e.Turno)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("TURNO");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Clvdoctor)
                    .HasName("PK__USUARIO__8A655AA0B595DB46");

                entity.ToTable("USUARIO");

                entity.Property(e => e.Clvdoctor)
                    .ValueGeneratedNever()
                    .HasColumnName("CLVDOCTOR");

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CONTRASEÑA");

                entity.Property(e => e.Nomusuario)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMUSUARIO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
