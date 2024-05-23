using AutoMapper;
using Entities.DataTranferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Services.Contrats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly ILogerService _logerService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationManager(ILogerService logerService, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _logerService = logerService;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            var user=_mapper.Map<User>(userForRegistrationDto);

            var result = await _userManager
                .CreateAsync(user, userForRegistrationDto.Password);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user,userForRegistrationDto.Roles);
             
            return result;  
            
        }
    }
}
