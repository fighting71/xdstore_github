using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XD.Store.Api.ImgServer.Tool;

namespace XD.Store.Api.ImgServer.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// 普通的文件上传
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseDeal()
        {
            //1.get upload Img
            HttpPostedFileBase imgFile = Request.Files["imgFile"];

            if (imgFile == null)
            {
                return Content("error");
            }

            //1.valid file type
            var ext = Path.GetExtension(imgFile.FileName);

            if (!(ext == ".jpeg" || ext == ".jpg" || ext == ".png" || ext == ".gif"))
            {
                //不是图片的时候：
                return Content("monster");
            }


            //2.product fileName
            var fileName = "/Content/img/Upload/" + Guid.NewGuid().ToString() + imgFile.FileName;

            //3.save file
            imgFile.SaveAs(Request.MapPath(fileName));

            return Content(string.Format("<img src={0} /> ", fileName));

        }

        /// <summary>
        /// 带水印的图片上传
        /// </summary>
        /// <returns></returns>
        public ActionResult WatermarkImg()
        {
            //1.get upload Img
            HttpPostedFileBase imgFile = Request.Files["imgFile"];

            if (imgFile == null)
            {
                return Content("error");
            }

            //1.valid file type
            var ext = Path.GetExtension(imgFile.FileName);

            if (!(ext == ".jpeg" || ext == ".jpg" || ext == ".png" || ext == ".gif"))
            {
                //不是图片的时候：
                return Content("monster");
            }

            //通过文件的输入流获取图片
            Image image = Image.FromStream(imgFile.InputStream);

            //获取画布
            Graphics graphics = Graphics.FromImage(image);

            //设置水印
            string makeStr = "xdStore" + DateTime.Now;

            graphics.DrawString(makeStr, new Font("宋体", 16), new SolidBrush(Color.Aquamarine), new PointF(
                (image.Width - (makeStr.Length * 16)), image.Height - 24
                ));

            //生成文件标识
            string pgml = Guid.NewGuid().ToString() + imgFile.FileName;

            //保存原图片
            string fileName = String.Format("/Content/img/Upload/{0}", pgml);
            imgFile.SaveAs(Request.MapPath(fileName));

            //保存水印图片
            fileName = String.Format("/Content/img/Upload/WaterMake/{0}", pgml);
            image.Save(Request.MapPath(fileName), ImageFormat.Jpeg);

            return Content(string.Format("<img src={0} /> ", fileName));
        }

        /// <summary>
        /// 缩略图上传
        /// </summary>
        /// <returns></returns>
        public ActionResult SmallImg()
        {
            //1.get upload Img
            HttpPostedFileBase imgFile = Request.Files["imgFile"];

            if (imgFile == null)
            {
                return Content("error");
            }

            //1.valid file type
            var ext = Path.GetExtension(imgFile.FileName);

            if (!(ext == ".jpeg" || ext == ".jpg" || ext == ".png" || ext == ".gif"))
            {
                //不是图片的时候：
                return Content("monster");
            }

            //通过文件获取Image图像
            Image image = Image.FromStream(imgFile.InputStream);

            //创建缩略图
            Bitmap bitmap = new Bitmap(100, 100);

            //获取缩略图画布
            Graphics graphics = Graphics.FromImage(bitmap);

            //将图片画到缩略图上
            graphics.DrawImage(image, new Rectangle(0, 0, 100, 100), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            //生成文件标识
            string pgml = Guid.NewGuid().ToString() + imgFile.FileName;

            //保存原图片
            string fileName = String.Format("/Content/img/Upload/{0}", pgml);
            imgFile.SaveAs(Request.MapPath(fileName));

            //保存缩略图
            fileName = String.Format("/Content/img/Upload/Small/{0}", pgml);
            bitmap.Save(Request.MapPath(fileName));

            return Content(string.Format("<img src={0} /> ", fileName));

        }

        /// <summary>
        /// 公开的文件上传
        /// </summary>
        /// <param name="makeStr"></param>
        /// <returns></returns>
        public ActionResult Upload(string makeStr)
        {
            //1.get upload Img
            HttpPostedFileBase imgFile = Request.Files["imgFile"];

            if (imgFile == null)
            {
                return Content("error");
            }

            //2.valid file type
            var ext = Path.GetExtension(imgFile.FileName);

            if (!(ext == ".jpeg" || ext == ".jpg" || ext == ".png" || ext == ".gif"))
            {
                //不是图片的时候：
                return Content("monster");
            }

            //generate fileName
            var fileName = Guid.NewGuid().ToString().Replace("-", "");

            //3.save file
            imgFile.SaveAs(Request.MapPath(string.Format("{0}{1}{2}", ImgServerTools.RootUrl, fileName, imgFile.FileName)));

            //4open threadPool to genger other img

            //waterMake img
            Task.Run(() =>
            {
                //通过文件的输入流获取图片
                Image image = Image.FromStream(imgFile.InputStream);

                //获取画布
                Graphics graphics = Graphics.FromImage(image);

                //设置水印
                makeStr = string.IsNullOrWhiteSpace(makeStr)?("xdStore" + DateTime.Now):makeStr;

                graphics.DrawString(makeStr, new Font("宋体", 16), new SolidBrush(Color.Aquamarine), new PointF(
                    (image.Width - (makeStr.Length * 16)), image.Height - 24
                    ));
                    
                image.Save(Request.MapPath(string.Format("{0}{1}{2}", ImgServerTools.WaterMakeUrl, fileName, imgFile.FileName)), ImageFormat.Jpeg);
            });

            //small img
            Task.Run(() =>
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
                bitmap.Save(Request.MapPath(string.Format("{0}{1}{2}", ImgServerTools.SmallUrl, fileName, imgFile.FileName)), ImageFormat.Jpeg);
            });

            return Content(string.Format("{0}{1}", fileName, imgFile.FileName));
        }

    }

}