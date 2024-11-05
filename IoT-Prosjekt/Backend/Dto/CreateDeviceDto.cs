namespace Backend.Dto
{
    public class CreateDeviceDto
    {
        public string Name { get; set; }
        public bool Paired { get; set; }
        public bool State { get; set; }
    }
}
