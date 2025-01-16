namespace AccountWeb.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version = "AccountSys";
        public const string Rule = root + "/" + version + "/";
        public static class AccountRouting
        {
            public const string Prefix = Rule + "Account";
            public const string List = Prefix + "/List";
            public const string GetById = Prefix + "/{Id}";
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/{Id}";
            public const string Paginated = Prefix + "/Paginated";

        }

        public static class TransactionAccountRouting
        {
            public const string Prefix = Rule + "TransactionAccount";
            public const string List = Prefix + "/List";
            public const string GetById = Prefix + "/{Id}";
            public const string Create = Prefix + "/Create";
        }

        public static class UserRouting
        {
            public const string Prefix = Rule + "User";
            public const string Create = Prefix + "/Create";
            public const string Paginated = Prefix + "/Paginated";
            public const string GetById = Prefix + "/{Id}";
            public const string Edite = Prefix + "/Edite";
            public const string Delete = Prefix + "/{Id}";
            public const string ChangePassword = Prefix + "/Change-Password";
        }
        public static class Authentication
        {
            public const string Prefix = Rule + "Authentication";
            public const string SignIn = Prefix + "/SignIn";
            public const string RefreshToken = Prefix + "/Refresh-Token";
            public const string ValidateToken = Prefix + "/Validate-Token";
            public const string ConfirmEmail = Prefix + "/ConfirmEmail";
            public const string SendResetPassword = Prefix + "/SendResetPassword";
            public const string ConfirmResetPassword = Prefix + "/ConfirmResetPassword";
            public const string ResetPassword = Prefix + "/ResetPassword";

        }
        public static class AuthorizationRouting
        {
            public const string Prefix = Rule + "Authorization";
            public const string Roles = Prefix + "/Roles";
            public const string Claims = Prefix + "/Claims";
            public const string Create = Roles + "/Create";
            public const string Edit = Roles + "/Edit";
            public const string Delete = Roles + "/Delete";
            public const string RoleList = Roles + "/Role-List";
            public const string GetById = Roles + "/Role-By-{Id}";
            public const string ManageUserRoles = Roles + "/Manage-User-Roles/{userId}";
            public const string UpdateUserRoles = Roles + "/Update-User-Roles";
            public const string ManageUserClaims = Claims + "/Manage-User-Claims/{userId}";
            public const string UpdateUserClaims = Claims + "/Update-User-Claims";

        }
        public static class EmailsRouting
        {
            public const string Prefix = Rule + "Emails";
            public const string SendEmail = Prefix + "/SendEmail";
        }

        public static class TransactionRouting
        {

        }
    }
}
