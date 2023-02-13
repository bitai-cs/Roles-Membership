namespace NetSqlAzMan.CustomDataLayer.EFCF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class identity_UserValidationRequest
    {
        [Key]
        [StringLength(255)]
        public string userValRequestId { get; set; }

        public DateTime valRequestDate { get; set; }

        [StringLength(50)]
        public string ldapDomain { get; set; }

        [Required]
        [StringLength(255)]
        public string userName { get; set; }

        [StringLength(255)]
        public string clientApplication { get; set; }

        [StringLength(255)]
        public string azManStore { get; set; }

        [StringLength(255)]
        public string azManAplication { get; set; }

        [StringLength(255)]
        public string azManItem { get; set; }

        public int? UserID { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual identity_User identity_User { get; set; }
    }
}
