using System;
using System.Configuration;

namespace XD.Store.Api.ImgServer.Tool
{
    public class ImgServerTools
    {

        #region  xml analysis

        public static string AppSettings(string xmlkey)
        {
            return AppSettings(xmlkey, string.Empty);
        }

        public static string AppSettings(string xmlkey, string defaultVal)
        {
            if (ConfigurationManager.AppSettings[xmlkey] == null)
                return defaultVal;
            string str = ConfigurationManager.AppSettings[xmlkey].ToString();
            return string.IsNullOrEmpty(str) ? defaultVal : str;
        }


        #endregion

        /// <summary>
        /// 获取默认水印字符串
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultMakeStr()
        {
            return string.Format("xdStore{0}", DateTime.Now.ToString("yy-MM-dd hh:mm:ss"));
        }

        /// <summary>
        /// 根路径
        /// </summary>
        public readonly static string RootUrl = AppSettings(nameof(RootUrl));

        /// <summary>
        /// 缩略图路径
        /// </summary>
        public readonly static string SmallUrl = AppSettings(nameof(SmallUrl));

        /// <summary>
        /// 水印图路径
        /// </summary>
        public readonly static string WaterMakeUrl = AppSettings(nameof(WaterMakeUrl));

    }
}