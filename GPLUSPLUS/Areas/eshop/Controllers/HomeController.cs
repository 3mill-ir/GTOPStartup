using GPLUSPLUS.Areas.eshop.Models.Home;
using GPLUSPLUS.Filters;
using GPLUSPLUS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace GPLUSPLUS.Areas.eshop.Controllers
{

     //[InitializeSimpleMembership]
    public class HomeController : Controller
    {
        //
        // GET: /eshop/Home/

        public ActionResult Index()
        {
            HomeProducts model = new HomeProducts();
           
              using (EntitiesConnection EC = new EntitiesConnection())
            {
              //  var ProductTypes = from c in EC.Products  group c by new {c.ProductType.Name , c.ProductType.Id} into grp select new {
              // Name= grp.Key.Name,
              // Id = grp.Key.Id,
              //count=grp.Count(),
              //Count_Normal=(from c in grp where c.P_ScoreCost<=1000 select c).Count(),
              // Count_Boronz = (from c in grp where c.P_ScoreCost > 1000 && c.P_ScoreCost<=5000 select c).Count(),
              // Count_Silver = (from c in grp where c.P_ScoreCost >5000 && c.P_ScoreCost<=15000 select c).Count(),
              // Count_Gold = (from c in grp where c.P_ScoreCost > 15000 select c).Count(),
              //  } ;

              //  foreach (var q in ProductTypes)
              //  {
              //            ProductTypesList temp_product = new ProductTypesList();
              //            temp_product.ProductTypesId = q.Id;
              //      temp_product.ProductTypesName=q.Name;
              //      temp_product.ProductTypesCount =q.count;
              //      temp_product.ProductTypesCount_Normal = q.Count_Normal;
              //      temp_product.ProductTypesCount_Boronz = q.Count_Boronz;
              //      temp_product.ProductTypesCount_Silver = q.Count_Silver;
              //      temp_product.ProductTypesCount_Gold = q.Count_Gold;
              //      model.ProductTypes.Add(temp_product);
              //  }




                  Random rnd = new Random();
                  int rand = rnd.Next();
                  var RandomProducts = EC.ProductImages.OrderBy(x=>Guid.NewGuid()).Take(9).ToList();
                  foreach (var q in RandomProducts)
                  {
                      ProductsToDisplay temp_product = new ProductsToDisplay();
                      temp_product.P_Brand = q.Product.P_Brand;
                      temp_product.P_Description = q.Product.P_Description;
                      temp_product.P_ID = q.Product.P_ID;
                      temp_product.P_ModelName = q.Product.P_ModelName;
                      temp_product.P_ScoreCost = q.Product.P_ScoreCost;


                          temp_product.ImageThumb.Add(q.PI_Thumb);
                          temp_product.ImageThumbID.Add(q.PI_ID);
                     
                      if (q.Product.P_ScoreCost <= 100000)
                          temp_product.ScoreClass = "Normal";
                      if (q.Product.P_ScoreCost > 100000 && q.Product.P_ScoreCost <= 500000)
                          temp_product.ScoreClass="Boronz";
                      if (q.Product.P_ScoreCost > 500000 && q.Product.P_ScoreCost <= 1500000)
                          temp_product.ScoreClass="Silver";
                      if (q.Product.P_ScoreCost > 1500000)
                          temp_product.ScoreClass="Gold";
                      model.RandomProducts.Add(temp_product);
                  }




                      var query_normal = (from c in EC.ProductImages where c.Product.P_ScoreCost <= 100000 select c).OrderByDescending(c => c.Product.P_DateAdded).Take(4);
                      foreach (var q in query_normal)
                      {
                          ProductsToDisplay temp_product = new ProductsToDisplay();
                          temp_product.P_Brand = q.Product.P_Brand;
                          temp_product.P_Description = q.Product.P_Description;
                          temp_product.P_ID = q.Product.P_ID;
                          temp_product.P_ModelName = q.Product.P_ModelName;
                          temp_product.P_ScoreCost = q.Product.P_Cost; // new add
                          temp_product.ImageThumb.Add(q.PI_Thumb);
                          temp_product.ImageThumbID.Add(q.PI_ID);
                          model.NewProducts_Normal.Add(temp_product);
                      }

                      var query_boronz = (from c in EC.ProductImages where c.Product.P_ScoreCost > 100000 & c.Product.P_ScoreCost <= 500000 select c).OrderByDescending(c => c.Product.P_DateAdded).Take(4);
                      foreach (var q in query_boronz)
                      {
                          ProductsToDisplay temp_product = new ProductsToDisplay();
                          temp_product.P_Brand = q.Product.P_Brand;
                          temp_product.P_Description = q.Product.P_Description;
                          temp_product.P_ID = q.Product.P_ID;
                          temp_product.P_ModelName = q.Product.P_ModelName;
                          temp_product.P_ScoreCost = q.Product.P_Cost;
                          temp_product.ImageThumb.Add(q.PI_Thumb);
                          temp_product.ImageThumbID.Add(q.PI_ID);
                          model.NewProducts_Boronz.Add(temp_product);
                      }

                      var query_silver = (from c in EC.ProductImages where c.Product.P_ScoreCost > 500000 & c.Product.P_ScoreCost <= 1500000 select c).OrderByDescending(c => c.Product.P_DateAdded).Take(4);
                      foreach (var q in query_silver)
                      {
                          ProductsToDisplay temp_product = new ProductsToDisplay();
                          temp_product.P_Brand = q.Product.P_Brand;
                          temp_product.P_Description = q.Product.P_Description;
                          temp_product.P_ID = q.Product.P_ID;
                          temp_product.P_ModelName = q.Product.P_ModelName;
                          temp_product.P_ScoreCost = q.Product.P_Cost;
                          temp_product.ImageThumb.Add(q.PI_Thumb);
                          temp_product.ImageThumbID.Add(q.PI_ID);
                          model.NewProducts_Silver.Add(temp_product);
                      }


                      var query_gold = (from c in EC.ProductImages where c.Product.P_ScoreCost > 1500000 select c).OrderByDescending(c => c.Product.P_DateAdded).Take(4);
                      foreach (var q in query_gold)
                      {
                          ProductsToDisplay temp_product = new ProductsToDisplay();
                          temp_product.P_Brand = q.Product.P_Brand;
                          temp_product.P_Description = q.Product.P_Description;
                          temp_product.P_ID = q.Product.P_ID;
                          temp_product.P_ModelName = q.Product.P_ModelName;
                          temp_product.P_ScoreCost = q.Product.P_Cost;
                          temp_product.ImageThumb.Add(q.PI_Thumb);
                          temp_product.ImageThumbID.Add(q.PI_ID);
                          model.NewProducts_Gold.Add(temp_product);
                      }



                      if (User.Identity.IsAuthenticated) { 

                           using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                    if (user != null)
                    {
                          var ScoreLowproducts= (from c in EC.ProductImages where c.Product.P_ScoreCost<=user.U_Score orderby c.Product.P_ScoreCost descending select c).Take(6);
                          foreach (var q in ScoreLowproducts)
                          {
                              ProductsToDisplay temp_product = new ProductsToDisplay();
                              temp_product.P_Brand = q.Product.P_Brand;
                              temp_product.P_Description = q.Product.P_Description;
                              temp_product.P_ID = q.Product.P_ID;
                              temp_product.P_ModelName = q.Product.P_ModelName;
                              temp_product.P_ScoreCost = q.Product.P_Cost;
                              temp_product.ImageThumb.Add(q.PI_Thumb);
                              temp_product.ImageThumbID.Add(q.PI_ID);
                              model.ScoreLowProducts.Add(temp_product);
                          }


                          var ScoreHighproducts = (from c in EC.ProductImages where c.Product.P_ScoreCost > user.U_Score orderby c.Product.P_ScoreCost ascending select c).Take(6);
                          foreach (var q in ScoreHighproducts)
                          {
                              ProductsToDisplay temp_product = new ProductsToDisplay();
                              temp_product.P_Brand = q.Product.P_Brand;
                              temp_product.P_Description = q.Product.P_Description;
                              temp_product.P_ID = q.Product.P_ID;
                              temp_product.P_ModelName = q.Product.P_ModelName;
                              temp_product.P_ScoreCost = q.Product.P_Cost;
                              temp_product.ImageThumb.Add(q.PI_Thumb);
                              temp_product.ImageThumbID.Add(q.PI_ID);
                              model.ScoreHighProducts.Add(temp_product);
                          }


                          string welcome = "جناب ";
                          if (user.U_Gender.Equals("مرد"))
                              welcome = welcome + "آقای ";
                          else
                              welcome = welcome + "خانم ";
                          @ViewBag.UserFullName = user.U_FirstName + " " + user.U_LastName;
                          @ViewBag.Userwelcome = welcome;
                    }
                           }
                    
                      
                      
                      }
                  
      }
            
            return View(model);
        }

    }
}
