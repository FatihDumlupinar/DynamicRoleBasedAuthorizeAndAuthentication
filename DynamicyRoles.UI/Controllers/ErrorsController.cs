using DynamicyRoles.UI.Attributes;
using DynamicyRoles.UI.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DynamicyRoles.UI.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : Controller
    {
        [Route("/error-development")]
        public IActionResult ErrorDevelopment()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            ErrorViewModel errorViewModel = new()
            {
                ExceptionMessage = exceptionDetails.Error.Message,
                ExceptionPath = exceptionDetails.Path,
                ExceptionStackTrace = exceptionDetails.Error.StackTrace ?? ""
            };
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return View(errorViewModel);
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //logging

            return View();
        }

        [Route("/unauthorized")]
        public IActionResult UnAuthorized()
        {
            return View();
        }
    }
}
