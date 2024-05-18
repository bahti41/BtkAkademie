using AutoMapper;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationManager(ILoggerService logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegisterDTO userForRegisterDTO)
        {
            var user = _mapper.Map<User>(userForRegisterDTO);

            var result = await _userManager.CreateAsync(user,userForRegisterDTO.Password);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegisterDTO.Roles);
            return result;
        }
    }
}
