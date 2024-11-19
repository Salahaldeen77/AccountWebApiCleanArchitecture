using AccountWeb.Data.Entities;
using AccountWeb.Infrustructure.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AccountWeb.Service.Abstracts
{
    public interface ILedgerEntriesService :IMainRepository<LedgerEntry>
    {
        
    }
}
