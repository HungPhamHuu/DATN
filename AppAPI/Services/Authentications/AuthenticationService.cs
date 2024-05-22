//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;
//using System.Text.Json;
//using AppAPI.Dtos;
//using AppAPI.ViewModels;
//using AppData.DB_Context;
//using AppData.Entities.Models;
//using AppData.Repositories.RefreshTokens;
//using AppData.Repositories.Roles;
//using AppData.Repositories.Users;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;

//namespace AppAPI.Services.Authentications
//{
//	public class AuthenticationService : IAuthenticationService
//	{
//		private readonly IUserRepository _userRepository;
//		private readonly IRefreshTokenRepository _refreshTokenRepository;
//		private readonly IRoleRepository _roleRepository;
//		private readonly ApplicationDbContext _context;
//		private readonly IConfiguration _configuration;
//		private readonly IHttpContextAccessor _httpContextAccessor;
//		private readonly IMapper _mapper;
//		public Task<UserDto> Login(LoginUserViewModel request)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<UserDto> RegisterUser(CreateUserViewModel request)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<bool> Update(UpdateProfileVM resquest)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<UserDto> RefreshToken()
//		{
//			throw new NotImplementedException();
//		}

//		public string CreateToken( user)
//		{
//			var userRoles = _context.UserEntities.Join(_context.RoleEntities, u => u.Id, r => r.Id, (u, r) => new { user = u, role = r }).Select(p => p.role.RoleName);

//			List<Claim> authClaims = new()
//			{
//				new(ClaimTypes.NameIdentifier, user.Id.ToString()),
//				new(ClaimTypes.Name, user.Account),
//			};
//			foreach (var role in userRoles)
//			{
//				authClaims.Add(new(ClaimTypes.Name, role));
//			}

//			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Jwt:Secret"]));
//			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//			var token = new JwtSecurityToken(
//				issuer: _configuration["Authentication:Jwt:ValidIssuer"],
//				audience: _configuration["Authentication:Jwt:ValidAudience"],
//				claims: authClaims,
//				expires: DateTime.Now.AddHours(Convert.ToInt16(_configuration["Authentication:Jwt:ExpiresTime"])),
//				signingCredentials: creds);
//			var jwt = new JwtSecurityTokenHandler().WriteToken(token);
//			return jwt;
//		}

//		public RefreshTokenDto CreateRefreshToken()
//		{
//			var refreshToken = new RefreshTokenDto
//			{
//				Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
//				Expires = DateTime.Now.AddHours(Convert.ToInt16(_configuration["Authentication:Jwt:ExpiresTime"])),
//				Created = DateTime.Now
//			};
//			Console.WriteLine(JsonSerializer.Serialize(refreshToken));
//			return refreshToken;
//		}

//		public void SetRefreshToken(RefreshTokenDto refreshToken, UserEntity user)
//		{
//			var refreshTokenEntity = new RefreshTokenEntity()
//			{
//				Id = Guid.NewGuid(),
//				UserId = user.Id,
//				IsUsed = false,
//				RefreshToken = refreshToken.Token,
//				Created = refreshToken.Created,
//				Expires = refreshToken.Expires
//			};
//			var cookieOptions = new CookieOptions
//			{
//				HttpOnly = true,
//				Expires = refreshToken.Expires,
//				Secure = true
//			};
//			_httpContextAccessor?.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
//			Console.WriteLine(refreshToken);

//			_refreshTokenRepository.AddAsync(refreshTokenEntity);
//			_refreshTokenRepository.SaveChangesAsync();
//		}

//		public void Logout()
//		{
//			_httpContextAccessor?.HttpContext?.Response.Cookies.Delete("refreshToken");
//		}

//		//   public void UpdateRoles(RoleEntity role)
//		//   {
//		//	_roleManager.UpdateAsync(role);
//		//}

//		public async Task<IList<string>> GetRolesOfUser(UserEntity user)
//		{
//			var userRoles = await _context.UserEntities.Join(_context.RoleEntities, u => u.Id, r => r.Id, (u, r) => new { user = u, role = r }).Where(c => c.user.Id == user.Id).Select(p => p.role.RoleName).ToListAsync();

//			return userRoles;
//		}

//		public async Task<List<RoleEntity>> GetRoles()
//		{
//			var roleCollection = _roleRepository.AsQueryable().ToList();
//			return roleCollection;
//		}
//	}
//}