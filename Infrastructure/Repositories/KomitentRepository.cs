using Application.Contracts.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class KomitentRepository : IKomitentRepository
    {
        private readonly ProjekatContext _context;

        public KomitentRepository(ProjekatContext context)
        {
            _context = context;
        }

        public async Task<KomitentEntity> AddAsync(KomitentEntity komitent)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Komitenti.AddAsync(komitent);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return komitent;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
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
                    var komitent = await _context.Komitenti.FindAsync(id);

                    if (komitent == null) return false;

                    await _context.Komitenti.Where(k => k.Id == id).ExecuteDeleteAsync();

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

        public Task<bool> DeleteAsync(string sifra)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<KomitentEntity>> GetAllAsync()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var komitenti = await _context.Komitenti.AsNoTracking().ToListAsync();

                    await transaction.CommitAsync();

                    return komitenti;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<IEnumerable<KomitentEntity>> GetBySifraAsync(string name)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var komitent = await _context.Komitenti.AsNoTracking().Where(k => k.Naziv!.ToLower().Contains(name.ToLower())).ToListAsync();

                    await transaction.CommitAsync();

                    return komitent;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<KomitentEntity?> UpdateAsync(int id, KomitentEntity komitentEntity)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var komitent = await _context.Komitenti.FindAsync(id);

                    if (komitent == null) return null;

                    await _context.Komitenti.Where(k => k.Id == id)
                                            .ExecuteUpdateAsync(k => k
                                                                    .SetProperty(k => k.Naziv, komitentEntity.Naziv)
                                                                    .SetProperty(k => k.Adresa, komitentEntity.Adresa));

                    await transaction.CommitAsync();

                    return komitentEntity;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public Task<KomitentEntity?> UpdateAsync(string sifra, KomitentEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
