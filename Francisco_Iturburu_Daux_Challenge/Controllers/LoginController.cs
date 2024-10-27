using Francisco_Iturburu_Daux_Challenge.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using System.Text;

namespace Francisco_Iturburu_Daux_Challenge.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthRequest _authRequest;

        public LoginController(IAuthRequest authRequest)
        {
            _authRequest = authRequest;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                Request request = new() { apellido = model.LastName, nombre = model.FirstName };
                string OK_STATUS = "OK";
                try
                {
                    Response res = await _authRequest.Auth(request);
                    if (res.result == OK_STATUS)
                    {
                        return Json(new { success = true, message = res.result });
                    }
                    else
                    {
                        throw new Exception(res.result);
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { success = false, errorMessage = ex.Message });

                }

            }

            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }
    }
}