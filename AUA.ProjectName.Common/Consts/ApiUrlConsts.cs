namespace AUA.ProjectName.Common.Consts
{
    public static class ApiUrlConsts
    {
        public static readonly string BaseUrl = "http://localhost:5032";

        public static readonly string Login = BaseUrl + @"/api/V1.0/Accounting/Login";

        public static readonly string GetCurrentUser = BaseUrl + @"/api/V1.0/AppUser/GetCurrentUser";

        public static readonly string ChangePassword = BaseUrl + @"/api/V1.0/AppUser/ChangePassword";

        public static readonly string UpdateAppUser = BaseUrl + @"/api/V1.0/AppUser/Update";

        public static readonly string InsertAppUser = BaseUrl + @"/api/V1.0/AppUser/Insert";

        public static readonly string DeleteAppUser = BaseUrl + @"/api/V1.0/AppUser/Delete";

        public static readonly string GetAppUsers = BaseUrl + @"/api/V1.0/AppUser/List";

        public static readonly string GetRoles = BaseUrl + @"/api/V1.0/Role/List";

        public static readonly string InsertRole = BaseUrl + @"/api/V1.0/Role/Insert";

        public static readonly string UpdateRole = BaseUrl + @"/api/V1.0/Role/Update";

        public static readonly string DeleteRole = BaseUrl + @"/api/V1.0/Role/Delete";

        public static readonly string GetUserRoles = BaseUrl + @"/api/V1.0/UserRole/GetUserRoles";

        public static readonly string InsertManyUserRoles = BaseUrl + @"/api/V1.0/UserRole/InsertMany";

        public static readonly string InsertUserRole = BaseUrl + @"/api/V1.0/UserRole/Insert";

        public static readonly string DeleteUserRole = BaseUrl + @"/api/V1.0/UserRole/Delete";

        public static readonly string GetAccesses = BaseUrl + @"/api/V1.0/UserAccess/List";

        public static readonly string GetUserAccessRoleByRoleId = BaseUrl + @"/api/V1.0/UserRoleAccess/GetUserAccessRoleByRoleId";
        
        public static readonly string DeleteUserRoleAccess = BaseUrl + @"/api/V1.0/UserRoleAccess/DeleteUserRoleAccess";
        
        public static readonly string InsertUserRoleAccess = BaseUrl + @"/api/V1.0/UserRoleAccess/InsertUserRoleAccess";

        public static string DeleteRoleUrl(long roleId)
        {
            return DeleteRole + "?id=" + roleId;
        }


        public static string DeleteAppUserUrl(long userId)
        {
            return DeleteAppUser + "?userId=" + userId;
        }

        public static string GetUserRoleUrl(long userId)
        {
            return GetUserRoles + "?userId=" + userId;
        }

        public static string GetUserAccessRoleByRoleIdUrl(int roleId)
        {
            return GetUserAccessRoleByRoleId + "?roleId=" + roleId;
        }
    }
}
