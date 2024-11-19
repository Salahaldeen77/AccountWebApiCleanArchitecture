using AccountWeb.Core.Features.Accounts.Commands.Models;
using AccountWeb.Core.Features.Accounts.Queries.Responses;
using AccountWeb.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace AccountWeb.Core.Mapping.AccountMap.CommandMapping
namespace AccountWeb.Core.Mapping.AccountMap
{
    public partial class AccountProfile 
    {
        public void AddAccountCommandMapping()  
        {
            CreateMap<AddAccountCommand,Account>();
        }
    }
}
