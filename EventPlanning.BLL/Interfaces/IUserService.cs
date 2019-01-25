using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EventPlanning.BLL.DTO;
using EventPlanning.BLL.Infrastructure;

namespace EventPlanning.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task MailConfirmation(UserDTO userDTO);
        UserDTO FindById(string id);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        bool IsEmailConfirmed(string mail);
    }
}
