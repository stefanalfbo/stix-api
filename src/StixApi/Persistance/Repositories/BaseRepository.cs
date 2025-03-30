using Microsoft.EntityFrameworkCore;
using StixApi.Contracts.Persistance;

namespace StixApi.Persistance.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    protected readonly StixDbContext _context;

    public BaseRepository(StixDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
}
