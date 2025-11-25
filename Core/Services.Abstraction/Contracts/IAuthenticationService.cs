using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contracts
{
    public interface IAuthenticationService
    {
        //Login    ==> Return UserResultDto [DisplayName , Token , Email] ==> take parameters [Email , Password]
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        //register ==> Return UserResultDto [DisplayName , Token , Email] ==> take parameters [Email , Password , PhoneNumber , UserName , DisplayName]
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);

        //Get Current User
        Task<UserResultDto> GetCurrentUserAsync(string userEmail);
        //Check If Email Exist
        Task<bool> CheckEmailExistAsync(string userEmail);
        //Get Address
        Task<AddressDto> GetUserAddressAsync(string userEmail);
        //Update Address
        Task<AddressDto> UpdateUserAddressAsync(string userEmail, AddressDto addressDto);
    }
}
