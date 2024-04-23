using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace gnCommon
{
    public enum LogLevel
    {
        ERROR = 3,
        WARNING = 2,
        DEFAULT = 1,
        DEBUG = 0
    }

    public class Helper
    {
        private static string cachedPreBilderPfad = string.Empty;
        private static string cachedPfadZurAnwendung = string.Empty;
        private const string noImageName = "0_no_image_thumb.png";
        private static Image noImage = null;

        public static int GetBit(bool value)
        {
            if (value)
                return 1;
            return 0;
        }

        public static string GetPfadZurAnwendung()
        {
            if (string.IsNullOrWhiteSpace(cachedPfadZurAnwendung))
            {
                FileInfo fi = new FileInfo(Assembly.GetEntryAssembly().Location);
                cachedPfadZurAnwendung = fi.DirectoryName;
            }
            
            return cachedPfadZurAnwendung;
        }

        public static string GetPrePfadZurBildern()
        {
            if (string.IsNullOrWhiteSpace(cachedPreBilderPfad))
                cachedPreBilderPfad = Path.Combine(GetPfadZurAnwendung(), "pix");
            
            return cachedPreBilderPfad;
        }

        public static Image GetNoImage()
        {
            if(noImage == null)
                noImage = GetImage(noImageName);

            return noImage;
        }

        public static Image GetImage(string pfad)
        {
            if(!string.IsNullOrWhiteSpace(pfad))
            {
                var pfadKompett = Path.Combine(GetPrePfadZurBildern(), pfad);
                if (File.Exists(pfadKompett))
                {
                    try
                    {
                        return Image.FromFile(pfadKompett);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            
            return null;
        }

        /// <summary>
        /// Noch nicht in Benutzung!!!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="columName"></param>
        /// <param name="nullRepresentation"></param>
        /// <returns></returns>
        public static T GetIt<T>(DataRow row, string columName, T nullRepresentation)
        {
            if(row == null)
                throw new ApplicationException("Es wurde eine leere Row übergeben.");
            if(!row.Table.Columns.Contains(columName))
                throw new ApplicationException("Unbekannter Spaltenname.");

            try
            {
                var value = row[columName];
                if (value == null)
                    return (T) Convert.ChangeType(nullRepresentation, typeof(T));
                else
                    return (T) Convert.ChangeType(value, typeof(T));
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return (T) Convert.ChangeType(nullRepresentation, typeof(T));
            }
        }

        public static Image ShrinkForList(Image img)
        {
            if (img == null)
                return GetNoImage();

            if (img.Size.Height <= 50)
                return img;
            
            var faktor = 50D / (img.Size.Height * 1D);
            var w = (int)(img.Size.Width * faktor);
            var h = (int)(img.Size.Height * faktor);
            Size newSize = new Size(w, h);

            return resizeImage(img, newSize);
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

    }
}
