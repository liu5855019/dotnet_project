namespace DM.Log.Entity
{
    using BaseEntity;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    [Index(nameof(DeviceId), nameof(GroupId), nameof(IsShop))]
    public class LogDotaRun : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string DeviceId { get; set; }

        [Required]
        [StringLength(100)]
        public string GroupId { get; set; }

        public bool IsShop { get; set; }
    }
}

