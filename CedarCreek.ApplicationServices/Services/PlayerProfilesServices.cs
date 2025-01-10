using CedarCreek.Core.Domain;
using CedarCreek.Core.ServiceInterface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedarCreek.ApplicationServices.Services
{
    public class PlayerProfilesServices : IPlayerProfilesServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountsServices _accountsServices;
    }

    public PlayerProfileServices
    {
        (
            UserManager<ApplicationUser> userManager,
            IAccountsServices = accountsServices
        )
    }
}
