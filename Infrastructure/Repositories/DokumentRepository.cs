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
                    // Check if Komitent with the same SifraKomitenta already exists
                    if (dokumentEntity.Komitent != null)
                    {
                        var existingKomitent = await _context.Komitenti
                            .AsNoTracking()
                            .FirstOrDefaultAsync(k => k.SifraKomitenta == dokumentEntity.Komitent.SifraKomitenta);

                        if (existingKomitent != null)
                        {
                            dokumentEntity.Komitent = existingKomitent;
                        }
                        else
                        {
                            // Add the new KomitentEntity if it does not exist
                            _context.Komitenti.Attach(dokumentEntity.Komitent);
                        }
                    }

                    await _context.Dokumenti.AddAsync(dokumentEntity);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return dokumentEntity;
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

        public Task<bool> DeleteAsync(string sifra)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DokumentEntity>> GetAllAsync()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var dokumenti = await _context.Dokumenti.AsNoTracking()
                                            .Include(d => d.Stavke!).ThenInclude(s => s.Roba)
                                            .Include(d => d.Komitent)
                                            .AsSplitQuery()
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

        public async Task<IEnumerable<DokumentEntity>> GetBySifraAsync(string name)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var dokumenti = await _context.Dokumenti.AsNoTracking()
                                      .Include(d => d.Stavke!).ThenInclude(s => s.Roba)
                                      .Include(d => d.Komitent)
                                      .Where(d => d.Komitent!.Naziv!.ToLower().Contains(name.ToLower()))
                                      .AsSplitQuery()
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

        public async Task<DokumentEntity?> UpdateAsync(int id, DokumentEntity dokumentEntity)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var dokument = await _context.Dokumenti.AsNoTracking()
                                                    .Include(d => d.Stavke!).ThenInclude(s => s.Roba)
                                                    .Include(d => d.Komitent)
                                                    .AsSplitQuery()
                                                    .FirstOrDefaultAsync(d => d.Id == id);

                    if (dokument == null) return null;

                    foreach (var stavka in dokument.Stavke!)
                    {
                        await _context.Stavke.Where(s => s.Id == stavka.Id)
                                             .ExecuteUpdateAsync(s => s
                                                    .SetProperty(s => s.Kolicina, stavka.Kolicina)
                                                    .SetProperty(s => s.UkupnaCenaStavke, stavka.UkupnaCenaStavke));
                    }



                    await _context.Dokumenti.Where(d => d.Id == id)
                                            .ExecuteUpdateAsync(d => d
                                                    .SetProperty(d => d.UkupnaCena, dokumentEntity.UkupnaCena)
                                                    .SetProperty(d => d.DatumIzdavanja, DateOnly.FromDateTime(DateTime.Now)));

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

        public Task<DokumentEntity?> UpdateAsync(DokumentEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
