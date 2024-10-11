using Application.Contracts.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class RobaRepository : IRobaRepository
    {
        private readonly ProjekatContext _context;

        public RobaRepository(ProjekatContext context)
        {
            _context = context;
        }

        public async Task<RobaEntity> AddAsync(RobaEntity roba)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Roba.AddAsync(roba);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return roba;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var roba = await _context.Roba.FindAsync(id);

                    if (roba == null) return false;

                    await _context.Roba.Where(r => r.Id == id).ExecuteDeleteAsync();

                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<IReadOnlyList<RobaEntity>> GetAllAsync()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var roba = await _context.Roba.AsNoTracking().ToListAsync();

                    await transaction.CommitAsync();

                    return roba;

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<IEnumerable<RobaEntity>> GetByNameAsync(string name)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var roba = await _context.Roba.AsNoTracking().Where(r => r.Naziv!.ToLower().Contains(name.ToLower())).ToListAsync();

                    await transaction.CommitAsync();

                    return roba;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<RobaEntity?> UpdateAsync(int id, RobaEntity robaEntity)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var roba = await _context.Roba.FindAsync(id);

                    if (roba == null) return null;

                    await _context.Roba.Where(r => r.Id == id)
                        .ExecuteUpdateAsync(r => r
                                                .SetProperty(r => r.Naziv, robaEntity.Naziv)
                                                .SetProperty(r => r.Cena, robaEntity.Cena));

                    await transaction.CommitAsync();

                    return robaEntity;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
