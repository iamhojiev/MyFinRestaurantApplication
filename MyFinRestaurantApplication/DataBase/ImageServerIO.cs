using ManagerApplication.Database;
using ManagerApplication.Helper;
using RestSharp;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace ManagerApplication.DataBase
{
    public static class ImageServerIO
    {
        public static string photosFolderUri = DataSQL.URL + @"/photos";
        private static readonly RestClient client = new RestClient(photosFolderUri);

        public static Image ResizeToSquare(Image originalImage, int size = 100)
        {
            int width = originalImage.Width;
            int height = originalImage.Height;
            int cropSize = Math.Min(width, height);

            // Рассчитываем центральную часть для обрезки   
            int x = (width - cropSize) / 2;
            int y = (height - cropSize) / 2;

            // Создаем квадратное изображение
            Rectangle cropArea = new Rectangle(x, y, cropSize, cropSize);
            Bitmap squareImage = new Bitmap(size, size);

            using (Graphics g = Graphics.FromImage(squareImage))
            {
                g.DrawImage(originalImage, new Rectangle(0, 0, size, size), cropArea, GraphicsUnit.Pixel);
            }

            return squareImage;
        }

        public static void SaveJpegImageToServer(Image image, string imageName)
        {
            if (image == null || string.IsNullOrEmpty(imageName))
            {
                return;
                // throw new ArgumentNullException("Image or imageName cannot be null");
            }

            try
            {
                using (var ms = new MemoryStream())
                {
                    // Сохраняем изображение в формате JPEG в MemoryStream
                    // Изменяем размер изображения
                    using (var resizedImage = ResizeImage(image, 800, 600)) // Пример максимального размера
                    {
                        // Сохраняем измененное изображение в формате JPEG в MemoryStream
                        resizedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    byte[] imageBytes = ms.ToArray();
                    imageName = TransliterationHelper.CyrillicToLatin(imageName);
                    var req = new RestRequest("/upload_image.php")
                        .AddFile("image", imageBytes, imageName + ".jpg", "image/jpeg");

                    var res = client.Post(req);
                }
            }
            catch (Exception ex)
            {
                // Обрабатываем ошибку
                Dialog.Error("Ошибка при отправке фото на сервер: " + ex.Message.ToString());
            }
        }

        public static Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }


        public static void SavePngImageToServer(Image image, string imageName, bool compress = false, int maxWidth = 800, int maxHeight = 600)
        {
            if (image == null || string.IsNullOrEmpty(imageName))
            {
                return;
                // throw new ArgumentNullException("Image or imageName cannot be null");
            }

            try
            {
                using (var ms = new MemoryStream())
                {
                    // Если требуется сжатие, изменяем размер изображения
                    if (compress)
                    {
                        using (var resizedImage = ResizeImage(image, maxWidth, maxHeight))
                        {
                            resizedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                    else
                    {
                        // Сохраняем изображение без сжатия
                        using (var bitmap = new Bitmap(image))
                        {
                            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }

                    byte[] imageBytes = ms.ToArray();
                    imageName = TransliterationHelper.CyrillicToLatin(imageName);
                    var req = new RestRequest("/upload_image.php")
                        .AddFile("image", imageBytes, imageName + ".png", "image/png");

                    var res = client.Post(req);
                }
            }
            catch (Exception ex)
            {
                // Обрабатываем ошибку
                Dialog.Error("Ошибка при отправке фото на сервер: " + ex.Message.ToString());
            }
        }


        public static Image GetProductImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                return null;
                //throw new ArgumentNullException("imageName cannot be null");
            }

            try
            {
                // URL сервера
                imageName = TransliterationHelper.CyrillicToLatin(imageName);
                string fullImagePath = photosFolderUri + "/images/" + imageName;
                var client = new RestClient(fullImagePath);
                var request = new RestRequest();

                // Выполняем запрос и получаем ответ
                byte[] imageBytes = client.DownloadData(request);

                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image img = Image.FromStream(ms);
                    return img;
                }
            }
            catch (Exception ex)
            {
                // Обрабатываем ошибку
                // Dialog.Error(ex.Message.ToString());
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
    }
}
