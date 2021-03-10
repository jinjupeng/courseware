using Aliyun.OSS;
using ApiServer.BLL.IBLL;
using ApiServer.Model.Model.OSS;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;

namespace ApiServer.BLL.BLL
{
    public class OssService : IOssService
    {
        private readonly OssInfo _ossInfo;

        public OssService(IOptions<OssInfo> ossInfo)
        {
            _ossInfo = ossInfo.Value;
        }

        public int Delete(string url)
        {
            var client = new OssClient(_ossInfo.Endpoint, _ossInfo.AccessKeyId, _ossInfo.AccessKeySecret);
            var result = client.DeleteObject(_ossInfo.BucketName, url).DeleteMarker;
            return result ? 1 : 0;
        }

        public string Upload(IFormCollection multipartFile)
        {
            var file = multipartFile.Files.FirstOrDefault();
            var objectName = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
            objectName = "resource/" + objectName;
            return BasicUpload(objectName, file.OpenReadStream());
        }

        public string UploadByPath(IFormCollection multipartFile, string path)
        {
            var file = multipartFile.Files.FirstOrDefault();
            var objectName = path + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
            objectName = "resource/" + objectName;
            return BasicUpload(objectName, file.OpenReadStream());
        }

        private string BasicUpload(string objectName, Stream fileStream)
        {
            var client = new OssClient(_ossInfo.Endpoint, _ossInfo.AccessKeyId, _ossInfo.AccessKeySecret); ;
            // 上传文件
            client.PutObject(_ossInfo.BucketName, objectName, fileStream);
            return "/" + objectName;
        }
    }
}
