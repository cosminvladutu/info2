using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using Common;

namespace SecProject.Models
{
    public class ProductOntologyFilterModelViewModel
    {
        [DisplayName("Brand")]
        public EnumerableDropDownViewModel Brand { get; set; }

        [DisplayName("Colour")]
        public EnumerableDropDownViewModel Colour { get; set; }

        [DisplayName("Gender")]
        public EnumerableDropDownViewModel Gender { get; set; }

        [DisplayName("Season")]
        public EnumerableDropDownViewModel Season { get; set; }

        [DisplayName("Style")]
        public EnumerableDropDownViewModel Style { get; set; }

        public List<ProductsToShow> ProductList { get; set; }


        public void PopulateModel(List<ProductType> pt, string selectedBrand, string selectedColour, string selectedGender, string selectedSeason, string selectedStyle)
        {
            var allBrands = new BL.Populate().PopulateAllBrands();
            allBrands.Add("All", "All");
            Brand = new EnumerableDropDownViewModel(allBrands, selectedBrand == "" ? "All" : selectedBrand);
            Brand.Items = Brand.Items.OrderBy(t => t.Text).ToList();

            var allColours = new BL.Populate().PopulateAllColours();
            allColours.Add("All", "All");
            Colour = new EnumerableDropDownViewModel(allColours, selectedColour == "" ? "All" : selectedColour);
            Colour.Items = Colour.Items.OrderBy(t => t.Text).ToList();

            var g = new BL.Populate().PopulateAllGenders();
            g.Add("All", "All");
            Gender = new EnumerableDropDownViewModel(g, selectedGender == "" ? "All" : selectedGender);
            Gender.Items = Gender.Items.OrderBy(t => t.Text).ToList();

            var allSeasons = new BL.Populate().PopulateAllSeasons();
            allSeasons.Add("All", "All");
            Season = new EnumerableDropDownViewModel(allSeasons, selectedBrand == "" ? "All" : selectedBrand);
            Season.Items = Season.Items.OrderBy(t => t.Text).ToList();

            var allStyles = new BL.Populate().PopulateAllStyles();
            allStyles.Add("All", "All");
            Style = new EnumerableDropDownViewModel(allStyles, selectedStyle == "" ? "All" : selectedBrand);
            Style.Items = Style.Items.OrderBy(t => t.Text).ToList();
        }

        public void PopulateProductListFull(List<ProductType> pt)
        {
            ProductList = new List<ProductsToShow>();
            foreach (var categ in pt)
            {
                foreach (var subcateg in categ.SubCategories)
                {
                    foreach (var prod in subcateg.Products)
                    {
                        ProductList.Add(new ProductsToShow
                        {
                            Name = prod.Name,
                            ImgUrl = !String.IsNullOrEmpty(prod.Url) ? prod.Url : String.Empty,
                            Categ = categ.CategoryName,
                            SubCateg = subcateg.SubCategoryName,
                            BuyUrl = !String.IsNullOrEmpty(prod.BuyUrl) ? prod.BuyUrl : String.Empty
                        });
                    }
                }
            }
        }
    }

}