using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XD.Store.Api.ImgServer.UploadInfo
{
    public class BaseUpload:SuperUpload
    {
        protected override string Run(HttpPostedFileBase imgFile, string saveUrl)
        {
            imgFile.SaveAs(saveUrl);
            return SUCCESS;
        }

    }
}