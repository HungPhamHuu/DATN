using AppAPI.Extensions.DependencyInjection;
using AppData.DB_Context;
using AppData.Repositories.BillDetails;
using AppData.Repositories.Bills;
using AppData.Repositories.Cart;
using AppData.Repositories.CartDetail;
using AppData.Repositories.Color;
using AppData.Repositories.Product;
using AppData.Repositories.ProductDetail;
using AppData.Repositories.RefreshTokens;
using AppData.Repositories.Roles;
using AppData.Repositories.Sizes;
using AppData.Repositories.TH;
using AppData.Repositories.Users;
using AppData.Repositories.VoucherDetails;
using AppData.Repositories.Vouchers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection")));
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
	{
		Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
		In = ParameterLocation.Header,
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey
	});

	options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins", policy =>
	{
		policy.WithOrigins("https://localhost:7265").AllowAnyMethod().AllowAnyHeader();
	})
);

builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<ICartDetailRepository, CartDetailRepository>();
builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IVoucherDetailRepository, VoucherDetailRepository>();
//builder.Services.AddTransient<IVoucherRepository, VoucherRepository>();
builder.Services.AddTransient<IBillRepository, BillRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductDetailRepository, ProductDetailRepository>();
builder.Services.AddTransient<IBillDetailRepository, BillDetailRepository>();
builder.Services.AddTransient<IMauSacRepository, MauSacRepository>();
builder.Services.AddTransient<ISizeRepository, SizeRepository>();
builder.Services.AddTransient<IThuongHieuRepository, ThuongHieuRepository>();
//builder.Services.AddTransient<IChatLieuServiece, ChatLieuServiece>();
//builder.Services.AddTransient<ISanPhamChiTietServiece, SanPhamChiTietServiece>();
//builder.Services.AddTransient<IVoucherServices, VoucherServices>();
//builder.Services.AddTransient<IVoucherDetailServices, VoucherDetailServices>();
//builder.Services.AddTransient<IHinhThucThanhToanServices, HinhThucThanhToanServiece>();
//builder.Services.AddTransient<INguoiDungServiece, NguoiDungServiece>();
//builder.Services.AddTransient<IQuyenService, QuyenService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("NgOrigins");
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
