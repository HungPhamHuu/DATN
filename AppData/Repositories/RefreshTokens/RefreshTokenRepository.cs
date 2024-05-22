using AppCommon.RepositoryAsync;
using AppData.DB_Context;
using AppData.Entities.Models;

namespace AppData.Repositories.RefreshTokens;

public class RefreshTokenRepository : RepositoryAsync<RefreshTokenEntity>, IRefreshTokenRepository
{
	private readonly ApplicationDbContext _context;
	public RefreshTokenRepository(ApplicationDbContext context) : base(context, context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}
}