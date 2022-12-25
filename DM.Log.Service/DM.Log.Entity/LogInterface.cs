

namespace DM.Log.Dal
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using DM.BaseEntity;


    public class LogInterface : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Comment("Scenario Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        [Comment("Scenario Description")]
        public string Description { get; set; }

    }
}
