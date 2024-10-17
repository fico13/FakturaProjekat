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

        public async Task<bool> DeleteAsync(string sifraRobe)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var roba = await _context.Roba.FirstOrDefaultAsync(r => r.SifraRobe == sifraRobe);

                    if (roba == null) return false;

                    await _context.Roba.Where(r => r.SifraRobe == sifraRobe).ExecuteDeleteAsync();

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

        public async Task<IEnumerable<RobaEntity>> GetAllAsync()
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

        public async Task<IEnumerable<RobaEntity>> GetBySifraAsync(string sifraRobe)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var roba = await _context.Roba.AsNoTracking()
                                                  .Where(r => r.SifraRobe!.ToLower().Contains(sifraRobe.ToLower()))
                                                  .ToListAsync();

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

        public async Task<RobaEntity?> UpdateAsync(RobaEntity robaEntity)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var roba = await _context.Roba.FirstOrDefaultAsync(r => r.SifraRobe == robaEntity.SifraRobe);

                    if (roba == null) return null;

                    await _context.Roba.Where(r => r.SifraRobe == roba.SifraRobe)
                                        .ExecuteUpdateAsync(r => r
                                                .SetProperty(r => r.Naziv, robaEntity.Naziv)
                                                .SetProperty(r => r.Cena, robaEntity.Cena)
                                                .SetProperty(r => r.JedinicaMere, robaEntity.JedinicaMere));

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
