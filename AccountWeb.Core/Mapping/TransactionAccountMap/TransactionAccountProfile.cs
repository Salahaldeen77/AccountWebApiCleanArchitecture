using AccountWeb.Core.Features.Accounts.Queries.Responses;
using AccountWeb.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountWeb.Core.Mapping.TransactionAccountMap
{
    public partial class TransactionAccountProfile:Profile
    {
        public TransactionAccountProfile()
        {
            GetTransactionAccountListMapping();
            AddTransactionAccountCommandMapping();
        }
    }
}
