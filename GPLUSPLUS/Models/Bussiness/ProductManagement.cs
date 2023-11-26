using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace GPLUSPLUS.Models.Bussiness
{
    public class ProductManagement
    {



        public static Image CreateThumbnail(byte[] byteArrayIn,
 int thumbWi, int thumbHi,
 bool maintainAspect)
        {
            // do nothing if the original is smaller than the designated
            // thumbnail dimensions
            MemoryStream ms = new MemoryStream(byteArrayIn);
            var source = Image.FromStream(ms);
            if (source.Width <= thumbWi && source.Height <= thumbHi) return Image.FromStream(ms);
            Bitmap thumbnail;
            try
            {
                int wi = thumbWi;
                int hi = thumbHi;
                if (maintainAspect)
                {
                    // maintain the aspect ratio despite the thumbnail size parameters
                    if (source.Width > source.Height)
                    {
                        wi = thumbWi;
                        hi = (int)(source.Height * ((decimal)thumbWi / source.Width));
                    }
                    else
                    {
                        hi = thumbHi;
                        wi = (int)(source.Width * ((decimal)thumbHi / source.Height));
                    }
                } thumbnail = new Bitmap(wi, hi);
                using (Graphics g = Graphics.FromImage(thumbnail))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.FillRectangle(Brushes.Transparent, 0, 0, wi, hi);
                    g.DrawImage(source, 0, 0, wi, hi);
                }

                //  thumbnail.Save(thumbnailName);

                return thumbnail;
            }
            catch
            {
                return null;
            }
        }


        public static List<SelectListItem> LoadProductType()
        {

            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "انتخاب کنید", Value = "انتخاب کنید" });
            using (EntitiesConnection EC_DB = new EntitiesConnection())
            {
                var query = from c in EC_DB.ProductTypes select c.Name;

                foreach (var q in query)
                {
                    li.Add(new SelectListItem { Text = q, Value = q });
                }
            }
            return li;
        }

        public List<ProductsJsonModel> AndroidPishnehadHa()
        {
            var Result = new List<ProductsJsonModel>();
            using (EntitiesConnection EC = new EntitiesConnection())
            {
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                    if (user != null)
                    {
                        var Temp1 = EC.ProductImages.Where(c => c.Product.P_ScoreCost > user.U_Score).OrderBy(t => t.Product.P_ScoreCost).Select(y => new { ProductImg = y.PI_Thumb, ProductName = y.Product.P_Name, ProductPrice = y.Product.P_ScoreCost, ProductID = y.Product.P_ID,ID=y.PI_ID }).Take(5).ToList();
                        var Temp2 = EC.ProductImages.Where(c => c.Product.P_ScoreCost < user.U_Score).OrderBy(t => t.Product.P_ScoreCost).Select(y => new { ProductImg = y.PI_Thumb, ProductName = y.Product.P_Name, ProductPrice = y.Product.P_ScoreCost, ProductID = y.Product.P_ID,ID=y.PI_ID }).Take(5).ToList();
                        Temp1.AddRange(Temp2);
                        foreach (var item in Temp1)
                        {
                            var p = new ProductsJsonModel();
                            p.ProductImg = "http://emtiaz.gtopmarketing.ir/Home/GetFile?FileName="+item.ID;
                            p.ProductID = "" + item.ProductID;
                            p.ProductPrice = item.ProductPrice.ToString();
                            p.ProductName = item.ProductName;
                            Result.Add(p);
                        }
                    }
                }
                return Result;
            }
        }
    }
}