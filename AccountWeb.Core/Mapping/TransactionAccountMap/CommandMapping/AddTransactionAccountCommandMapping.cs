using AccountWeb.Core.Features.Accounts.Commands.Models;
using AccountWeb.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace AccountWeb.Core.Mapping.TransactionAccountMap.CommandMapping
namespace AccountWeb.Core.Mapping.TransactionAccountMap
{
    public partial class TransactionAccountProfile
    {
        public void AddTransactionAccountCommandMapping()
        {
            CreateMap<AddTransactionAccountCommand, TransactionAccount>();
        }
    }
}
