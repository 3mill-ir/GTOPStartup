using GPLUSPLUS.Models;
using GPLUSPLUS.Models.Bussiness;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;


namespace GPLUSPLUS.Controllers
{

    public class GTOPController : Controller
    {
        //
        // GET: /GTOP/

        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult Index()
        {

            return View();
        }

        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult ProductsList()
        {
            EntitiesConnection EC_DB = new EntitiesConnection();
            return View(EC_DB.Products.ToList());
        }

        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult ProductCreate()
        {
            ViewData["ProductType"] = ProductManagement.LoadProductType();
            return View();
        }
        [Authorize(Users = "09144390137,09141863449")]
        [HttpPost]
        public ActionResult ProductCreate(ProductCustom model)
        {
            ViewData["ProductType"] = ProductManagement.LoadProductType();
            if (ModelState.IsValid)
            {
                EntitiesConnection EC_DB = new EntitiesConnection();
                Product temp_product = new Product();


                temp_product.P_Brand = model.P_Brand;
                temp_product.P_Name = model.P_Name;
                temp_product.P_Description = model.P_Description;
                temp_product.P_Cost = model.P_Cost;
                double scorecoste = model.P_Cost ?? default(double);
                temp_product.P_ScoreCost = (int)Math.Round(((scorecoste + (0.37 * scorecoste)) / 1000) * 100);
                temp_product.P_ModelName = model.P_ModelName;
                temp_product.P_DateAdded = DateTime.Now;
                temp_product.P_Count = model.P_Count;
                temp_product.P_Status = true;

                ProductType PType = EC_DB.ProductTypes.FirstOrDefault(u => u.Name == model.P_Category);
                temp_product.F_ProductType_ID = PType.Id;
                EC_DB.Products.Add(temp_product);

                ProductHistory ph = new ProductHistory();
                ph.OperationDate = DateTime.Now;
                ph.OperationHistory = "Create Product," + model.P_Category;
                ph.F_P_ID = temp_product.P_ID;
                ph.F_UserID = WebSecurity.CurrentUserId;
                EC_DB.ProductHistories.Add(ph);
                EC_DB.SaveChanges();
                return RedirectToAction("ProductsList");
            }
            return View(model);
        }

        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult ProductEdit(int ID)
        {
            ViewData["ProductType"] = ProductManagement.LoadProductType();
            EntitiesConnection EC_DB = new EntitiesConnection();
            Product temp_product = EC_DB.Products.FirstOrDefault(u => u.P_ID == ID);
            ProductCustom model = new ProductCustom();

            model.P_ID = temp_product.P_ID;
            //  model.P_Category = temp_product.P_Category;
            model.P_Name = temp_product.P_Name;
            model.P_Brand = temp_product.P_Brand;
            model.P_Description = temp_product.P_Description;
            model.P_ScoreCost = temp_product.P_ScoreCost;
            model.P_ModelName = temp_product.P_ModelName;
            model.P_DateAdded = temp_product.P_DateAdded;
            model.P_Count = temp_product.P_Count;
            model.P_Cost = temp_product.P_Cost;
            model.P_Category = temp_product.ProductType.Name;
            model.P_Status = temp_product.P_Status;
            return View(model);
        }
        [Authorize(Users = "09144390137,09141863449")]
        [HttpPost]
        public ActionResult ProductEdit(ProductCustom model, int ID)
        {
            ViewData["ProductType"] = ProductManagement.LoadProductType();

            EntitiesConnection EC_DB = new EntitiesConnection();
            Product temp_product = EC_DB.Products.FirstOrDefault(u => u.P_ID == ID);
            if (temp_product != null)
            {
                if (ModelState.IsValid)
                {

                    //      temp_product.P_Category = model.P_Category;
                    temp_product.P_Brand = model.P_Brand;
                    temp_product.P_Description = model.P_Description;
                    temp_product.P_ScoreCost = model.P_ScoreCost;
                    temp_product.P_ModelName = model.P_ModelName;

                    temp_product.P_Count = model.P_Count;
                    temp_product.P_Status = model.P_Status;
                    temp_product.P_Cost = model.P_Cost;
                    double scorecoste = model.P_Cost ?? default(double);
                    temp_product.P_ScoreCost = (int)Math.Round(((scorecoste + (0.37 * scorecoste)) / 1000) * 100);
                    ProductType PType = EC_DB.ProductTypes.FirstOrDefault(u => u.Name == model.P_Category);
                    temp_product.F_ProductType_ID = PType.Id;
                    temp_product.P_Name = model.P_Name;

                    ProductHistory ph = new ProductHistory();
                    ph.OperationDate = DateTime.Now;
                    //  ph.OperationHistory = "Edit," + temp_product.P_Category + ">" + model.P_Category + "," + temp_product.P_ID;
                    ph.F_P_ID = temp_product.P_ID;
                    ph.F_UserID = WebSecurity.CurrentUserId;
                    EC_DB.ProductHistories.Add(ph);


                    EC_DB.SaveChanges();
                    return RedirectToAction("ProductsList");
                }
            }
            return View(model);
        }

        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult ProductDelete(int ID)
        {

            EntitiesConnection EC_DB = new EntitiesConnection();
            Product temp_product = EC_DB.Products.FirstOrDefault(u => u.P_ID == ID);
            ProductCustom model = new ProductCustom();

            model.P_ID = temp_product.P_ID;
            //  model.P_Category = temp_product.P_Category;
            model.P_Name = temp_product.P_Name;
            model.P_Brand = temp_product.P_Brand;
            model.P_Description = temp_product.P_Description;
            model.P_ScoreCost = temp_product.P_ScoreCost;
            model.P_ModelName = temp_product.P_ModelName;
            model.P_DateAdded = temp_product.P_DateAdded;
            model.P_Count = temp_product.P_Count;
            model.P_Status = temp_product.P_Status;
            model.P_Category = temp_product.ProductType.Name;



            return View(model);
        }

