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
                    // Attach the existing Komitent instead of fetching it again (assuming it already exists in the database)
                    var komitentEntity = await _context.Komitenti.FirstOrDefaultAsync(k => k.SifraKomitenta == dokumentEntity.SifraKomitenta);

                    if (komitentEntity != null)
                    {
                        _context.Attach(komitentEntity);
                        dokumentEntity.Komitent = komitentEntity; // Attach instead of fetching again
                    }

                    // For each StavkaDokumenta, attach the existing Roba entity
                    foreach (var stavka in dokumentEntity.Stavke!)
                    {
                        var robaEntity = await _context.Roba.FirstOrDefaultAsync(r => r.SifraRobe == stavka.SifraRobe);
                        if (robaEntity != null)
                        {
                            _context.Attach(robaEntity);
                            stavka.Roba = robaEntity; // Attach the Roba entity to StavkaDokumenta
                        }
                    }

                    // Now add the Dokument with all its relations
                    await _context.Dokumenti.AddAsync(dokumentEntity);
                    await _context.SaveChangesAsync();
                    // Commit the transaction if everything is successful
                    await transaction.CommitAsync();

                    return dokumentEntity;
                }
                catch (Exception)
                {
                    // Rollback the transaction if an error occurs
                    await transaction.RollbackAsync();
                    // Log or handle the exception as necessary
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAsync(string brojDokumenta)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var dokument = await _context.Dokumenti.FirstOrDefaultAsync(d => d.BrojDokumenta == brojDokumenta);

                    if (dokument == null) return false;

                    await _context.Dokumenti.Where(d => d.BrojDokumenta == brojDokumenta).ExecuteDeleteAsync();

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

        public async Task<IEnumerable<DokumentEntity>> GetBySifraAsync(string brojDokumenta)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var dokumenti = await _context.Dokumenti.AsNoTracking()
                                      .Include(d => d.Stavke!).ThenInclude(s => s.Roba)
                                      .Include(d => d.Komitent)
                                      .Where(d => d.BrojDokumenta!.ToLower().Contains(brojDokumenta.ToLower()))
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

        public async Task<DokumentEntity?> UpdateAsync(DokumentEntity dokumentEntity)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var dokument = await _context.Dokumenti.FindAsync(dokumentEntity.Id);

                    if (dokument == null) return null;

                    foreach (var stavka in dokumentEntity.Stavke!)
                    {
                        await _context.Stavke.Where(s => s.Id == stavka.Id)
                                             .ExecuteUpdateAsync(s => s
                                                    .SetProperty(s => s.Kolicina, stavka.Kolicina)
                                                    .SetProperty(s => s.UkupnaCenaStavke, stavka.UkupnaCenaStavke));
                    }



                    await _context.Dokumenti.Where(d => d.Id == dokumentEntity.Id)
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
    }
}
