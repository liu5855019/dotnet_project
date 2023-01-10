namespace DM.Log.Entity
{
    using DM.BaseEntity;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;


    public class LogInterface : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Comment("Service Name")]
        public string Service { get; set; }

        [Required]
        [StringLength(100)]
        [Comment("Interface Name")]
        public string Name { get; set; }

        [StringLength(500)]
        [Comment("Remark")]
        public string Remark { get; set; }
    }
}
