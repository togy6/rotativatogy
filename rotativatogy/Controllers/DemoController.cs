using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using rotativatogy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rotativatogy.Controllers
{
    public class DemoController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        public DemoController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }


        public async Task<IActionResult> Certificate(string id)
        {

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"{id} User Id li kullanıcı yoktur.";
                return View();
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                AdSoyad = user.AdSoyad,
                Kurum = user.Kurum,
                Bolum = user.Bolum,
                Unvan = user.Unvan,
                Telefon = user.Telefon
            };

            ViewData["username"] = model.AdSoyad;
            var report = new ViewAsPdf("Certificate")
            {
                //FileName = "Certificate.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A1,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                PageMargins = { Left = 20, Bottom = 0, Right = 0, Top = 14 },
                CustomSwitches = "--enable-smart-shrinking"
                //PageHeight=297,
                //PageWidth=210
                //CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12"
            };


            //return View(model);

            //return new ViewAsPdf("Certificate", model.AdSoyad);
            return report;
        }


        public IActionResult DemoViewAsHTML()
        {
            return View();
        }

        [Authorize]

        public IActionResult Index()
        {
            return View();
        }
    }
}
