using System;
using System.Collections.Generic;
using System.Text;
using Teleport.Model.Models;
using Teleport.Service.Interfaces;
using Teleport.Data.Entity;
using Teleport.Data.Repository;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Teleport.Model.Models.ApiResultModels;
using Mapster;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Teleport.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly AppSettings _appSettings;
        public UserService(IRepository<User> userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }
        public async Task<Result> RegisterAsync(UserRegisterModel userModel)
        {
            try
            {
                var existingUser = _userRepository.Queryable().FirstOrDefault(x => x.UserName == userModel.UserName);
                if (existingUser != null)
                {
                    return new FailureResult("User is already registered.");
                }
                
                var userEnt = userModel.Adapt<User>();
                userEnt.DateCreated = DateTime.Now;
                userEnt.Password = getHashPassword(userModel.Password);
                
                //insert user
                await _userRepository.InsertAsync(userEnt);
                var responseModel = userEnt.Adapt<UserModel>();
                
                //after save user login process and update token coloumn.
                var tokenResult = await AuthenticateAsync(userModel.UserName, userModel.Password);
                string token = (tokenResult.Root as AuthModel).Token;
                await UpdateUserTokenAsync(userEnt, token);
                responseModel.UserToken = token;
                responseModel.Password = ""; // for security
                return new SuccessResult(responseModel, "User register process is successful.");
            }
            catch (Exception ex)
            {
                return new FailureResult("An error has been occurred ex message : " + ex.Message);
            }
        }
        public async Task<Result> AuthenticateAsync(string userName, string password)
        {
            try
            {
                var user = _userRepository.Queryable().FirstOrDefault(x => x.UserName == userName);
                if (user == null)
                {
                    return new FailureResult("Invalid login attempt! User is not registered.");
                }               

                bool isPasswordMatched = validatePassword(user, password);
                if (!isPasswordMatched)
                {
                    return new FailureResult("Invalid password!");
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(365),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                await UpdateUserTokenAsync(user, tokenString);

                return new SuccessResult(new AuthModel { Token = tokenString }, "Login attempt is successful");
            }
            catch (Exception ex)
            {
                return new FailureResult("An error has been occurred ex message : " + ex.Message);
            }
        }

        private async Task UpdateUserTokenAsync(User user, string tokenString)
        {
            user.UserToken = tokenString;
            await _userRepository.UpdateAsync(user);
        }

        private bool validatePassword(User user, string password)
        {
            /* Fetch the stored value */
            string savedPasswordHash = user.Password;
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

        private string getHashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public AuthModel Authenticate(string UserName, string Password)
        {
            var user = _userRepository.Queryable().FirstOrDefault(x => (x.UserName == UserName || x.Email == UserName) && x.Password == Password);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(365),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new AuthModel { Token = tokenString };

        }

        public List<UserModel> GetAllUsers()
        {
            var userEntities = _userRepository.Queryable().ToList();
            List<UserModel> userList = userEntities.Adapt<List<UserModel>>();
            return userList;
        }
    }
}
