using Aliyun.OSS;
using ApiServer.BLL.IBLL;
using ApiServer.Model.Model.OSS;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace ApiServer.BLL.BLL
{
    public class OssService : IUploadService
    {
        private readonly OssInfo _ossInfo;
        private readonly IOss _oss;

        public OssService(OssInfo ossInfo, IOss oss)
        {
            _ossInfo = ossInfo;
            _oss = oss;
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
            var client = new OssClient(_ossInfo.EndPoint, _ossInfo.AccessKeyId, _ossInfo.AccessKeySecret); ;
            // 上传我呢见
            var putObjectRequest = new PutObjectRequest(_ossInfo.BucketName, objectName, fileStream);
            client.PutObject(putObjectRequest);
            return "/resource/" + objectName;
        }
    }
}
