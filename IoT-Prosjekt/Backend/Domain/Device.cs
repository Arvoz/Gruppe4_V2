﻿namespace Backend.Domain
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool State { get; set; }
        public bool Paired { get; set; }

        public void ChangeState(bool state)
        {
            State = state;
        }

        public void ChangePaired(bool state)
        {
            Paired = state;
        }
    }
}