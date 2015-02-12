using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Common;
using SecProject.BL;
using SecProject.DAL;

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

        public static List<ProductType> AllProductTypes { get; set; }

        public static List<AuxProductModel> AuxProductsOntology { get; set; }

        public static List<AuxProductModel> AuxProductsWardrobe { get; set; }




        public void PopulateModel(List<ProductType> pt, string selectedGender, string selectedSeason, string selectedStyle)
        {
            var g = new Populate().PopulateAllGenders();
            g.Remove("Unisex");
            Gender = new EnumerableDropDownViewModel(g, selectedGender == "" ? g.Values.First() : selectedGender);
            Gender.Items = Gender.Items.OrderBy(t => t.Text).ToList();

            var allSeasons = new Populate().PopulateAllSeasons();
            allSeasons.Add("All", "All");
            Season = new EnumerableDropDownViewModel(allSeasons, selectedSeason == "" ? "All" : selectedSeason);
            Season.Items = Season.Items.OrderBy(t => t.Text).ToList();

            var allStyles = new Populate().PopulateAllStyles();
            allStyles.Add("All", "All");
            Style = new EnumerableDropDownViewModel(allStyles, selectedStyle == "" ? "All" : selectedStyle);
            Style.Items = Style.Items.OrderBy(t => t.Text).ToList();

            AllProductTypes = new List<ProductType>();
            AllProductTypes = pt;

            AuxProductsOntology = new List<AuxProductModel>();
            AuxProductsWardrobe = new List<AuxProductModel>();


            foreach (var categ in AllProductTypes)
            {
                foreach (var subcateg in categ.SubCategories)
                {
                    AuxProductsOntology.AddRange(subcateg.Products.Select
                        (prod => new AuxProductModel { Category = categ.CategoryName, ProductsInstance = prod, SubCategory = subcateg.SubCategoryName }));
                }
            }
            var userProfile = new DAL.UserProfile();
            var wardrobe = new List<UserProductLinkTables>();
            userProfile = new DALContext().GetUserProfile(HttpContext.Current.User.Identity.Name);
            if (userProfile != null)
                wardrobe = new DALContext().GetProductsByUserId(userProfile.UserId);
            ProductList = new List<ProductsToShow>();
            var list = wardrobe.Select(item => item.ProductName).ToList();

            foreach (var itemFromWordrobe in list)
            {
                foreach (var item in AuxProductsOntology)
                {
                    if (item.ProductsInstance.Name.ToUpper() == itemFromWordrobe.ToUpper())
                    {
                        AuxProductsWardrobe.Add(item);
                        break;
                    }
                }



            }

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

        public List<ProductsToShow> PopulateRandomList(string selectedGender, string selectedStyle, string selectedSeason)
        {
            var returnList = new List<ProductsToShow>();

            #region All
            if (selectedSeason.ToUpper() == "ALL" && selectedSeason.ToUpper() != "" &&
               selectedStyle.ToUpper() == "ALL" && selectedStyle.ToUpper() != "")
            {
                //var listOfItems = new List<string> {"RINGS", "NECKLACES", "BOOTS"};
                //var testBool = true;
                if (selectedGender.ToUpper() != "MALE")
                {

                    if (AuxProductsWardrobe.Any(f => f.Category.ToUpper() == "DRESSES"))
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.Category.ToUpper() == "DRESSES").ToList())));
                    }
                    else
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.Category.ToUpper() == "DRESSES").ToList())));
                    }
                    if (AuxProductsWardrobe.Any(f => f.SubCategory.ToUpper() == "RINGS"))
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.SubCategory.ToUpper() == "RINGS").ToList())));
                    }
                    else
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.SubCategory.ToUpper() == "RINGS").ToList())));
                    }
                    if (AuxProductsWardrobe.Any(f => f.SubCategory.ToUpper() == "NECKLACES"))
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.SubCategory.ToUpper() == "NECKLACES").ToList())));
                    }
                    else
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.SubCategory.ToUpper() == "NECKLACES").ToList())));
                    }
                    if (AuxProductsWardrobe.Any(f => f.SubCategory.ToUpper() == "BOOTS"))
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.SubCategory.ToUpper() == "BOOTS").ToList())));
                    }
                    else
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.SubCategory.ToUpper() == "BOOTS").ToList())));
                    }
                }
                else
                {
                    if (AuxProductsWardrobe.Any(f => f.Category.ToUpper() == "PANTS"))
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.Category.ToUpper() == "PANTS").ToList())));
                    }
                    else
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.Category.ToUpper() == "PANTS").ToList())));
                    }
                    if (AuxProductsWardrobe.Any(f => f.SubCategory.ToUpper() == "WATCHES"))
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.SubCategory.ToUpper() == "WATCHES").ToList())));
                    }
                    else
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.SubCategory.ToUpper() == "WATCHES").ToList())));
                    }
                    if (AuxProductsWardrobe.Any(f => f.SubCategory.ToUpper() == "SPORT_SHOES"))
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.SubCategory.ToUpper() == "SPORT_SHOES").ToList())));
                    }
                    else
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.SubCategory.ToUpper() == "SPORT_SHOES").ToList())));
                    }
                    if (AuxProductsWardrobe.Any(f => f.SubCategory.ToUpper() == "HOODIE"))
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.SubCategory.ToUpper() == "HOODIE").ToList())));
                    }
                    else
                    {
                        returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.SubCategory.ToUpper() == "HOODIE").ToList())));
                    }
                }
            }
            #endregion

                #region season

                if ((selectedSeason.ToUpper() == "WINTER" || selectedSeason=="AUTUMN") &&
                    selectedStyle.ToUpper() == "ALL" && selectedStyle.ToUpper() != "")
                {
                    if (selectedGender.ToUpper() != "MALE")
                    {

                        if (AuxProductsWardrobe.Any(f => f.Category.ToUpper() == "PANTS"))
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.Category.ToUpper() == "PANTS").ToList())));
                        }
                        else
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.Category.ToUpper() == "PANTS").ToList())));
                        }
                        if (AuxProductsWardrobe.Any(f => f.SubCategory.ToUpper() == "RINGS"))
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.SubCategory.ToUpper() == "RINGS").ToList())));
                        }
                        else
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.SubCategory.ToUpper() == "RINGS").ToList())));
                        }
                        if (AuxProductsWardrobe.Any(f => f.SubCategory.ToUpper() == "NECKLACES"))
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.SubCategory.ToUpper() == "NECKLACES").ToList())));
                        }
                        else
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.SubCategory.ToUpper() == "NECKLACES").ToList())));
                        }
                        if (AuxProductsWardrobe.Any(f => f.Category.ToUpper() == "SHOES"))
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.Category.ToUpper() == "BOOTS").ToList())));
                        }
                        else
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.Category.ToUpper() == "SHOES").ToList())));
                        }
                    }
                    else
                    {
                        if (AuxProductsWardrobe.Any(f => f.Category.ToUpper() == "PANTS"))
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.Category.ToUpper() == "PANTS").ToList())));
                        }
                        else
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.Category.ToUpper() == "PANTS").ToList())));
                        }
                        if (AuxProductsWardrobe.Any(f => f.SubCategory.ToUpper() == "WATCHES"))
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.SubCategory.ToUpper() == "WATCHES").ToList())));
                        }
                        else
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.SubCategory.ToUpper() == "WATCHES").ToList())));
                        }
                        if (AuxProductsWardrobe.Any(f => f.SubCategory.ToUpper() == "SPORT_SHOES"))
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.SubCategory.ToUpper() == "SPORT_SHOES").ToList())));
                        }
                        else
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.SubCategory.ToUpper() == "SPORT_SHOES").ToList())));
                        }
                        if (AuxProductsWardrobe.Any(f => f.SubCategory.ToUpper() == "HOODIE"))
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsWardrobe.Where(f => f.SubCategory.ToUpper() == "HOODIE").ToList())));
                        }
                        else
                        {
                            returnList.Add(GetProductsToShowFromRandom(GetRandomByType(AuxProductsOntology.Where(f => f.SubCategory.ToUpper() == "HOODIE").ToList())));
                        }
                        if (AuxProductsWardrobe.Any(f => f.SubCategory.ToUpper() == "BASIC_SWEATERS"))
                        {
                            returnList.Add(
                                GetProductsToShowFromRandom(
                                    GetRandomByType(
                                        AuxProductsWardrobe.Where(f => f.SubCategory.ToUpper() == "BASIC_SWEATERS" )
                                            .ToList())));
                        }
                        else
                        {
                            returnList.Add(
                                GetProductsToShowFromRandom(
                                    GetRandomByType(
                                        AuxProductsOntology.Where(f => f.SubCategory.ToUpper() == "BASIC_SWEATERS")
                                            .ToList())));
                        }
                    }
                }

                #endregion
          
            return returnList;
        }

        public ProductsToShow GetProductsToShowFromRandom(AuxProductModel random1)
        {
            return new ProductsToShow
                    {
                        BuyUrl = random1.ProductsInstance.BuyUrl,
                        Categ = random1.Category,
                        SubCateg = random1.SubCategory,
                        Name = random1.ProductsInstance.Name,
                        ImgUrl = random1.ProductsInstance.Url
                    };
        }

        public AuxProductModel GetRandom()
        {
            var ar = AuxProductsWardrobe;

            var r = new Random();
            var index = r.Next(0, ar.Count - 1);
            return ar[index];
        }
        public AuxProductModel GetRandomByType(List<AuxProductModel> list)
        {
           // if (list.Count == 0) return null;
            var ar = list;
            
            var r = new Random();
            var index = r.Next(0, ar.Count - 1);
            return ar[index];
        }
    }

}