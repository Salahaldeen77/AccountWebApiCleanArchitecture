using AccountWeb.Data.Entities.Identity;
using AccountWeb.Infrustructure.Context;
using AccountWeb.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AccountWeb.Service.Implementations
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailsService _emailsService;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUrlHelper _urlHelper;
        #endregion
        #region Constructors
        public UserService(UserManager<User> userManager,
                            IHttpContextAccessor httpContextAccessor,
                            IEmailsService emailsService,
                            ApplicationDbContext applicationDbContext,
                            IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
            _emailsService = emailsService;
            _urlHelper = urlHelper;
        }
        #endregion
        #region Handle Functions
        public async Task<string> AddUserAsync(User user, string Password)
        {
            using (var transact = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    //if email exist
                    var existUser = await _userManager.FindByEmailAsync(user.Email);
                    if (existUser != null) return "Failed, Email is Exist";
                    //if UserName is Exist
                    var userByUserName = await _userManager.FindByNameAsync(user.UserName);
                    if (userByUserName != null) return "Failed, UserName is Exist";

                    //Create 
                    var CreateResult = await _userManager.CreateAsync(user, Password);
                    //Failed
                    if (!CreateResult.Succeeded) return "Error in create user," + CreateResult.Errors.FirstOrDefault().Description;

                    //Adding Role User
                    await _userManager.AddToRoleAsync(user, "User");

                    //send Confirm Email
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var requestAccessor = _httpContextAccessor.HttpContext.Request;
                    var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
                    //$"/Api/AccountSys/Authentication/ConfirmEmail?userId={user.Id}&code={code}";

                    var message = $"To Confirm Email Click Link: <a href='{returnUrl}'></a>";
                    // message or body
                    var sendEmailResult = await _emailsService.SendEmail(user.Email, message, "Confirm Email");
                    if (sendEmailResult != "Success")
                        return "Operation to send email failed " + sendEmailResult;

                    await transact.CommitAsync();
                    //Success
                    return "Success";

                }
                catch (Exception ex)
                {
                    await transact.RollbackAsync();
                    return "Failed, " + ex.Message;
                }

            }

        }
        #endregion

    }
}
