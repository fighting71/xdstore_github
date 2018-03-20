using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace XD.Store.Api.ImgServer.UploadInfo
{
    public class SmallUpload:SuperUpload
    {
        protected override string Run(HttpPostedFileBase imgFile, string saveUrl)
        {

            //通过文件获取Image图像
            Image image = Image.FromStream(imgFile.InputStream);

            //创建缩略图
            Bitmap bitmap = new Bitmap(100, 100);

            //获取缩略图画布
            Graphics graphics = Graphics.FromImage(bitmap);

            //将图片画到缩略图上
            graphics.DrawImage(image, new Rectangle(0, 0, 100, 100), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            //保存缩略图
            bitmap.Save(saveUrl,ImageFormat.Jpeg);
            return SUCCESS;

        }
    }
}