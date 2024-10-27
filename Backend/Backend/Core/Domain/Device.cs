namespace Backend.Core.Domain
{
    public class Device
    {
        public int Id { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool Status { get; set; }

        public Device(string name, string type, bool status)
        {
            DeviceName = name;
            Type = type;
            Status = status;
        }

        public Device() 
        {
        }

        public void UpdateStatus(bool status) 
        {
            Status = status;
        }

    }
}