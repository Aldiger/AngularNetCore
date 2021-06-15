using Architecture.Database;
using Architecture.Domain;
using Architecture.Model;
using DotNetCore.Extensions;
using DotNetCore.Results;
using DotNetCore.Security;
using DotNetCore.Validation;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Architecture.Application
{
    public sealed class AuthService : IAuthService
    {
        private readonly IAuthFactory _authFactory;
        private readonly IAuthRepository _authRepository;
        private readonly IHashService _hashService;
        private readonly IUserRepository _userRepository;
        private readonly IJsonWebTokenService _jsonWebTokenService;
        

        public AuthService
        (
            IAuthFactory authFactory,
            IAuthRepository authRepository,
            IHashService hashService,
            IUserRepository userRepository,
            IJsonWebTokenService jsonWebTokenService
            
        )
        {
            _authFactory = authFactory;
            _authRepository = authRepository;
            _hashService = hashService;
            _userRepository = userRepository;
            _jsonWebTokenService = jsonWebTokenService;
            
        }

        public async Task<IResult<Auth>> AddAsync(AuthModel model)
        {
            var validation = new AuthModelValidator().Validation(model);

            if (validation.Failed) return validation.Fail<Auth>();

            if (await _authRepository.AnyByLoginAsync(model.Login)) return Result<Auth>.Fail("Login exists!");

            var auth = _authFactory.Create(model);

            var password = _hashService.Create(auth.Password, auth.Salt);

            auth.UpdatePassword(password);

            await _authRepository.AddAsync(auth);

            return auth.Success();
        }

        public async Task DeleteAsync(long id)
        {
            await _authRepository.DeleteAsync(id);
        }

        public async Task<IResult<TokenModel>> SignInAsync(SignInModel model)
        {
            var failResult = Result<TokenModel>.Fail("Invalid login or password!");

            var validation = new SignInModelValidator().Validation(model);

            if (validation.Failed) return failResult;

            var auth = await _authRepository.GetByLoginAsync(model.Login);

            if (auth is null) return failResult;

            var password = _hashService.Create(model.Password, auth.Salt);

            if (auth.Password != password) return failResult;

            return await CreateToken(auth);
        }

        private async Task<IResult<TokenModel>> CreateToken(Auth auth)
        {
            var claims = new List<Claim>();

            var user = await _userRepository.GetUserByAuthIdAsync(auth.Id);

            var fullName = $"{user.FirstName} {user.LastName}";

            claims.Add(new Claim(ClaimTypes.Actor, auth.Id.ToString()));

            claims.Add(new Claim(ClaimTypes.Name, fullName));

            claims.AddRoles(new string []{auth.Roles.ToString()});

            var token = _jsonWebTokenService.Encode(claims);

            return new TokenModel(token, fullName,auth.Roles.ToString(), auth.Id.ToString()).Success();
        }
    }
}
