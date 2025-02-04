using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Accounts.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Data.Entities;
using AccountWeb.Service.Abstracts;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Accounts.Commands.Handlers
{
    public class AccountCommandHandler : ResponseHandler,
                                        IRequestHandler<AddAccountCommand, Response<string>>,
                                        IRequestHandler<EditAccountCommand, Response<string>>,
                                        IRequestHandler<DeleteAccountCommand, Response<string>>

    {
        #region Fields
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public AccountCommandHandler(IAccountService accountService, IMapper mapper, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            this._accountService = accountService;
            this._mapper = mapper;
        }
        #endregion

        #region Handel Functions
        public async Task<Response<string>> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            //Mapping Between request and Account
            var AccountMapper = _mapper.Map<Account>(request);
            //add Account To Db
            var result = await _accountService.AddAccountAsync(AccountMapper, request.Image);
            //Check Condition
            //if (result == "Exist") return UnprocessableEntity<String>("AccountNumber is Exist");
            ////Return Response
            //else if (result == "Success") return Created("Added Account Successfully");
            //else return BadRequest<string>();

            if (result == "Success") return Created("");
            else return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(EditAccountCommand request, CancellationToken cancellationToken)
        {
            //check if the Id is Exist or not
            var account = await _accountService.GetAccountByIdAsNoTrackingAsync(request.Id);
            //return not found
            if (account == null) return NotFound<string>($"The AccountId={request.Id} is not found");
            //mapping between request and Account
            var accountMapper = _mapper.Map(request, account);
            //call service that make Edit
            var result = await _accountService.EditAccountAsync(accountMapper);
            //return response
            if (result == "Success") return Success($"Edit  Successfully AccountId={accountMapper.Id}");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            //check if the Id is Exist or not
            var account = await _accountService.GetAccountByIdAsync(request.Id);
            //return not found
            if (account == null) return NotFound<string>($"The AccountId={request.Id} is not found");
            //call service that make delete
            var result = await _accountService.DeleteAccountAsync(account);
            if (result == "Success") return Deleted<string>($"Deleted  Successfully AccountId={account.Id}");
            else return BadRequest<string>();
        }
        #endregion

    }
}
