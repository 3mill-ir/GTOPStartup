using GPLUSPLUS.Areas.seller.Models.Home;
using GPLUSPLUS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace GPLUSPLUS.Areas.seller.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /seller/Home/

        public ActionResult Index()
        {

            HomeShops model = new HomeShops();

              using (EntitiesConnection EC = new EntitiesConnection())
              {
                  Random rnd = new Random();
                  int rand = rnd.Next();
                  var RandomShops = (from c in EC.ShopImages where (c.Shop.ShopRegisterStatus == true) || (c.Shop.ShopRegisterStatus == false & SqlFunctions.DateDiff("ss",c.Shop.DateJoined, DateTime.Now) <= 7) select c).OrderBy(x => Guid.NewGuid()).Take(9).ToList();

                  foreach (var q in RandomShops)
                  {
                      ShopsToDisplay temp_shop = new ShopsToDisplay();
                      temp_shop.Shop_ID = q.Shop.Id;
                      temp_shop.ImageThumb = q.ShopThumbnail;
                      temp_shop.ImageThumbID = q.Id;
                      temp_shop.Image = q.ShopImage1;
                      model.RandomShops.Add(temp_shop);
                  }



                  var query_newShop = (from c in EC.ShopImages where (c.Shop.ShopRegisterStatus == true) || (c.Shop.ShopRegisterStatus == false & SqlFunctions.DateDiff("ss", c.Shop.DateJoined, DateTime.Now) <= 7) select c).OrderByDescending(c => c.Shop.DateJoined).Take(6);
                      foreach (var q in query_newShop)
                      {
                          ShopsToDisplay temp_shop = new ShopsToDisplay();
                          temp_shop.Shop_ID = q.Shop.Id;
                          temp_shop.ImageThumb = q.ShopThumbnail;
                          temp_shop.ImageThumbID = q.Id;
                          temp_shop.Image = q.ShopImage1;
                          temp_shop.ShopName = q.Shop.ShopName;
                          model.NewShops.Add(temp_shop);
                      }
            

                      if (User.Identity.IsAuthenticated)
                      {

                          using (UsersContext db = new UsersContext())
                          {
                              UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                              if (user != null)
                              {
                                  var query_newsmartShop = (from c in EC.ShopImages where (c.Shop.ShopRegisterStatus == true & c.Shop.ShopCity == user.U_City & c.Shop.ShopState == user.U_State) || (c.Shop.ShopRegisterStatus == false & SqlFunctions.DateDiff("ss", c.Shop.DateJoined, DateTime.Now) <= 7 & c.Shop.ShopCity == user.U_City & c.Shop.ShopState == user.U_State) orderby c.Shop.DateJoined descending select c).Take(6);
                                  foreach (var q in query_newsmartShop)
                                  {
                                      ShopsToDisplay temp_shop = new ShopsToDisplay();
                                      temp_shop.Shop_ID = q.Shop.Id;
                                      temp_shop.ImageThumb = q.ShopThumbnail;
                                      temp_shop.ImageThumbID = q.Id;
                                      temp_shop.Image = q.ShopImage1;
                                      temp_shop.ShopName = q.Shop.ShopName;
                                      model.NewSmartShops.Add(temp_shop);
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
