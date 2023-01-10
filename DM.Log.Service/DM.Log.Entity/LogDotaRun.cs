namespace DM.Log.Entity
{
    using BaseEntity;

    public class LogDotaRun : BaseEntity
    {
        public string DeviceId { get; set; }

        public string GroupId { get; set; }

        public bool IsShop { get; set; }
    }
}

