using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Globalization;
using System.Collections;
using LitJson;

namespace LotterySln.Web.Handlers
{
    /// <summary>
    /// KindeditorFilesUpload 的摘要说明
    /// </summary>
    public class KindeditorFilesUpload : IHttpHandler
    {
        WebHelper.UploadFilesHelper ufh;

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            HttpPostedFile imgFile = context.Request.Files["imgFile"];
            if (imgFile == null || imgFile.ContentLength == 0)
            {
                showError(context, "请选择文件。");
            }
            string dirName = context.Request.QueryString["dir"];
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }

            try
            {
                if (ufh == null) ufh = new WebHelper.UploadFilesHelper();
                string fileUrl = ufh.Upload(imgFile, "HtmlEditorFiles/" + dirName + "");
                Hashtable hash = new Hashtable();
                hash["error"] = 0;
                hash["url"] = fileUrl.TrimStart('~');
                context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                context.Response.Write(JsonMapper.ToJson(hash));
                context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                showError(context, ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void showError(HttpContext context, string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
        }
    }
}