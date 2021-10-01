using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKANA.Extensions
{
    public static class ViewComponentExtension
    {
        public static string GetViewPath(this ViewComponent viewComponent, string area, string viewName = "Default")
        {
            return $"/Areas/{area}/Views/Shared/Components/{viewComponent.ViewComponentContext.ViewComponentDescriptor.ShortName}/{viewName}.cshtml";
        }
    }
}
