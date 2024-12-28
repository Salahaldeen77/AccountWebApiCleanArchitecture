using AccountWeb.Core.Features.Authorization.Commands.Models;
using AccountWeb.Core.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Authorization.Commands.Validators
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        //private readonly RoleManager<Role> _roleManager;
        #endregion

        #region Constructors
        public DeleteRoleValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            //_roleManager = roleManager;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();

        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
        }
        //public async Task<bool> isExist(int id)
        //{
        //    var role = await _roleManager.FindByIdAsync(id.ToString());
        //    return role != null;
        //}
        public void ApplyCustomValidationsRules()
        {
            //RuleFor(x => x.Id)
            //    .MustAsync(async (Key, CancellationToken) => await isExist(Key))
            //    .WithMessage(_stringLocalizer[SharedResourcesKeys.NotFound]);
        }
        #endregion
    }
}
