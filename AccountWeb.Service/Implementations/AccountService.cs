﻿using AccountWeb.Data.Entities;
using AccountWeb.Data.Enums;
using AccountWeb.Infrustructure.Abstracts;
using AccountWeb.Infrustructure.Context;
using AccountWeb.Infrustructure.Repositories;
using AccountWeb.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace AccountWeb.Service.Implementations
{
    public class AccountService : MainRepository<Account>, IAccountService
    {
        //private readonly DbSet<Account> _accounts;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext)
        {
            // _accounts=dbContext.Set<Account>();
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsWithIncludeAsync()
        {
            //return await _accounts.Include(x => x.TransactionAccounts).ToListAsync();
            return await _unitOfWork.Accountes.GetTableNoTracking().
                                                Include(x => x.TransactionAccounts).ToListAsync();
        }

        public async Task<Account> GetAccountByIdWithIncludeAsync(int id)
        {
            var account = await _unitOfWork.Accountes.GetTableNoTracking().Where(x => x.Id == id)
                                        .Include(x => x.TransactionAccounts).ThenInclude(x => x.Transaction)
                                        .FirstOrDefaultAsync();
            return account;
        }

        public async Task<string> AddAccountAsync(Account account)
        {
            //check if the AccountNumber is Exist or not
            var accountResult = _unitOfWork.Accountes.GetTableNoTracking().Where(x => x.AccountNumber.Equals(account.AccountNumber)).FirstOrDefault();
            if (accountResult != null) return "Exist";

            //Added Account
            //if (account.Id != null) account.Id = null;
            await _unitOfWork.Accountes.AddAsync(account);
            return "Success";

        }

        public async Task<bool> IsAccountNumberExist(int accountNumber)
        {
            var accountResult = await _unitOfWork.Accountes.GetTableNoTracking().Where(x => x.AccountNumber.Equals(accountNumber)).FirstOrDefaultAsync();
            if (accountResult != null) return true;
            else return false;

        }

        public async Task<bool> IsAccountNumberExistExcludeSelf(int accountNumber, int id)
        {
            var accountResult = await _unitOfWork.Accountes.GetTableNoTracking().Where(x => x.AccountNumber == accountNumber & x.Id != id).FirstOrDefaultAsync();
            if (accountResult != null) return true;
            else return false;
        }

        public async Task<string> EditAccountAsync(Account account)
        {
            using (var trans = _unitOfWork.Accountes.BeginTransaction())
            {
                try
                {
                    // تحديث الحالة لكي لا تتم متابعة الكائن
                    await _unitOfWork.Accountes.UpdateAsync(account);

                    await trans.CommitAsync();

                    return "Success";
                }
                catch
                {
                    await trans.RollbackAsync();
                    return "Failed";
                }
            }
        }

        public async Task<string> DeleteAccountAsync(Account account)
        {
            using (var trans = _unitOfWork.Accountes.BeginTransaction())
            {
                try
                {
                    await _unitOfWork.Accountes.DeleteAsync(account);
                    await trans.CommitAsync();
                    return "Success";
                }
                catch
                {
                    await trans.RollbackAsync();
                    return "Failed";
                }
            }


        }

        public async Task<bool> IsAccountHaveTransactionByIdAsync(int id)
        {
            var result = await _unitOfWork.TransactionAccounts.GetTableNoTracking().Where(x => x.AccountId.Equals(id)).FirstOrDefaultAsync();
            if (result != null) return true;
            else return false;
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await _unitOfWork.Accountes.FindByIdAsync(id);
        }

        public IQueryable<Account> GetAllAccountsQuerable()
        {
            return _unitOfWork.Accountes.GetTableNoTracking().Include(x => x.TransactionAccounts).AsQueryable();
        }

        public IQueryable<Account> FilterAccountPaginatedQuerable(string search, AccountOrderingEnum orderingEnum)
        {
            var querable = _unitOfWork.Accountes.GetTableNoTracking().Include(x => x.TransactionAccounts).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.AccountNumber.ToString().Contains(search)
                                         || x.Id.ToString().Contains(search));
            }
            switch (orderingEnum)
            {
                case AccountOrderingEnum.Id:
                    querable = querable.OrderBy(x => x.Id);
                    break;
                case AccountOrderingEnum.AccountNumber:
                    querable = querable.OrderBy(x => x.AccountNumber);
                    break;
                default:
                    querable = querable.OrderBy(x => x.Id);
                    break;
            }
            return querable;
        }

        public async Task<Account> GetAccountByIdAsNoTrackingAsync(int id)
        {
            return await _unitOfWork.Accountes.GetTableNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //private readonly IUnitOfWork _unitOfWork;
        //private readonly ApplicationDbContext _applicationDbContext;

        //public AccountService(IUnitOfWork unitOfWork,ApplicationDbContext applicationDbContext)
        //{
        //    this._unitOfWork = unitOfWork;
        //    this._applicationDbContext = applicationDbContext;
        //}

    }
}
