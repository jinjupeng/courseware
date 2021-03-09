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
            throw new NotImplementedException();
        }

        public string Upload(IFormCollection multipartFile)
        {
            var file = multipartFile.Files.FirstOrDefault();
            var objectName = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
            return BasicUpload(objectName, file.OpenReadStream());
        }

        public string UploadByPath(IFormCollection multipartFile, string path)
        {
            throw new NotImplementedException();
        }

        private string BasicUpload(string objectName, Stream fileStream)
        {
            var client = new OssClient(_ossInfo.Endpoint, _ossInfo.AccessKeyId, _ossInfo.AccessKeySecret); ;
            // 上传文件
            var putObjectRequest = new PutObjectRequest(_ossInfo.BucketName, objectName, fileStream);
            client.PutObject(putObjectRequest);
            return "/resource/" + objectName;
        }
    }
}
