using System;
using Microsoft.AspNetCore.Components.Web;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;
using System.Collections.Generic;
using ITDocumentation.Data;
using System.Data;


namespace ITDocumentation
{
    interface UserFilter
    {
        void NameFilter(string name);
        void UserNameFilter(string username);
        void RoleFilter(string roleName);
        void SubdepartmentFilter(string subdepartmentName);
        void ResetFilter();
    }
}