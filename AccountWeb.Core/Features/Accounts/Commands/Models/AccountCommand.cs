using AccountWeb.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccountWeb.Core.Features.Accounts.Commands.Models
{
    public class AddAccountCommand : IRequest<Response<string>>
    {
        //[Required(ErrorMessage = "The AccountNumber is Required*_*")]
        public int AccountNumber { get; set; }
        //[Required(ErrorMessage = "The OpeningBalance is Required*_*")]
        public decimal OpeningBalance { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 0 Inactive,
        /// 1 Active
        /// </summary>
        public bool IsActive { get; set; }
        public IFormFile? Image { get; set; }

    }
    public class EditAccountCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        //[Required(ErrorMessage = "The OpeningBalance is Required*_*")]
        public decimal Balance { get; set; }

        /// <summary>
        /// 0 Inactive,
        /// 1 Active
        /// </summary>
        public bool IsActive { get; set; }
        public IFormFile? Image { get; set; }

    }
    public class DeleteAccountCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteAccountCommand(int id)
        {
            Id = id;
        }
    }
}