        [Authorize(Users = "09144390137,09141863449")]
        [HttpPost]
        public ActionResult ProductDelete(ProductCustom model, int ID)
        {

            EntitiesConnection EC_DB = new EntitiesConnection();
            Product temp_product = EC_DB.Products.FirstOrDefault(u => u.P_ID == ID);
            if (temp_product != null)
            {



                ProductHistory ph = new ProductHistory();
                ph.OperationDate = DateTime.Now;
                //  ph.OperationHistory = "Delete," + temp_product.P_Category + "," + temp_product.P_ID;
                ph.F_UserID = WebSecurity.CurrentUserId;
                EC_DB.ProductHistories.Add(ph);
                EC_DB.Products.Remove(temp_product);
                EC_DB.SaveChanges();
            }
            return RedirectToAction("ProductsList");

        }


        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult ProductDetails(int ID)
        {

            EntitiesConnection EC_DB = new EntitiesConnection();
            Product temp_product = EC_DB.Products.FirstOrDefault(u => u.P_ID == ID);
            ProductCustomDetails model = new ProductCustomDetails();

            model.P_ID = temp_product.P_ID;
            //  model.P_Category = temp_product.P_Category;
            model.P_Brand = temp_product.P_Brand;
            model.P_Description = temp_product.P_Description;
            model.P_ScoreCost = temp_product.P_ScoreCost;
            model.P_ModelName = temp_product.P_ModelName;
            model.P_DateAdded = temp_product.P_DateAdded;
            model.P_Count = temp_product.P_Count;
            model.P_Status = temp_product.P_Status;
            model.P_Category = temp_product.ProductType.Name;
            model.P_Cost = temp_product.P_Cost;
            model.P_Name = temp_product.P_Name;

            var query = from c in EC_DB.ProductImages
                        where c.F_P_ID == ID
                        select c;


            if (query.Count() > 0 || query != null)
            {
                foreach (var row in query)
                {

                    string imageBase64Data = Convert.ToBase64String(row.PI_Thumb);
                    string temp_imgURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                    if (!string.IsNullOrEmpty(temp_imgURL))
                    {
                        model.ImageThumb.Add(temp_imgURL);
                        model.ImageThumbID.Add(row.PI_ID);
                    }
                }

            }

            return View(model);
        }

