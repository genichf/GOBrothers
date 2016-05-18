using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace GOBrothers.Areas.Admin.Utilites
{
    public static class ImageUtilites
    {
        // Максимальные допустимые размеры картинки
        //const int maxWidth = 400;
        //const int maxHeight = 400;

        public static void SaveImageWithServerResize(HttpPostedFileBase hpf, string filePath, int maxWidth, int maxHeight)
        {
            if (hpf != null && hpf.ContentLength != 0 && hpf.ContentLength <= 5000000)
            {
                using (System.Drawing.Bitmap originalPic =
                            new System.Drawing.Bitmap(hpf.InputStream, false))
                {
                    // Вычисление новых размеров картинки
                    int width = originalPic.Width; //текущая ширина
                    int height = originalPic.Height; //текущая высота
                    int widthDiff = (width - maxWidth); //разница с допуст. шириной
                    int heightDiff = (height - maxHeight); //разница с допуст. высотой

                    // Определение размеров, которые необходимо изменять
                    bool doWidthResize = (maxWidth > 0 && width > maxWidth &&
                                        widthDiff > -1 && widthDiff > heightDiff);
                    bool doHeightResize = (maxHeight > 0 && height > maxHeight &&
                                        heightDiff > -1 && heightDiff > widthDiff);

                    // Ресайз картинки
                    if (doWidthResize || doHeightResize || (width.Equals(height)
                                    && widthDiff.Equals(heightDiff)))
                    {
                        int iStart;
                        Decimal divider;
                        if (doWidthResize) /*Я добавив: && widthDiff > heightDiff*/
                        {
                            iStart = width;
                            divider = Math.Abs((Decimal)iStart / maxWidth);
                            width = maxWidth;
                            height = (int)Math.Round((height / divider));
                        }
                        else
                        {
                            iStart = height;
                            divider = Math.Abs((Decimal)iStart / maxHeight);
                            height = maxHeight;
                            width = (int)Math.Round((width / divider));
                        }
                    }

                    // Сохраняем файл в папку пользователя
                    using (System.Drawing.Bitmap newBMP =
                            new System.Drawing.Bitmap(originalPic, width, height))
                    {
                        using (System.Drawing.Graphics oGraphics =
                                    System.Drawing.Graphics.FromImage(newBMP))
                        {
                            oGraphics.SmoothingMode =
                                    System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            oGraphics.InterpolationMode =
                 System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            oGraphics.DrawImage(originalPic, 0, 0, width, height);

                            /*int idx = hpf.FileName.LastIndexOf('.');
                            string ext =
                             hpf.FileName.Substring(idx, hpf.FileName.Length - idx);*/

                            // Формируем имя для картинки
                            /*Random rnd = new Random();
                            int imageName = rnd.Next();*/

                            //string filePath =
                                /*HttpContext.Current.Server.MapPath(
                                ConfigurationManager.AppSettings["PostImagesPath"] +
                                    imageName + ext);*/
                            //Path.Combine(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PostImagesPath"]), Guid.NewGuid() + Path.GetExtension(hpf.FileName));
                            if (System.IO.File.Exists(filePath))
                                System.IO.File.Delete(filePath);
                            newBMP.Save(filePath);
                        }
                    }
                }

            }
        }

    }
}