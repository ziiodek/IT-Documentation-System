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
    interface Filter
    {
        void NameFilter(string name);
        void DateFilter(DateTime? date);
        void AuthorFilter(string authorName);
        void ResetFilter();
    }
}