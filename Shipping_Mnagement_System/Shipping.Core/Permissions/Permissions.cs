using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Permissions
{
    public static class Permissions
    {
        // Module: User Groups
        public static class UserGroups
        {
            public const string View = "UserGroups.View";
            public const string Create = "UserGroups.Create";
            public const string Edit = "UserGroups.Edit";
            public const string Delete = "UserGroups.Delete";
            public const string ManagePermissions = "UserGroups.ManagePermissions";
        }

        // Module: Users
        public static class Users
        {
            public const string View = "Users.View";
            public const string Create = "Users.Create";
            public const string Edit = "Users.Edit";
            public const string Delete = "Users.Delete";
            public const string ManageStatus = "Users.ManageStatus";
        }

        // Module: Orders
        public static class Orders
        {
            public const string View = "Orders.View";
            public const string Create = "Orders.Create";
            public const string Edit = "Orders.Edit";
            public const string UpdateStatus = "Orders.UpdateStatus";
            public const string Assign = "Orders.Assign";
        }

        // Module: Locations
        public static class Locations
        {
            public const string ViewGovernorates = "Locations.ViewGovernorates";
            public const string ManageGovernorates = "Locations.ManageGovernorates";
            public const string ViewCities = "Locations.ViewCities";
            public const string ManageCities = "Locations.ManageCities";
            public const string ViewAreas = "Locations.ViewAreas";
            public const string ManageAreas = "Locations.ManageAreas";
        }

        // Module: Dashboard
        public static class Dashboard
        {
            public const string ViewMerchant = "Dashboard.ViewMerchant";
            public const string ViewEmployee = "Dashboard.ViewEmployee";
            public const string ViewAdmin = "Dashboard.ViewAdmin";
        }
    }
}
