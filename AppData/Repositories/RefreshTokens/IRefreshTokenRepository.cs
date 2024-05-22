using AppCommon.RepositoryAsync;
using AppData.Entities.Models;

namespace AppData.Repositories.RefreshTokens;

public interface IRefreshTokenRepository   : IRepositoryAsync<RefreshTokenEntity>
{
	
}