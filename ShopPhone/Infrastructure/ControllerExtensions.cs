namespace ShopPhone.Infrastructure
{
    using Microsoft.AspNetCore.Mvc;
    using System;

    public static class ControllerExtensions
    {
        public static string GetControllerName(this Type controllerType)
        {
            return controllerType.Name.Replace(nameof(Controller), string.Empty);
        }
    }
}
