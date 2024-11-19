using AccountWeb.Data.Entities;
using AccountWeb.Infrustructure.Abstracts;
using AccountWeb.Infrustructure.Context;
using AccountWeb.Infrustructure.Repositories;
using AccountWeb.Service.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AccountWeb.Service.Implementations
{
    public class TransactionService : MainRepository<Transaction>, ITransactionService
    {
        private readonly DbSet<Transaction> _transactions;
        public TransactionService(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
         _transactions=applicationDbContext.Set<Transaction>(); 
        }
        public override async Task<IEnumerable<Transaction>> FindAllAsync()
        {
            return await _transactions.Include(x => x.TransactionAccounts).ToListAsync();
        }
    }
}