        [Authorize(Users = "09144390137,09141863449")]
        [HttpPost]
        public ActionResult ProductDetails(ProductCustomDetails model, int ID)
        {
            if (ModelState.IsValid)
            {
                if (model.PI_Image != null)
                {
                    if (model.PI_Image.ContentType == "image/png" || model.PI_Image.ContentType == "image/jpg" || model.PI_Image.ContentType == "image/jpeg" || model.PI_Image.ContentType == "image/gif" || model.PI_Image.ContentType == "image/bmp")
                    {



                        if (model.PI_Image.ContentLength > 0)
                        {
                            EntitiesConnection EC_DB = new EntitiesConnection();
                            byte[] image = new byte[model.PI_Image.ContentLength];
                            model.PI_Image.InputStream.Read(image, 0, image.Length);

                            ProductImage temp_PI = new ProductImage();
                            temp_PI.F_P_ID = ID;
                            Byte[] result
               = (Byte[])new ImageConverter().ConvertTo(ProductManagement.CreateThumbnail(image, 100, 100, true), typeof(Byte[]));
                            temp_PI.PI_Thumb = result;
                            temp_PI.PI_Image = image;
                            EC_DB.ProductImages.Add(temp_PI);


                            ProductHistory ph = new ProductHistory();
                            ph.OperationDate = DateTime.Now;
                            ph.OperationHistory = "Add Pic," + @ViewBag.P_Category + "," + @ViewBag.P_ID;
                            ph.F_UserID = WebSecurity.CurrentUserId;
                            EC_DB.ProductHistories.Add(ph);

                            EC_DB.SaveChanges();


                            return RedirectToAction("ProductDetails", new { ID = ID });

                        }
                    }
                    else
                    {
                        ModelState.AddModelError("PI_Image", " فرمت فایل نادرست است.");
                        return View(model);
                    }
                }
            }


            return View(model);
        }

        //public string resize_img()
        //{
        //    using (EntitiesConnection EC_DB = new EntitiesConnection())
        //    {
        //        var query = from c in EC_DB.ProductImages select new { c.PI_ID, c.PI_Image };

        //        foreach (var q in query)
        //        {

        //            Byte[] result
        //        = (Byte[])new ImageConverter().ConvertTo(ProductManagement.CreateThumbnail(q.PI_Image, 200, 200, true), typeof(Byte[]));

        //            ProductImage PI = EC_DB.ProductImages.FirstOrDefault(u => u.PI_ID == q.PI_ID);
        //            if (PI != null)
        //            {
        //                PI.PI_Thumb = result;
        //            }
        //            EC_DB.SaveChanges();
        //        }
        //    }
        //    return "OK";
        //}


        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult PicDelete(int ID)
        {

            EntitiesConnection EC_DB = new EntitiesConnection();
            ProductImage temp_image = EC_DB.ProductImages.FirstOrDefault(u => u.PI_ID == ID);
            ProductCustomDetails model = new ProductCustomDetails();
            string imageBase64Data = Convert.ToBase64String(temp_image.PI_Image);
            string temp_imgURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
            model.P_ID = temp_image.F_P_ID ?? default(int);
            model.ImageThumbID.Add(temp_image.PI_ID);
            model.ImageThumb.Add(temp_imgURL);

            return View(model);
        }

        [Authorize(Users = "09144390137,09141863449")]
        [HttpPost]
        public ActionResult PicDelete(ProductCustomDetails model, int ID)
        {

            EntitiesConnection EC_DB = new EntitiesConnection();
            ProductImage temp_image = EC_DB.ProductImages.FirstOrDefault(u => u.PI_ID == ID);
            if (temp_image != null)
            {
                int? F_P_ID = temp_image.F_P_ID;
                EC_DB.ProductImages.Remove(temp_image);

                ProductHistory ph = new ProductHistory();
                ph.OperationDate = DateTime.Now;
                ph.OperationHistory = "Delete Pic," + @ViewBag.P_Category + "," + F_P_ID;
                ph.F_UserID = WebSecurity.CurrentUserId;
                EC_DB.ProductHistories.Add(ph);
                EC_DB.SaveChanges();
                return RedirectToAction("ProductDetails", new { ID = F_P_ID });
            }
            return RedirectToAction("ProductsList");

        }



        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult ShopTypesList()
        {
            EntitiesConnection EC_DB = new EntitiesConnection();


            return View(EC_DB.ShopTypes.ToList());
        }

        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult ShopTypeCreate()
        {
            return View();
        }

