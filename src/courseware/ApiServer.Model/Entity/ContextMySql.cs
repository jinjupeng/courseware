using Microsoft.EntityFrameworkCore;

namespace ApiServer.Model.Entity
{
    public partial class ContextMySql : DbContext
    {
        public ContextMySql()
        {
        }

        public ContextMySql(DbContextOptions<ContextMySql> options)
            : base(options)
        {
        }

        public virtual DbSet<cw_courseware> cw_courseware { get; set; }
        public virtual DbSet<cw_exchange_key> cw_exchange_key { get; set; }
        public virtual DbSet<cw_order> cw_order { get; set; }
        public virtual DbSet<cw_user_courseware> cw_user_courseware { get; set; }
        public virtual DbSet<sys_role> sys_role { get; set; }
        public virtual DbSet<sys_role_permission> sys_role_permission { get; set; }
        public virtual DbSet<sys_user> sys_user { get; set; }
        public virtual DbSet<sys_users_roles> sys_users_roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=courseware;uid=root;pwd=123456;allow user variables=True", x => x.ServerVersion("8.0.22-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cw_courseware>(entity =>
            {
                entity.Property(e => e.carousel_url)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.count).HasDefaultValueSql("'0'");

                entity.Property(e => e.cover)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.create_time)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.is_carousel).HasDefaultValueSql("'0'");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.price).HasColumnType("decimal(10,2)");

                entity.Property(e => e.url)
                    .HasColumnType("varchar(10000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<cw_exchange_key>(entity =>
            {
                entity.HasIndex(e => e.cw_id)
                    .HasName("cw_exchange_key_cw_courseware_id_fk");

                entity.HasIndex(e => e.user_id)
                    .HasName("cw_exchange_key_user_id_fk");

                entity.Property(e => e.create_time)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ex_key)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.is_used).HasDefaultValueSql("'0'");

                entity.Property(e => e.use_time).HasColumnType("timestamp");

                entity.HasOne(d => d.cw_)
                    .WithMany(p => p.cw_exchange_key)
                    .HasForeignKey(d => d.cw_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cw_exchange_key_cw_courseware_id_fk");

                entity.HasOne(d => d.user_)
                    .WithMany(p => p.cw_exchange_key)
                    .HasForeignKey(d => d.user_id)
                    .HasConstraintName("cw_exchange_key_user_id_fk");
            });

            modelBuilder.Entity<cw_order>(entity =>
            {
                entity.HasIndex(e => e.cw_id)
                    .HasName("cw_order_cw_courseware_id_fk");

                entity.HasIndex(e => e.order_sn)
                    .HasName("cw_order_order_sn_index");

                entity.HasIndex(e => e.user_id)
                    .HasName("cw_order_user_id_fk");

                entity.Property(e => e.create_time)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.is_pay).HasDefaultValueSql("'0'");

                entity.Property(e => e.order_sn)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.pay_time).HasColumnType("timestamp");

                entity.Property(e => e.pay_type).HasComment("0->小程序");

                entity.Property(e => e.price).HasColumnType("decimal(10,2)");

                entity.Property(e => e.wx_order)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.cw_)
                    .WithMany(p => p.cw_order)
                    .HasForeignKey(d => d.cw_id)
                    .HasConstraintName("cw_order_cw_courseware_id_fk");

                entity.HasOne(d => d.user_)
                    .WithMany(p => p.cw_order)
                    .HasForeignKey(d => d.user_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cw_order_user_id_fk");
            });

            modelBuilder.Entity<cw_user_courseware>(entity =>
            {
                entity.HasIndex(e => e.cw_id)
                    .HasName("cw_user_courseware_cw_courseware_id_fk");

                entity.HasIndex(e => e.user_id)
                    .HasName("cw_user_courseware_user_id_fk");

                entity.Property(e => e.create_time)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.cw_)
                    .WithMany(p => p.cw_user_courseware)
                    .HasForeignKey(d => d.cw_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cw_user_courseware_cw_courseware_id_fk");

                entity.HasOne(d => d.user_)
                    .WithMany(p => p.cw_user_courseware)
                    .HasForeignKey(d => d.user_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cw_user_courseware_user_id_fk");
            });

            modelBuilder.Entity<sys_role>(entity =>
            {
                entity.HasKey(e => e.role_id)
                    .HasName("PRIMARY");

                entity.HasComment("角色表");

                entity.HasIndex(e => e.name)
                    .HasName("role_name_index")
                    .IsUnique();

                entity.Property(e => e.role_id).HasComment("ID");

                entity.Property(e => e.create_by)
                    .HasColumnType("varchar(255)")
                    .HasComment("创建者")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.create_time)
                    .HasColumnType("datetime")
                    .HasComment("创建日期");

                entity.Property(e => e.data_scope)
                    .HasColumnType("varchar(255)")
                    .HasComment("数据权限")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.description)
                    .HasColumnType("varchar(255)")
                    .HasComment("描述")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.level).HasComment("角色级别");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasComment("名称")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.update_by)
                    .HasColumnType("varchar(255)")
                    .HasComment("更新者")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.update_time)
                    .HasColumnType("datetime")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<sys_role_permission>(entity =>
            {
                entity.HasIndex(e => e.role_id)
                    .HasName("sys_role_permission_sys_role_role_id_fk");

                entity.Property(e => e.permission)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<sys_user>(entity =>
            {
                entity.HasIndex(e => e.username)
                    .HasName("user_username_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.uuid)
                    .HasName("user_uuid_uindex")
                    .IsUnique();

                entity.Property(e => e.background)
                    .HasColumnType("varchar(255)")
                    .HasComment("背景图片")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.gender)
                    .IsRequired()
                    .HasColumnType("enum('男','女','保密')")
                    .HasDefaultValueSql("'保密'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.nickname)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.password)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.phone_number)
                    .HasColumnType("varchar(11)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.portrait)
                    .HasColumnType("varchar(255)")
                    .HasComment("头像")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.username)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.uuid)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<sys_users_roles>(entity =>
            {
                entity.HasKey(e => new { e.user_id, e.role_id })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasComment("用户角色关联");

                entity.HasIndex(e => e.role_id)
                    .HasName("FKq4eq273l04bpu4efj0jd0jb98");

                entity.Property(e => e.user_id).HasComment("用户ID");

                entity.Property(e => e.role_id).HasComment("角色ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
