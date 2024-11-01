﻿using Backend.Domain;

namespace Backend.Ports
{
    public interface ILightRepository
    {
        Task<List<Light>> GetAllDevices();
        Task<Light> GetDeviceById(int id);
        Task AddDevice(Light light);
        Task DeleteDevice(int id);
    }
}
