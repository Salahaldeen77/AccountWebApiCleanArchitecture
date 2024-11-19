using AccountWeb.Data.Entities;
using AccountWeb.Infrustructure.Abstracts;
using AccountWeb.Infrustructure.Context;
using AccountWeb.Infrustructure.Repositories;
using AccountWeb.Service.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LedgerEntryWeb.Service.Implementations
{
    public class LedgerEntriesService:MainRepository<LedgerEntry>, ILedgerEntriesService
    {
        private readonly DbSet<LedgerEntry> _LedgerEntries;
        public LedgerEntriesService(ApplicationDbContext dbContext):base(dbContext)
        {
            _LedgerEntries=dbContext.Set<LedgerEntry>();
        }
        public override async Task<IEnumerable<LedgerEntry>> FindAllAsync()
        {
            return await _LedgerEntries.Include(x=>x.TransactionAccount).ToListAsync();
        }

    }
}
