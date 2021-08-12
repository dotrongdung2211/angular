using hrmSolution.Models.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hrmSolution.Services.SystemServices.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginRequest request);
    }
}
