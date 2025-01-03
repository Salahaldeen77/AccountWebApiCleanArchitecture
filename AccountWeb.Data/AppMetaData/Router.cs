﻿namespace AccountWeb.Data.AppMetaData
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

        }
        public static class AuthorizationRouting
        {
            public const string Prefix = Rule + "Authorization";
            public const string Create = Prefix + "/Role/Create";
            public const string Edit = Prefix + "/Role/Edit";
            public const string Delete = Prefix + "/Role/Delete";

        }

        public static class TransactionRouting
        {

        }
    }
}
