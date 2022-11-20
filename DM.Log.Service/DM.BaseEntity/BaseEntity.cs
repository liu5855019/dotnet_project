namespace DM.BaseEntity
{
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity
    {
        public long Id { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? CreateDt { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? UpdateDt { get; set; }

    }
}