        [Authorize(Users = "09144390137,09141863449")]
        [HttpPost]
        public ActionResult ShopTypeCreate(ShopTypeCustom model)
        {
            if (ModelState.IsValid)
            {

                EntitiesConnection EC_DB = new EntitiesConnection();
                ShopType temp_ShopType = new ShopType();
                temp_ShopType.Name = model.ST_Name;
                temp_ShopType.DescriptionOf = model.ST_DescriptionOf;
                if (model.ST_ImageOf != null)
                {
                    if (model.ST_ImageOf.ContentType == "image/png" || model.ST_ImageOf.ContentType == "image/jpg" || model.ST_ImageOf.ContentType == "image/jpeg" || model.ST_ImageOf.ContentType == "image/gif" || model.ST_ImageOf.ContentType == "image/bmp")
                    {

                        if (model.ST_ImageOf.ContentLength > 0)
                        {
                            byte[] image = new byte[model.ST_ImageOf.ContentLength];
                            model.ST_ImageOf.InputStream.Read(image, 0, image.Length);
                            Byte[] result
             = (Byte[])new ImageConverter().ConvertTo(ProductManagement.CreateThumbnail(image, 100, 100, true), typeof(Byte[]));
                            temp_ShopType.ImageOf = result;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ST_Image", " فرمت فایل نادرست است.");
                        return View(model);
                    }
                }
                EC_DB.ShopTypes.Add(temp_ShopType);

                //ProductHistory ph = new ProductHistory();
                //ph.OperationDate = DateTime.Now;
                //ph.OperationHistory = "Create Product," + model.P_Category;
                //ph.F_P_ID = temp_product.P_ID;
                //ph.F_UserID = WebSecurity.CurrentUserId;
                //EC_DB.ProductHistories.Add(ph);
                EC_DB.SaveChanges();
                return RedirectToAction("ShopTypesList");



            }
            return View(model);
        }


        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult ShopTypeEdit(int ID)
        {
            EntitiesConnection EC_DB = new EntitiesConnection();
            ShopType temp_ShopType = EC_DB.ShopTypes.FirstOrDefault(u => u.Id == ID);
            ShopTypeCustom model = new ShopTypeCustom();

            model.ST_ID = temp_ShopType.Id;
            model.ST_Name = temp_ShopType.Name;
            model.ST_DescriptionOf = temp_ShopType.DescriptionOf;
            if (temp_ShopType.ImageOf != null)
            {
                string imageBase64Data = Convert.ToBase64String(temp_ShopType.ImageOf);
                string temp_imgURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                model.ImageThumb = temp_imgURL;
            }

            return View(model);
        }

        [Authorize(Users = "09144390137,09141863449")]
        [HttpPost]
        public ActionResult ShopTypeEdit(ShopTypeCustom model, int ID, string submitValue)
        {

            EntitiesConnection EC_DB = new EntitiesConnection();
            ShopType temp_ShopType = EC_DB.ShopTypes.FirstOrDefault(u => u.Id == ID);
            if (temp_ShopType != null)
            {
                if (submitValue.Equals("ذخیره"))
                {

                    temp_ShopType.Name = model.ST_Name;
                    temp_ShopType.DescriptionOf = model.ST_DescriptionOf;
                    if (model.ST_ImageOf != null)
                    {
                        if (model.ST_ImageOf.ContentType == "image/png" || model.ST_ImageOf.ContentType == "image/jpg" || model.ST_ImageOf.ContentType == "image/jpeg" || model.ST_ImageOf.ContentType == "image/gif" || model.ST_ImageOf.ContentType == "image/bmp")
                        {

                            if (model.ST_ImageOf.ContentLength > 0)
                            {
                                byte[] image = new byte[model.ST_ImageOf.ContentLength];
                                model.ST_ImageOf.InputStream.Read(image, 0, image.Length);
                                Byte[] result
           = (Byte[])new ImageConverter().ConvertTo(ProductManagement.CreateThumbnail(image, 100, 100, true), typeof(Byte[]));
                                temp_ShopType.ImageOf = result;
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("ST_ImageOf", " فرمت فایل نادرست است.");
                            return View(model);
                        }
                    }

                    EC_DB.SaveChanges();
                    return RedirectToAction("ShopTypesList");
                }
                else if (submitValue.Equals("حذف عکس"))
                {

                    temp_ShopType.ImageOf = null;
                    EC_DB.SaveChanges();
                    return View(model);

                }
            }

            return View(model);
        }



        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult ShopTypeDelete(int ID)
        {

            EntitiesConnection EC_DB = new EntitiesConnection();
            ShopType temp_ShopType = EC_DB.ShopTypes.FirstOrDefault(u => u.Id == ID);
            ShopTypeCustom model = new ShopTypeCustom();

            model.ST_ID = temp_ShopType.Id;
            model.ST_Name = temp_ShopType.Name;
            model.ST_DescriptionOf = temp_ShopType.DescriptionOf;
            if (temp_ShopType.ImageOf != null)
            {
                string imageBase64Data = Convert.ToBase64String(temp_ShopType.ImageOf);
                string temp_imgURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                model.ImageThumb = temp_imgURL;
            }


            return View(model);
        }

        [Authorize(Users = "09144390137,09141863449")]
        [HttpPost]
        public ActionResult ShopTypeDelete(ShopTypeCustom model, int ID)
        {

            EntitiesConnection EC_DB = new EntitiesConnection();
            ShopType temp_ShopType = EC_DB.ShopTypes.FirstOrDefault(u => u.Id == ID);
            if (temp_ShopType != null)
            {

                EC_DB.ShopTypes.Remove(temp_ShopType);
                EC_DB.SaveChanges();
            }
            return RedirectToAction("ShopTypesList");

        }


        //********************************
        [Authorize(Users = "himan2z")]
        public ActionResult GenerateCode(string Id)
        {
            if (Id.Equals("PARSADORSACODE"))
            {
                string SecureCode = "PARSADORSA";
                EntitiesConnection EC_DB = new EntitiesConnection();

                CodeManagement CM = new CodeManagement();
                List<string[]> CodesList = CM.GenerateCodeMethod(200, 200, 200, 200, 200, 200, 200, 200, 200);
                int i = 0;
                foreach (var element in CodesList)
                {
                    ScoreCard Sc = new ScoreCard();
                    Sc.CodeNumber = StringCipher.Encrypt(element[0], SecureCode);
                    Sc.TrackingCode = element[1];
                    Sc.CodeScore = Int32.Parse(element[2]);
                    Sc.CardStatus = true;
                    Sc.GenereateDate = DateTime.Now;
                    Sc.ExpiredDate = DateTime.Now.AddYears(2);
                    EC_DB.ScoreCards.Add(Sc);
                    i++;
                    if (i % 100 == 0)
                    {
                        EC_DB.SaveChanges();
                    }

                }
                EC_DB.SaveChanges();

                return View(CodesList);
            }
            else
            {
                return View("Index");
            }
        }










        ///***************Product Type 
        ///
        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult ProductTypesList()
        {
            EntitiesConnection EC_DB = new EntitiesConnection();


            return View(EC_DB.ProductTypes.ToList());
        }

        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult ProductTypeCreate()
        {
            return View();
        }

        [Authorize(Users = "09144390137,09141863449")]
        [HttpPost]
        public ActionResult ProductTypeCreate(ProductTypeCustom model)
        {
            if (ModelState.IsValid)
            {

                EntitiesConnection EC_DB = new EntitiesConnection();
                ProductType temp_ProductType = new ProductType();
                temp_ProductType.Name = model.PT_Name;
                temp_ProductType.DescriptionOf = model.PT_DescriptionOf;
                if (model.PT_ImageOf != null)
                {
                    if (model.PT_ImageOf.ContentType == "image/png" || model.PT_ImageOf.ContentType == "image/jpg" || model.PT_ImageOf.ContentType == "image/jpeg" || model.PT_ImageOf.ContentType == "image/gif" || model.PT_ImageOf.ContentType == "image/bmp")
                    {

                        if (model.PT_ImageOf.ContentLength > 0)
                        {
                            byte[] image = new byte[model.PT_ImageOf.ContentLength];
                            model.PT_ImageOf.InputStream.Read(image, 0, image.Length);
                            Byte[] result
             = (Byte[])new ImageConverter().ConvertTo(ProductManagement.CreateThumbnail(image, 100, 100, true), typeof(Byte[]));
                            temp_ProductType.ImageOf = result;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("PT_Image", " فرمت فایل نادرست است.");
                        return View(model);
                    }
                }
                EC_DB.ProductTypes.Add(temp_ProductType);

                //ProductHistory ph = new ProductHistory();
                //ph.OperationDate = DateTime.Now;
                //ph.OperationHistory = "Create Product," + model.P_Category;
                //ph.F_P_ID = temp_product.P_ID;
                //ph.F_UserID = WebSecurity.CurrentUserId;
                //EC_DB.ProductHistories.Add(ph);
                EC_DB.SaveChanges();
                return RedirectToAction("ProductTypesList");


            }
            return View(model);
        }


        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult ProductTypeEdit(int ID)
        {
            EntitiesConnection EC_DB = new EntitiesConnection();
            ProductType temp_ProductType = EC_DB.ProductTypes.FirstOrDefault(u => u.Id == ID);
            ProductTypeCustom model = new ProductTypeCustom();

            model.PT_ID = temp_ProductType.Id;
            model.PT_Name = temp_ProductType.Name;
            model.PT_DescriptionOf = temp_ProductType.DescriptionOf;
            if (temp_ProductType.ImageOf != null)
            {
                string imageBase64Data = Convert.ToBase64String(temp_ProductType.ImageOf);
                string temp_imgURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                model.ImageThumb = temp_imgURL;
            }


            return View(model);
        }

        [Authorize(Users = "09144390137,09141863449")]
        [HttpPost]
        public ActionResult ProductTypeEdit(ProductTypeCustom model, int ID, string submitValue)
        {

            EntitiesConnection EC_DB = new EntitiesConnection();
            ProductType temp_ProductType = EC_DB.ProductTypes.FirstOrDefault(u => u.Id == ID);
            if (temp_ProductType != null)
            {
                if (submitValue.Equals("ذخیره"))
                {

                    temp_ProductType.Name = model.PT_Name;
                    temp_ProductType.DescriptionOf = model.PT_DescriptionOf;
                    if (model.PT_ImageOf != null)
                    {

                        if (model.PT_ImageOf.ContentType == "image/png" || model.PT_ImageOf.ContentType == "image/jpg" || model.PT_ImageOf.ContentType == "image/jpeg" || model.PT_ImageOf.ContentType == "image/gif" || model.PT_ImageOf.ContentType == "image/bmp")
                        {

                            if (model.PT_ImageOf.ContentLength > 0)
                            {
                                byte[] image = new byte[model.PT_ImageOf.ContentLength];
                                model.PT_ImageOf.InputStream.Read(image, 0, image.Length);
                                Byte[] result
           = (Byte[])new ImageConverter().ConvertTo(ProductManagement.CreateThumbnail(image, 100, 100, true), typeof(Byte[]));
                                temp_ProductType.ImageOf = result;
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("PT_ImageOf", " فرمت فایل نادرست است.");
                            return View(model);
                        }
                    }

                    EC_DB.SaveChanges();
                    return RedirectToAction("ProductTypesList");
                }
                else if (submitValue.Equals("حذف عکس"))
                {

                    temp_ProductType.ImageOf = null;
                    EC_DB.SaveChanges();
                    return View(model);

                }

            }

            return View(model);
        }



        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult ProductTypeDelete(int ID)
        {

            EntitiesConnection EC_DB = new EntitiesConnection();
            ProductType temp_ProductType = EC_DB.ProductTypes.FirstOrDefault(u => u.Id == ID);
            ProductTypeCustom model = new ProductTypeCustom();

            model.PT_ID = temp_ProductType.Id;
            model.PT_Name = temp_ProductType.Name;
            model.PT_DescriptionOf = temp_ProductType.DescriptionOf;
            if (temp_ProductType.ImageOf != null)
            {
                string imageBase64Data = Convert.ToBase64String(temp_ProductType.ImageOf);
                string temp_imgURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                model.ImageThumb = temp_imgURL;
            }


            return View(model);
        }


        [Authorize(Users = "09144390137,09141863449")]
        [HttpPost]
        public ActionResult ProductTypeDelete(ProductTypeCustom model, int ID)
        {

            EntitiesConnection EC_DB = new EntitiesConnection();
            ProductType temp_ProductType = EC_DB.ProductTypes.FirstOrDefault(u => u.Id == ID);
            if (temp_ProductType != null)
            {

                EC_DB.ProductTypes.Remove(temp_ProductType);
                EC_DB.SaveChanges();
            }
            return RedirectToAction("ProductTypesList");

        }



        //***************Shop 
        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult PendingShopsList()
        {
            EntitiesConnection EC_DB = new EntitiesConnection();

            var query = from c in EC_DB.Shops where c.ShopRegisterStatus == false select c;

            return View(query.ToList());
        }

        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult PendingShopActivate(int ID)
        {
            ViewData["StateName"] = AddressManagement.LoadState();

            ViewData["ShopType"] = ShopManagement.LoadShopType();


            EntitiesConnection EC_DB = new EntitiesConnection();
            Shop temp_shop = EC_DB.Shops.FirstOrDefault(u => u.Id == ID);
            ShopCustomPending model = new ShopCustomPending();
            ViewData["CityName"] = AddressManagement.GetCity(temp_shop.ShopState);
            model.Id = temp_shop.Id;
            //  model.P_Category = temp_product.P_Category;
            model.FirstName = temp_shop.FirstName;
            model.LastName = temp_shop.LastName;
            model.Mobile = temp_shop.Mobile;
            model.ShopAddress = temp_shop.ShopAddress;
            model.ShopCity = temp_shop.ShopCity;
            model.ShopDescription = temp_shop.ShopDescription;
            model.ShopName = temp_shop.ShopName;
            model.ShopRegisterStatus = temp_shop.ShopRegisterStatus;
            model.ShopState = temp_shop.ShopState;
            model.ShopTell = temp_shop.ShopTell;
            model.AcceptBanner = temp_shop.AcceptBanner;
            model.AcceptCode = temp_shop.AcceptCode;
            model.AcceptSite = temp_shop.AcceptSite;
            model.DateJoined = temp_shop.DateJoined;
            model.F_ShopType_name = temp_shop.ShopType.Name;
            model.ShopQuote = temp_shop.PosterQuotes;
            return View(model);
        }
        [Authorize(Users = "09144390137,09141863449")]
        [HttpPost]
        public ActionResult PendingShopActivate(ShopCustomPending model, int ID)
        {

            ViewData["StateName"] = AddressManagement.LoadState();

            ViewData["ShopType"] = ShopManagement.LoadShopType();

            EntitiesConnection EC_DB = new EntitiesConnection();
            Shop temp_shop = EC_DB.Shops.FirstOrDefault(u => u.Id == ID);
            if (temp_shop != null)
            {
                if (ModelState.IsValid)
                {

                    temp_shop.ShopRegisterStatus = model.ShopRegisterStatus;
                    temp_shop.FirstName = model.FirstName;
                    temp_shop.LastName = model.LastName;
                    temp_shop.Mobile = model.Mobile;
                    temp_shop.PosterQuotes = model.ShopQuote;
                    temp_shop.ShopAddress = model.ShopAddress;
                    temp_shop.ShopCity = model.ShopCity;
                    temp_shop.ShopDescription = model.ShopDescription;
                    temp_shop.ShopName = model.ShopName;
                    temp_shop.ShopState = model.ShopState;
                    temp_shop.ShopTell = model.ShopTell;
                    temp_shop.AcceptBanner = model.AcceptBanner;
                    temp_shop.AcceptSite = model.AcceptSite;
                    temp_shop.InstagramAddress = model.IntagramAddress;
                    temp_shop.PersonalSiteAddress = model.PersonalSiteAddress;


                    ShopType temp_shoptype = EC_DB.ShopTypes.FirstOrDefault(u => u.Name == model.F_ShopType_name);
                    if (temp_shoptype != null)
                        temp_shop.F_ShopType_ID = temp_shoptype.Id;

                    if (temp_shop.AcceptBanner)
                    {
                        if (model.S_ImageOf != null)
                        {
                            if (model.S_ImageOf.ContentType == "image/png" || model.S_ImageOf.ContentType == "image/jpg" || model.S_ImageOf.ContentType == "image/jpeg" || model.S_ImageOf.ContentType == "image/gif" || model.S_ImageOf.ContentType == "image/bmp")
                            {



                                if (model.S_ImageOf.ContentLength > 0)
                                {
                                    ShopImage temp_shopimage = EC_DB.ShopImages.FirstOrDefault(u => u.F_Shop_ID == ID);

                                    byte[] image = new byte[model.S_ImageOf.ContentLength];
                                    model.S_ImageOf.InputStream.Read(image, 0, image.Length);
                                    //ShopImage temp_shopimage = new ShopImage();
                                    temp_shopimage.DateAccepted = DateTime.Now;
                                    temp_shopimage.DateUploaded = DateTime.Now;
                                    //temp_shopimage.F_Shop_ID = temp_shop.Id;
                                    temp_shopimage.ImageStatus = true;
                                    temp_shopimage.ShopImage1 = image;

                                    //             Byte[] result
                                    //= (Byte[])new ImageConverter().ConvertTo(ProductManagement.CreateThumbnail(image, 100, 100, true), typeof(Byte[]));
                                    //             temp_shopimage.ShopThumbnail = result;
                                    //EC_DB.ShopImages.Add(temp_shopimage);
                                }
                                else
                                {
                                    ModelState.AddModelError("", "لطف انتخاب نمایید.");
                                    return View(model);
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "لطفاً قرارداد را نیز انتخاب نمایید.");
                                return View(model);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "لطفاً بنر مرکز طرف قرارداد را نیز انتخاب نمایید.");
                            return View(model);
                        }
                    }
                    //ProductHistory ph = new ProductHistory();
                    //ph.OperationDate = DateTime.Now;
                    ////  ph.OperationHistory = "Edit," + temp_product.P_Category + ">" + model.P_Category + "," + temp_product.P_ID;
                    //ph.F_P_ID = temp_product.P_ID;
                    //ph.F_UserID = WebSecurity.CurrentUserId;
                    //EC_DB.ProductHistories.Add(ph);


                    EC_DB.SaveChanges();
                    return RedirectToAction("PendingShopsList");
                }
            }
            return View(model);
        }



        //***************ShopAdvertise

        [Authorize(Users = "graphist")]
        public ActionResult PendingShopsAdvertiseList()
        {
            EntitiesConnection EC_DB = new EntitiesConnection();


            return View(EC_DB.ShopAdvertisments.Where(u => u.ShopRequestStatus == false).Where(u => u.ShopRequestDate.Value.Month == DateTime.Now.Month).OrderBy(u => u.ShopRequestDate).ToList());
        }


        [Authorize(Users = "graphist")]
        public ActionResult PendingShopsAdvertiseActivate(int ID)
        {


            EntitiesConnection EC_DB = new EntitiesConnection();
            ShopAdvertisment temp_shopAd = EC_DB.ShopAdvertisments.FirstOrDefault(u => u.Id == ID);
            ShopAdvertiseCustomPending model = new ShopAdvertiseCustomPending();

            //  model.P_Category = temp_product.P_Category;
            model.DateRequestAccepted = temp_shopAd.ShopRequestAcceptedDate;
            model.DateRequested = temp_shopAd.ShopRequestDate;
            model.RequestStatus = temp_shopAd.ShopRequestStatus ?? default(bool);
            model.ShopAddress = temp_shopAd.ShopAddress;
            model.ShopDescription = temp_shopAd.ShopDescription;
            model.ShopName = temp_shopAd.ShopName;
            model.ShopTell = temp_shopAd.ShopTell;

            return View(model);
        }

        [Authorize(Users = "graphist")]
        [HttpPost]
        public ActionResult PendingShopsAdvertiseActivate(ShopAdvertiseCustomPending model, int ID)
        {

            EntitiesConnection EC_DB = new EntitiesConnection();
            ShopAdvertisment temp_shopAd = EC_DB.ShopAdvertisments.FirstOrDefault(u => u.Id == ID);
            if (temp_shopAd != null)
            {
                if (ModelState.IsValid)
                {

                    temp_shopAd.ShopAddress = model.ShopAddress;
                    temp_shopAd.ShopDescription = model.ShopDescription;
                    temp_shopAd.ShopName = model.ShopName;
                    temp_shopAd.ShopRequestAcceptedDate = DateTime.Now;
                    temp_shopAd.ShopRequestStatus = model.RequestStatus;
                    temp_shopAd.ShopTell = model.ShopTell;


                    EC_DB.SaveChanges();
                    return RedirectToAction("PendingShopsAdvertiseList");
                }
            }
            return View(model);
        }

        [Authorize(Users = "graphist")]
        public ActionResult AcceptedShopsAdvertiseList()
        {
            EntitiesConnection EC_DB = new EntitiesConnection();


            return View(EC_DB.ShopAdvertisments.Where(u => u.ShopRequestStatus == true).Where(u => u.ShopRequestDate.Value.Month == DateTime.Now.Month).OrderBy(u => u.ShopRequestDate).ToList());
        }



    }
}
