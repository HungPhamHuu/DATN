using System.Reflection;
using System.Text;
using AppCommon.RepositoryAsync;
using AppData.DB_Context;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AppAPI.Extensions.DependencyInjection;

public static class ServiceCollection
{
	public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
	{
		var executingAssembly = Assembly.GetExecutingAssembly();
		var entryAssembly = Assembly.GetEntryAssembly();

		services.AddAutoMapper(configuration => { configuration.AddExpressionMapping(); }, executingAssembly,
			entryAssembly);
		//services.AddAuthorization();

		services.AddAuthentication(
				options => //được sử dụng để cấu hình xác thực trong ứng dụng và thiết lập chế độ xác thực và thách thức mặc định cho JWT bearer authentication.
				{
					// options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					// options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
					// options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
					//options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
					// This forces challenge results to be handled by Google OpenID Handler, so there's no
					// need to add an AccountController that emits challenges for Login.
					//options.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
					//// This forces forbid results to be handled by Google OpenID Handler, which checks if
					//// extra scopes are required and does automatic incremental auth.
					//options.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
					//// Default scheme that will handle everything else.
					//// Once a user is authenticated, the OAuth2 token info is stored in cookies.
					// options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

					options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				}
			).AddJwtBearer(options =>
			{
				options.SaveToken =
					true; //Một giá trị boolean xác định liệu có nên lưu token nhận được trong vé xác thực vào AuthenticationProperties sau khi xác thực thành công hay không.
						  //options.RequireHttpsMetadata = false; // Một giá trị boolean xác định liệu middleware có yêu cầu HTTPS để truy cập điểm cuối xác thực hay không.
				options.TokenValidationParameters =
					new TokenValidationParameters() //xác định các tham số được sử dụng để xác thực token JWT
					{
						ValidateIssuerSigningKey = true,
						ValidateIssuer = false, // Một giá trị boolean xác định liệu nên xác thực nhà cung cấp token (issuer) hay không.
						ValidateAudience = false, // Một giá trị boolean xác định liệu nên xác thực khán giả (audience) của token hay không.
						ValidAudience = configuration["Authentication:Jwt:ValidAudience"], //Một chuỗi giá trị xác định khán giả hợp lệ cho token.
						ValidIssuer = configuration["Authentication:Jwt:ValidIssuer"], // Một chuỗi giá trị xác định nhà cung cấp token hợp lệ cho token.
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Jwt:Secret"])), //Một đối tượng SymmetricSecurityKey chứa khóa bí mật được sử dụng để ký token.
						RequireExpirationTime = false, // Một giá trị boolean xác định liệu nên yêu cầu token có thời gian hết hạn hay không.

					};
			});

		services.AddDbContext<ApplicationDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("MSSQLConnection")));
		//services.AddDbContext<ApplicationDbContext>(c => c.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection")));
		services.AddHttpContextAccessor();


		services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>)); //chỉ dùng cho class Gennerric


		services.AddTransient<IAuthenticationService, AuthenticationService>();


		return services;

	}
}