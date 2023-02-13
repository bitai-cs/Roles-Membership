namespace NetSqlAzMan.CustomDataLayer.EFCF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class identity_Login
    {
        [Key]
        [StringLength(255)]
        public string LoginId { get; set; }

        [Required]
        [StringLength(255)]
        public string AppName { get; set; }

        [StringLength(50)]
        public string LDAPDomain { get; set; }

        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string RemoteIpV4 { get; set; }

        public DateTime LogIn { get; set; }

        public DateTime Expires { get; set; }

        public int Timeout { get; set; }

        public DateTime? Expiration { get; set; }

        public bool Expired { get; set; }

        public DateTime? LogOff { get; set; }

        public bool LoggedOut { get; set; }

        public bool Renovated { get; set; }

        public DateTime? Renewal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? Expiration_Renewal { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
