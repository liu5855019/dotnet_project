namespace DM.Log.Entity
{
    using BaseEntity;

    public class LogDotaRun : BaseEntity
    {
        public long DeviceId { get; set; }

        public long GroupId { get; set; }

        public bool IsShop { get; set; }
    }
}

