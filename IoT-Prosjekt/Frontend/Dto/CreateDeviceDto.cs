namespace Backend.Dto
{
    public class CreateDeviceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Paired { get; set; }
        public bool State { get; set; }
        public int Brightness { get; set; }
    }
}
