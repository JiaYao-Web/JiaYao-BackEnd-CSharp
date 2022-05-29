using Aliyun.OSS;
using Aliyun.OSS.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace JiaYao.OSS
{
    public static class PicUploadBll
    {
        //需根据部署安装说明文档自行填写参数
        private static string endpoint = "oss-cn-shanghai.aliyuncs.com";
        private static string accessKeyId = "LTAI5tJQvjsE3EW6ckDrrJUa";
        private static string accessKeySecret = "GMM9B15OrR1Dnw4MaQ8rxDyXwZY4k7";
        private static string bucketName = "jiayao-net";
        private static string urlPrefix = "";


        static AutoResetEvent _event = new AutoResetEvent(false);

        // 创建OssClient实例。
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        private static void PutObjectCallback(IAsyncResult ar)
        {
            try
            {
                client.EndPutObject(ar);
                Console.WriteLine(ar.AsyncState as string);
                Console.WriteLine("Put object succeeded");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _event.Set();
            }
        }

        //多线程应用 异步上传
        public static PicUploadResult AsyncPutObject(Stream fs, string originalFileName)
        {
            PicUploadResult result = new PicUploadResult();
            string randomName = getFilePath(originalFileName);
            try
            {

                using (fs)
                {
                    string res = "Notice user: put object finish";
                    client.BeginPutObject(bucketName, randomName, fs, null, PutObjectCallback, res.ToCharArray());
                    result.status = true;
                    result.url = "https://" + urlPrefix + randomName;
                    // 异步上传。
                    _event.WaitOne();
                }
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
                result.status = false;
                result.url = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
                result.status = false;
                result.url = "";
            }
            return result;
        }

        public static PicUploadResult UploadPic(Stream fs, string originalFileName)
        {
            PicUploadResult result = new PicUploadResult();
            string randomName = getFilePath(originalFileName);

            // 创建OssClient实例。
            OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);
            try
            {
                // 上传文件。
                client.PutObject(bucketName, randomName, fs);
                result.status = true;
                result.url = "https://" + urlPrefix + randomName;
            }
            catch (Exception ex)
            {
                result.status = false;
                result.url = "";
            }
            return result;
        }

        private static string getFilePath(string sourceFileName)
        {
            DateTime dateTime = DateTime.Now;
            // 使用C++/CLI
            return dateTime.Year + "/"
                + dateTime.Month + "/"
                + dateTime.Day + "/"
                +JiaYaoCLI.IDGenerator.generate_id() + '.' + sourceFileName.Split(".")[1];
        }
    }
}
