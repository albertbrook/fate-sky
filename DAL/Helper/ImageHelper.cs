using System;
using System.IO;

namespace DAL
{
    public class ImageHelper
    {
        /// <summary>
        /// 将流转换成图片字节流
        /// </summary>
        public static byte[] ToByteStream(Stream stream)
        {
            byte[] image = new byte[stream.Length];
            stream.Read(image, 0, image.Length);
            return image;
        }

        /// <summary>
        /// 将字节流转换成可解析的图片文字
        /// </summary>
        public static string ToStringImage(byte[] image)
        {
            MemoryStream stream = new MemoryStream(image);
            string base64 = Convert.ToBase64String(stream.ToArray());
            return "data:image/jpg;base64," + base64;
        }
    }
}
