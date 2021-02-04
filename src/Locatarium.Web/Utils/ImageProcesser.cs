using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Locatarium.Web.Utils
{
    public static class ImageProcesser
    {
        private static string fileTargetUrl;
        private const int size = 540;
        private const long quality = 75;

        public static async Task UploadeAndResize(IHostingEnvironment iHostingEnvironment, IFormFile image)
        {
            if (!(image == null || image.Length == 0))
            {
                if (image.ContentType.IndexOf("image", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    throw new Exception("File type is not image");
                }

                var imageGuid = Guid.NewGuid() + ".jpg";
                var pathTarget = iHostingEnvironment.WebRootPath + "\\images\\residences\\" + imageGuid;
                var pathTargetResized = iHostingEnvironment.WebRootPath + "\\images\\residences\\" + "resized_" + imageGuid;

                using (var stream = new FileStream(pathTarget, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                Resize(pathTarget, pathTargetResized);
                fileTargetUrl = "/images/residences/resized_" + imageGuid;
                File.Delete(pathTarget);
            }
        }

        private static void Resize(string inputImage, string outputImage)
        {
            Bitmap sourceBitmap = new Bitmap(inputImage);
            int newWidth = new int();
            int newHeight = new int();
            Bitmap newDrawArea = new Bitmap(540, 295);

            if (sourceBitmap.Width > sourceBitmap.Height)
            {
                newWidth = GetWidth(sourceBitmap.Width, sourceBitmap.Height);
                newHeight = 295;
            }
            else
            {
                newWidth = 540;
                newHeight = GetHeight(sourceBitmap.Width, sourceBitmap.Height);
            }


            DrawNewImage(sourceBitmap, newDrawArea, outputImage, newWidth, newHeight);

            sourceBitmap.Dispose();
        }

        private static int GetWidth(int imageWidth, int imageHeight)
        {
            int width;

            if (imageWidth > imageHeight)
            {
                width = size;
            }
            else
            {
                width = Convert.ToInt32(imageWidth * size / (double)imageHeight);
            }

            return width;
        }

        private static int GetHeight(int imageWidth, int imageHeight)
        {
            int height;

            if (imageHeight > imageWidth)
            {
                height = size;
            }
            else
            {
                height = Convert.ToInt32(imageHeight * size / (double)imageWidth);
            }

            return height;
        }

        private static void DrawNewImage(Bitmap imageOriginal, Bitmap imageNewArea, string outputImage, int width, int height)
        {
            using (var graphicOfDrawArea = Graphics.FromImage(imageNewArea))
            {
                graphicOfDrawArea.CompositingQuality = CompositingQuality.HighSpeed;
                graphicOfDrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicOfDrawArea.CompositingMode = CompositingMode.SourceCopy;
                graphicOfDrawArea.DrawImage(imageOriginal, 0, 0, width, height);

                using (var output = File.Open(outputImage, FileMode.Create))
                {
                    var qualityParamId = Encoder.Quality;
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    imageNewArea.Save(output, codec, encoderParameters);

                    output.Close();
                }

                graphicOfDrawArea.Dispose();
            }
        }


        public static string ReturnFileTarget()
        {
            return fileTargetUrl;
        }
    }
}
