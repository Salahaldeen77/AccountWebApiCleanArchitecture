﻿using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.TransactionAccounts.Queries.Responses;
using MediatR;

namespace AccountWeb.Core.Features.TransactionAccounts.Queries.Models
{
    public class GetTransactionAccountListResponseQuery : IRequest<Response<List<GetTransactionAccountListResponse>>>
    {

    }


}
