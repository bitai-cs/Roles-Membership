namespace NetSqlAzMan.CustomDataLayer.EFCF {
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
	using EntityFramework.Functions;

	public partial class AzManEntities : DbContext {
		public AzManEntities()
			 : base("name=AzManEntitiesDesign") {
		}

		public virtual DbSet<identity_BranchOffice> identity_BranchOffice { get; set; }
		public virtual DbSet<identity_Department> identity_Department { get; set; }
		public virtual DbSet<identity_LDAPConfig> identity_LDAPConfig { get; set; }
		public virtual DbSet<ldapwac_DomainProfile> ldapwac_DomainProfile { get; set; }
		public virtual DbSet<ldapwac_ServerBaseDN> ldapwac_ServerBaseDN { get; set; }
		public virtual DbSet<identity_Login> identity_Login { get; set; }
		public virtual DbSet<identity_User> identity_User { get; set; }
		public virtual DbSet<identity_UserBranchOffice> identity_UserBranchOffice { get; set; }
		public virtual DbSet<identity_UserValidationRequest> identity_UserValidationRequest { get; set; }
		public virtual DbSet<identity_vw_UserBranchOffices> identity_vw_UserBranchOffices { get; set; }
		public virtual DbSet<identity_vw_Users> identity_vw_Users { get; set; }
		public virtual DbSet<identity_vw_UsersByDepartment> identity_vw_UsersByDepartment { get; set; }
		public virtual DbSet<identity_vw_WrongItemsHierarchyView> identity_vw_WrongItemsHierarchyView { get; set; }
		public virtual DbSet<ldapwac_vw_DomainProfilesWithConfiguredBaseDNs> ldapwac_vw_DomainProfilesWithConfiguredBaseDNs { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Entity<identity_vw_UserBranchOffices>()
				 .Property(e => e.BranchOfficeId)
				 .IsUnicode(false);

			modelBuilder.Entity<identity_vw_UserBranchOffices>()
				 .Property(e => e.RelativeBranchOfficeId)
				 .IsUnicode(false);

			modelBuilder.Entity<identity_vw_Users>()
				 .Property(e => e.RowVersion)
				 .IsFixedLength();

			modelBuilder.Entity<identity_BranchOffice>()
				 .Property(e => e.BranchOfficeId)
				 .IsUnicode(false);

			modelBuilder.Entity<identity_BranchOffice>()
				 .Property(e => e.RelativeBranchOfficeId)
				 .IsUnicode(false);

			modelBuilder.Entity<identity_BranchOffice>()
				 .Property(e => e.RowVersion)
				 .IsFixedLength();

			modelBuilder.Entity<identity_BranchOffice>()
				 .HasMany(e => e.identity_BranchOffice1)
				 .WithRequired(e => e.identity_BranchOffice2)
				 .HasForeignKey(e => e.RelativeBranchOfficeId);

			modelBuilder.Entity<identity_BranchOffice>()
				 .HasMany(e => e.identity_UserBranchOffice)
				 .WithRequired(e => e.identity_BranchOffice)
				 .WillCascadeOnDelete(false);

			modelBuilder.Entity<identity_Department>()
				 .Property(e => e.RowVersion)
				 .IsFixedLength();

			modelBuilder.Entity<identity_LDAPConfig>()
				 .Property(e => e.RowVersion)
				 .IsFixedLength();

			modelBuilder.Entity<identity_Login>()
				 .Property(e => e.RowVersion)
				 .IsFixedLength();

			modelBuilder.Entity<identity_User>()
				 .Property(e => e.RowVersion)
				 .IsFixedLength();

			modelBuilder.Entity<identity_User>()
				 .HasMany(e => e.identity_UserBranchOffice)
				 .WithRequired(e => e.identity_User)
				 .WillCascadeOnDelete(false);

			modelBuilder.Entity<identity_UserBranchOffice>()
				 .Property(e => e.BranchOfficeId)
				 .IsUnicode(false);

			modelBuilder.Entity<identity_UserBranchOffice>()
				 .Property(e => e.RowVersion)
				 .IsFixedLength();

			modelBuilder.Entity<identity_UserValidationRequest>()
				 .Property(e => e.RowVersion)
				 .IsFixedLength();

			modelBuilder.Entity<ldapwac_DomainProfile>()
				 .Property(e => e.DomainProfile)
				 .IsUnicode(false);

			modelBuilder.Entity<ldapwac_DomainProfile>()
				 .Property(e => e.LdapProxyWebApiUri)
				 .IsUnicode(false);

			modelBuilder.Entity<ldapwac_DomainProfile>()
				 .Property(e => e.LdapServerProfile)
				 .IsUnicode(false);

			modelBuilder.Entity<ldapwac_DomainProfile>()
				 .HasMany(e => e.ldapwac_ServerBaseDN)
				 .WithRequired(e => e.ldapwac_DomainProfile)
				 .WillCascadeOnDelete(false);

			modelBuilder.Entity<ldapwac_ServerBaseDN>()
				 .Property(e => e.BaseDN)
				 .IsUnicode(false);

			modelBuilder.Entity<ldapwac_ServerBaseDN>()
				 .Property(e => e.RowVersion)
				 .IsFixedLength();

			modelBuilder.Entity<ldapwac_vw_DomainProfilesWithConfiguredBaseDNs>()
				 .Property(e => e.DomainProfile)
				 .IsUnicode(false);

			modelBuilder.Entity<ldapwac_vw_DomainProfilesWithConfiguredBaseDNs>()
				 .Property(e => e.LdapProxyWebApiUri)
				 .IsUnicode(false);

			modelBuilder.Entity<ldapwac_vw_DomainProfilesWithConfiguredBaseDNs>()
				 .Property(e => e.LdapServerProfile)
				 .IsUnicode(false);

			modelBuilder.Entity<ldapwac_vw_DomainProfilesWithConfiguredBaseDNs>()
				 .Property(e => e.BaseDN)
				 .IsUnicode(false);

			modelBuilder.Entity<ldapwac_vw_DomainProfilesWithConfiguredBaseDNs>()
				 .Property(e => e.RowVersion)
				 .IsFixedLength();


			// Add functions on AdventureWorks to entity model.
			modelBuilder.Conventions.Add(new FunctionConvention<AzManEntities>());
			//// Add all complex types used by functions.
			//modelBuilder.ComplexType<ContactInformation>();
			//modelBuilder.ComplexType<ManagerEmployee>();			
		}
	}
}
