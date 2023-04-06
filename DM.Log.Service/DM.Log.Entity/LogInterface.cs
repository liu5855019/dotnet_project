namespace DM.Log.Entity
{
    using DM.BaseEntity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.ComponentModel.DataAnnotations;

    [Index(nameof(Service), nameof(RequestPath))]
    public class LogInterface : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Comment("Service Name")]
        public string Service { get; set; }


        [Required]
        [StringLength(100)]
        public string RequestPath { get; set; }

        [StringLength(1000)]
        public string RequestPara { get; set; }

        [StringLength(1000)]
        public string RequestHeader { get; set; }

        public DateTime? RequestDt { get; set; }

        [StringLength(2000)]
        public string Response { get; set; }

        [StringLength(1000)]
        public string ResponseHeader { get; set; }

        public DateTime? ResponseDt { get; set; }


        [StringLength(2000)]
        [Comment("Remark")]
        public string Remark { get; set; }
    }
}
