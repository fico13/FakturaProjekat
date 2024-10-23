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

        public async Task<bool> DeleteAsync(string sifraKomitenta)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var komitent = await _context.Komitenti.FirstOrDefaultAsync(k => k.SifraKomitenta == sifraKomitenta);

                    if (komitent == null) return false;

                    await _context.Komitenti.Where(k => k.SifraKomitenta == sifraKomitenta).ExecuteDeleteAsync();

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

        public async Task<IEnumerable<KomitentEntity>> GetBySifraAsync(string sifraKomitenta)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var komitent = await _context.Komitenti.AsNoTracking()
                        .Where(k => k.SifraKomitenta!.ToLower().Contains(sifraKomitenta.ToLower())).ToListAsync();

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

        public async Task<KomitentEntity?> UpdateAsync(KomitentEntity komitentEntity)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var komitent = await _context.Komitenti.FindAsync(komitentEntity.Id);

                    if (komitent == null) return null;

                    await _context.Komitenti.Where(k => k.SifraKomitenta == komitentEntity.SifraKomitenta)
                                            .ExecuteUpdateAsync(k => k
                                                                    .SetProperty(k => k.Naziv, komitentEntity.Naziv)
                                                                    .SetProperty(k => k.Adresa, komitentEntity.Adresa)
                                                                    .SetProperty(k => k.Grad, komitentEntity.Grad));

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
    }
}
