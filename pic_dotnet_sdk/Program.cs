using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QCloud.PicApi.Api;
using QCloud.PicApi.Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace QCloud.PicApi
{
    class Program
    {
        const int APP_ID = 10000000;
        const string SECRET_ID = "SECRET_ID";
        const string SECRET_KEY = "SECRET_KEY";
        static void Main(string[] args)
        {
            try
            {
                var result = "";
                var bucketName = "porndetect";
                var pic = new PicCloud(APP_ID, SECRET_ID, SECRET_KEY);
                var start = DateTime.Now.ToUnixTime();
                result = pic.Upload(bucketName, @"d:\Tulips.jpg");
                var end = DateTime.Now.ToUnixTime();
                Console.WriteLine(result);
                var obj = (JObject)JsonConvert.DeserializeObject(result);
                var code = (int)obj["code"];
                if (code == 0)
                {
                    var data = obj["data"];
                    var fileId = data["fileid"].ToString();
                    var downloadUrl = data["download_url"].ToString();
                    //result = pic.Query(bucketName, fileId);
                    //result = pic.Copy(bucketName, fileId);
                    //result = pic.Delete(bucketName, fileId);
                    //result = pic.Detection(bucketName, downloadUrl);
                    Console.WriteLine(result);
                }
                Console.WriteLine("总用时：" + (end - start) + "毫秒");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
