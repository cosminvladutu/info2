using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Common;
using SecProject.BL;

namespace SecProject.Models
{
    public class WhatToWearMainViewModel
    {
        [DisplayName("Gender")]
        public EnumerableDropDownViewModel Gender { get; set; }

        [DisplayName("Season")]
        public EnumerableDropDownViewModel Season { get; set; }

        [DisplayName("Style")]
        public EnumerableDropDownViewModel Style { get; set; }

        public List<ProductsToShow> ProductList { get; set; }


        public void PopulateModel(List<ProductType> pt, string selectedBrand, string selectedColour, string selectedGender, string selectedSeason, string selectedStyle)
        {
            var g = new Populate().PopulateAllGenders();
            g.Add("All", "All");
            Gender = new EnumerableDropDownViewModel(g, selectedGender == "" ? "All" : selectedGender);
            Gender.Items = Gender.Items.OrderBy(t => t.Text).ToList();

            var allSeasons = new Populate().PopulateAllSeasons();
            allSeasons.Add("All", "All");
            Season = new EnumerableDropDownViewModel(allSeasons, selectedBrand == "" ? "All" : selectedBrand);
            Season.Items = Season.Items.OrderBy(t => t.Text).ToList();

            var allStyles = new Populate().PopulateAllStyles();
            allStyles.Add("All", "All");
            Style = new EnumerableDropDownViewModel(allStyles, selectedStyle == "" ? "All" : selectedBrand);
            Style.Items = Style.Items.OrderBy(t => t.Text).ToList();
        }

        //public void PopulateProductFromWardrobe(List<ProductType> pt, string subcategoryName, string selectedBrand,
        //    string selectedColour, string selectedGender,
        //    string selectedSeason, string selectedStyle)
        //{
        //    var userProfile = new DAL.UserProfile();
        //    var wardrobe = new List<UserProductLinkTables>();
        //    userProfile = new DALContext().GetUserProfile(HttpContext.Current.User.Identity.Name);
        //    if (userProfile != null)
        //        wardrobe = new DALContext().GetProductsByUserId(userProfile.UserId);
        //    ProductList = new List<ProductsToShow>();
        //    var list = new List<string>();
        //    foreach (var item in wardrobe)
        //    {
        //        list.Add(item.ProductName);
        //    }
        //    foreach (var categ in pt)
        //    {
        //        if (subcategoryName != null)
        //        {
        //            foreach (var subcateg in categ.SubCategories.Where(f => f.SubCategoryName.ToUpper() == subcategoryName.ToUpper()))
        //            {
        //                FillProductsToShows(subcateg, categ, selectedBrand, selectedColour, selectedGender, selectedSeason, selectedStyle, list);
        //            }
        //        }
        //        else
        //        {
        //            foreach (var subcateg in categ.SubCategories)
        //            {
        //                FillProductsToShows(subcateg, categ, selectedBrand, selectedColour, selectedGender, selectedSeason, selectedStyle, list);
        //            }
        //        }
        //    }
        //}
    }

}