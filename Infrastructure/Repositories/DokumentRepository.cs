using Application.Contracts.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class DokumentRepository : IDokumentRepository
    {
        private readonly ProjekatContext _context;

        public DokumentRepository(ProjekatContext context)
        {
            _context = context;
        }

        public async Task<DokumentEntity> AddAsync(DokumentEntity dokumentEntity)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Dokumenti.AddAsync(dokumentEntity);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    var dokument = await _context.Dokumenti.AsNoTracking().Include(d => d.Stavke!).ThenInclude(s => s.Roba)
                                                    .Include(d => d.Komitent)
                                                    .AsSplitQuery()
                                                    .FirstOrDefaultAsync(d => d.Id == dokumentEntity.Id);

                    return dokument!;
                }
                catch
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
                    var dokument = await _context.Dokumenti.FindAsync(id);

                    if (dokument == null) return false;

                    await _context.Dokumenti.Where(d => d.Id == id).ExecuteDeleteAsync();

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

        public async Task<IReadOnlyList<DokumentEntity>> GetAllAsync()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var dokumenti = await _context.Dokumenti.AsNoTracking()
                                            .Include(d => d.Stavke!).ThenInclude(s => s.Roba)
                                            .Include(d => d.Komitent)
                                            .AsSplitQuery()
                                            .AsNoTracking()
                                            .ToListAsync();

                    await transaction.CommitAsync();

                    return dokumenti;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<DokumentEntity?> GetAsync(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var dokument = await _context.Dokumenti.AsNoTracking().Include(d => d.Stavke!).ThenInclude(s => s.Roba)
                                                    .Include(d => d.Komitent)
                                                    .AsSplitQuery()
                                                    .FirstOrDefaultAsync(d => d.Id == id);

                    await transaction.CommitAsync();

                    return dokument == null ? null : dokument;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public Task<IEnumerable<DokumentEntity>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<DokumentEntity?> UpdateAsync(int id, DokumentEntity dokumentEntity)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var dokument = await _context.Dokumenti.AsNoTracking().Include(d => d.Stavke!).ThenInclude(s => s.Roba)
                                                    .Include(d => d.Komitent)
                                                    .FirstOrDefaultAsync(d => d.Id == id);

                    if (dokument == null) return null;

                    await _context.Dokumenti.Where(d => d.Id == id)
                        .ExecuteUpdateAsync(d => d.
                                SetProperty(d => d.UkupnaCena, dokumentEntity.UkupnaCena));

                    await transaction.CommitAsync();

                    return dokument;
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
