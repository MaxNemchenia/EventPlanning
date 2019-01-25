using System;
using EventPlanning.DAL.Entities;

namespace EventPlanning.DAL.Interfaces
{
    public interface IClientManager :IDisposable
    {
        void Create(ClientProfile item);
    }
}
