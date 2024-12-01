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
        public static class UserRouting
        {
            public const string Prefix = Rule + "User";
            public const string Create = Prefix + "/Create";
            public const string Paginated = Prefix + "/Paginated";
            public const string GetById = Prefix + "/{Id}";

        }
        public static class TransactionAccountRouting
        {
            public const string Prefix = Rule + "TransactionAccount";
            public const string List = Prefix + "/List";
            public const string GetById = Prefix + "/{Id}";
            public const string Create = Prefix + "/Create";
        }
        public static class TransactionRouting
        {

        }
    }
}
