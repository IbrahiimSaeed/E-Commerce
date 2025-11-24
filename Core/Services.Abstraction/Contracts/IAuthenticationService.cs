using Shared.Dtos.IdentityModule;
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
    }
}
