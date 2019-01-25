using EventPlanning.BLL.DTO;
using EventPlanning.BLL.Infrastructure;
using EventPlanning.BLL.Interfaces;
using EventPlanning.DAL.Entities;
using EventPlanning.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventPlanning.BLL.Services
{
    class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }



        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email, Password = userDto.Password, EmailConfirmed = userDto.ConfirmedEmail };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                userDto.Id = user.Id;
                ClientProfile clientProfile = new ClientProfile { Id = user.Id };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Registration complete", "");
            }
            else
            {
                return new OperationDetails(false, "User with this login already exists", "Email");
            }
        }


        public bool IsEmailConfirmed(string email)
        {
            ApplicationUser user = Database.UserManager.FindByEmail(email);
            return user.EmailConfirmed;
        }


        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }


        public async Task MailConfirmation(UserDTO userDTO)
        {
            var user = Database.UserManager.FindById(userDTO.Id);
            user.EmailConfirmed = true;
            await Database.UserManager.UpdateAsync(user);
        }


        public UserDTO FindById(string id)
        {
            ApplicationUser user = Database.UserManager.FindById(id);
            var userDTO = AutoMapper.Mapper.Map<ApplicationUser, UserDTO>(user);
            return userDTO;
        }


        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }


        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
