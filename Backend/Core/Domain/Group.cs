namespace Backend.Core.Domain
{
    public class Group
    {

        public int Id { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public List<Device> Devices { get; set; } = new List<Device>();

    }
}
