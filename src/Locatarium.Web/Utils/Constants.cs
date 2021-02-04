using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Locatarium.Web.Utils
{
    public class Constants
    {
        public static string ClaimTypeSerialNumber = "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber";

        public static string wwwroot = "\\wwwroot\\";

        public static OperationAuthorizationRequirement Create = new OperationAuthorizationRequirement { Name = "Create" };
        public static OperationAuthorizationRequirement Read = new OperationAuthorizationRequirement { Name = "Read" };
        public static OperationAuthorizationRequirement Update = new OperationAuthorizationRequirement { Name = "Update" };
        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement { Name = "Delete" };
    }
}
