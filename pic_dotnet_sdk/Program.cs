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
        const int APP_ID = 111;
        const string SECRET_ID = "SECRET_ID";
        const string SECRET_KEY = "SECRET_KEY";
        static void Main(string[] args)
        {
            try
            {
                var result = "";
                var bucketName = "bucketName";
                var pic = new PicCloud(APP_ID, SECRET_ID, SECRET_KEY);

                //PornDetect
                string pornUrl1 = "http://b.hiphotos.baidu.com/image/pic/item/8ad4b31c8701a18b1efd50a89a2f07082938fec7.jpg";
                result = pic.Detection(bucketName, pornUrl1);
                Console.WriteLine(result);

                //PornDetectUrls
                string[] pornUrl = {"http://b.hiphotos.baidu.com/image/pic/item/8ad4b31c8701a18b1efd50a89a2f07082938fec7.jpg",
                               "http://c.hiphotos.baidu.com/image/h%3D200/sign=7b991b465eee3d6d3dc680cb73176d41/96dda144ad3459829813ed730bf431adcaef84b1.jpg"};
                result = pic.DetectionUrl(bucketName, pornUrl);
                Console.WriteLine(result);

                //PornDetectFiles
                string[] pornFile = {@"D:\porn\test1.jpg",
                                     @"D:\porn\test2.jpg",
                                     @"..\..\..\..\..\..\..\..\porn\test3.png"};
                result = pic.DetectionFile(bucketName, pornFile);
                Console.WriteLine(result);

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
                    result = pic.Query(bucketName, fileId);
                    result = pic.Copy(bucketName, fileId);
                    result = pic.Delete(bucketName, fileId);
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
