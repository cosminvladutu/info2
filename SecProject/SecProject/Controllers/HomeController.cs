using System.Collections.Generic;
using System.Web.Mvc;
using Common;
using SecProject.BL;
using SecProject.Models;

namespace SecProject.Controllers
{
    public class HomeController : Controller
    {
        //   public StardogConnector context;
        public static List<ProductType> ProductTypes;

        //   SecProject.BL.SecService service = new BL.SecService(context);
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            new Base().Initialise();
            ProductTypes = new Base().ReturnProductTypes();



            return View();
        }

        public ActionResult Products()
        {
            new Base().Initialise();

            var productVM = new ProductViewModel();
            //productVM.PopulateProductType(new Base().ReturnProductTypes());
            productVM.PopulateProductType(ProductTypes);

            return View("Products", productVM);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Information";

            return View();
        }


        public ActionResult DropDownFilterOnProducts(string selectedBrand, string selectedColour, string selectedGender, string selectedStyle, string selectedSeason)
        {
            // var model = new List<ProductsToShow>();
            var model = new ProductOntologyFilterModelViewModel();
            model.PopulateModel(ProductTypes, selectedBrand, selectedColour, selectedGender, selectedSeason, selectedStyle);
            model.PopulateProductListFull(ProductTypes, null, selectedBrand, selectedColour, selectedGender, selectedSeason, selectedStyle);
            return PartialView("PartialViews/ProductsPartialView", model.ProductList);
        }

        public ActionResult ChangeSubCategory(string subCateg)
        {
            var pofmVm = new ProductOntologyFilterModelViewModel();
            pofmVm.PopulateModel(ProductTypes, "", "", "", "", "");
            pofmVm.PopulateProductListFull(ProductTypes, subCateg, "", "", "", "", "");

            return PartialView("PartialViews/ProductFilter", pofmVm);
        }

        [HttpPost]
        public ActionResult AddToWardrobe(string productName, string selectedBrand, string selectedColour, string selectedGender, string selectedStyle, string selectedSeason)
        {
            var model = new ProductOntologyFilterModelViewModel();
            var dal = new DAL.DALContext();
            var up = dal.GetUserProfile(User.Identity.Name);
            dal.AddToWardrobe(productName, up.UserId);
            model.PopulateModel(ProductTypes, selectedBrand, selectedColour, selectedGender, selectedSeason, selectedStyle);
            model.PopulateProductListFull(ProductTypes, null, selectedBrand, selectedColour, selectedGender, selectedSeason, selectedStyle);
            return PartialView("PartialViews/ProductsPartialView", model.ProductList);
        }
    }
}
