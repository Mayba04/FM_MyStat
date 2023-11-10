using AutoMapper;
using FM_MyStat.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Services.Users
{
    public class AdministartorService
    {
        private readonly UserManager<Administrator> _userManager;
        private readonly SignInManager<Administrator> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AdministartorService(
                UserManager<Administrator> userManager,
                SignInManager<Administrator> signInManager,
                RoleManager<IdentityRole> roleManager,
                EmailService emailService,
                IMapper mapper,
                IConfiguration configuration
            )
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._emailService = emailService;
            this._mapper = mapper;
            this._configuration = configuration;
        }
        
    }
}
