using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;

namespace GPLUSPLUS.Models.Bussiness
{
    public class AgentManagement
    {
        public byte[] AgentPoster(/*byte[] byteArrayIn*/ string shopname,string qoute)
        {
            //Load the Image to be written on.
            //MemoryStream ms = new MemoryStream(byteArrayIn);
            //var source = Image.FromStream(ms);
            //using (Bitmap bitMapImage = new Bitmap(source.Width, source.Height))
            //{
            //    using (Graphics g = Graphics.FromImage(bitMapImage))
            //    {
            //        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //        g.FillRectangle(Brushes.Transparent, 0, 0, source.Width, source.Height);
            //        g.DrawImage(source, 0, 0, source.Width, source.Height);
            //    }
       
            Bitmap bitMapImage = new
      System.Drawing.Bitmap(HttpContext.Current.Server.MapPath("~/Images/myposternew-compressor.jpg"));
            
                Graphics graphicImage = Graphics.FromImage(bitMapImage);
                
                    //Smooth graphics is nice.
                    graphicImage.SmoothingMode = SmoothingMode.AntiAlias;
                    //Write your text.
                    StringFormat stringFormat = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                 RectangleF header2Rect = new RectangleF();

    header2Rect.Location = new Point(50, 650);
    header2Rect.Size = new Size(bitMapImage.Width-100, 150);

                        SizeF s = graphicImage.MeasureString(shopname, new Font("Arial", 18, FontStyle.Bold));
                        float fontScale = Math.Max(s.Width / header2Rect.Width, s.Height / header2Rect.Height);
                        using (Font font = new Font("Arial", 18 / fontScale,FontStyle.Bold, GraphicsUnit.Point))
                        {
                            graphicImage.DrawString(shopname, font, Brushes.Black, header2Rect, stringFormat);
                        }
                 //   graphicImage.DrawString(shopname,  new Font("Arial", 18, FontStyle.Bold),SystemBrushes.WindowText, new Point(285, 675));
                    //I am drawing a oval around my text.
                
                    //Set the content type
                 //   HttpContext.Current.Response.ContentType = "image/jpeg";
                    //   Save the new image to the response output stream.


                        RectangleF Desc2Rect = new RectangleF();
                        Desc2Rect.Location = new Point(50, 865);
                        Desc2Rect.Size = new Size(bitMapImage.Width - 100, 100);

                        SizeF s_d = graphicImage.MeasureString(qoute, new Font("Arial", 14, FontStyle.Bold));
                        float fontScale_d = Math.Max(s_d.Width / Desc2Rect.Width, s_d.Height / Desc2Rect.Height);
                        using (Font font = new Font("Arial", 14 / fontScale_d, FontStyle.Bold, GraphicsUnit.Point))
                        {
                            graphicImage.DrawString(qoute, font, Brushes.Black, Desc2Rect, stringFormat);
                        }
           //         graphicImage.DrawString(qoute,
           //new Font("Arial", 14, FontStyle.Bold),
           //SystemBrushes.WindowText, new Point(265, 880));
                    //bitMapImage.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
                    //Clean house.
                    //graphicImage.Dispose();
                    //bitMapImage.Dispose();

                        Pen pen = new Pen(Color.Black, 5);

                        graphicImage.DrawRectangle(pen, 0, 0, 100, 200);
                    return (Byte[])new ImageConverter().ConvertTo(bitMapImage, typeof(Byte[])); ;
                
            
        }

        public string AgentTempPoster() { 
              using (Image image = Image.FromFile(HttpContext.Current.Server.MapPath("~/Images/myposter.jpg")))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        // Convert byte[] to Base64 String
                      string   base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                    }
                }
           
        }

        public byte[] DrawText(string shopname, string qoute)
        {

            //create a new image of the right size
          Image  img = new Bitmap(600 ,400);
            Graphics drawing = Graphics.FromImage(img);

            Random random = new Random(DateTime.Now.Second+DateTime.Now.Millisecond);
            int red=0, green=0, blue = 0;
            bool acceptColor = false;
            while (acceptColor == false) {
                acceptColor = false;
                red = random.Next(0, 255);
                if (red > 155)
                    acceptColor = true;

                green = random.Next(0, 255);
                if (green > 155)
                    acceptColor = true;

                blue = random.Next(0, 255);
                if (blue > 155)
                    acceptColor = true;
            }
            Color myRandom_Col = Color.FromArgb(red, green, blue);

            //paint the background
            drawing.Clear(myRandom_Col);

            //Smooth graphics is nice.
            drawing.SmoothingMode = SmoothingMode.AntiAlias;
            //Write your text.
            StringFormat stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags= StringFormatFlags.DirectionRightToLeft
            };

            RectangleF header2Rect = new RectangleF();

            header2Rect.Location = new Point(50, 50);
            header2Rect.Size = new Size(img.Width - 100, 150);

            SizeF s = drawing.MeasureString(shopname, new Font("Arial", 18, FontStyle.Bold));
            float fontScale = Math.Max(s.Width / header2Rect.Width, s.Height / header2Rect.Height);
            using (Font font = new Font("Arial", 18 / fontScale, FontStyle.Bold, GraphicsUnit.Point))
            {
                drawing.DrawString(shopname, font, Brushes.Black, header2Rect, stringFormat);
            }
            //   graphicImage.DrawString(shopname,  new Font("Arial", 18, FontStyle.Bold),SystemBrushes.WindowText, new Point(285, 675));
            //I am drawing a oval around my text.

            //Set the content type
            //   HttpContext.Current.Response.ContentType = "image/jpeg";
            //   Save the new image to the response output stream.

            RectangleF Desc2Rect = new RectangleF();
            Desc2Rect.Location = new Point(50, 250);
            Desc2Rect.Size = new Size(img.Width - 100, 100);

            SizeF s_d = drawing.MeasureString(qoute, new Font("Arial", 14, FontStyle.Bold));
            float fontScale_d = Math.Max(s_d.Width / Desc2Rect.Width, s_d.Height / Desc2Rect.Height);
            using (Font font = new Font("Arial", 14 / fontScale_d, FontStyle.Bold, GraphicsUnit.Point))
            {
                drawing.DrawString(qoute, font, Brushes.Black, Desc2Rect, stringFormat);
            }

            Pen pen = new Pen(Color.Black, 5);
            Rectangle rect = new Rectangle(30, 250, 540, 100);
            drawing.DrawRectangle(pen, rect);


            drawing.Save();

            drawing.Dispose();

            return (Byte[])new ImageConverter().ConvertTo(img, typeof(Byte[])) ;

        }
    }

}