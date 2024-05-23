using AutoMapper;
using Entities.DataTranferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services.Contrats;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly ILogerService _loger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User? _user;

        public AuthenticationManager(ILogerService logerService, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _loger = logerService;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            var signinCredenttials = GetSiginCredentials();
            var claims = await GetClaims();
            var rokenOptions = GenereteTokenOptions(signinCredenttials,claims);
            return new JwtSecurityTokenHandler().WriteToken(rokenOptions);
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

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto)
        {
            _user=await _userManager.FindByNameAsync(userForAuthDto.UserName);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthDto.Password));
            if (!result)
            {
                _loger.LogWarning($"{nameof(ValidateUser)}: Authentication failed. Wrong username or password.");
            }
            return result;
        }

        private SigningCredentials GetSiginCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
            var secret=new SymmetricSecurityKey(key);

            return new SigningCredentials(secret,SecurityAlgorithms.HmacSha256);
        }
        private async Task <List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            { 
                new Claim(ClaimTypes.Name,_user.UserName)
            };
            var roles =await _userManager
                .GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenereteTokenOptions(SigningCredentials signinCredenttials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken(
                    issuer: jwtSettings["validIssuer"],
                    audience: jwtSettings["validAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                    signingCredentials: signinCredenttials);
                
            return tokenOptions;
        }
    }
}
