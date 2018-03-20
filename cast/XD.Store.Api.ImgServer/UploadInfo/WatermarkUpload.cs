using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace XD.Store.Api.ImgServer.UploadInfo
{
    public class WatermarkUpload : SuperUpload
    {

        /// <summary>
        /// 水印信息
        /// </summary>
        public string WatermarkStr { get; set; }

        protected override string Run(HttpPostedFileBase imgFile, string saveUrl)
        {
            //通过文件的输入流获取图片
            Image image = Image.FromStream(imgFile.InputStream);

            //获取画布
            Graphics graphics = Graphics.FromImage(image);

            //设置水印
            string makeStr = string.IsNullOrWhiteSpace(WatermarkStr) ? ("xdStore" + DateTime.Now) : WatermarkStr;

            graphics.DrawString(makeStr, new Font("宋体", 16), new SolidBrush(Color.Aquamarine), new PointF(
                (image.Width - (makeStr.Length * 16)), image.Height - 24
                ));

            image.Save(saveUrl, ImageFormat.Jpeg);
            return SUCCESS;
        }
    }
}