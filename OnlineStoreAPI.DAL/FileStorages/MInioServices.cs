using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.DataModel.Response;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Configurations;

namespace OnlineStoreAPI.DAL.FileStorages
{
    public class MInioServices : IFileStorage
    {
        private readonly MinioOptions _minioOptions;
        private readonly MinioClient? _client;
        private string BucketName { get; set; }

        public MInioServices(IOptions<MinioOptions> options)
        {
            _minioOptions = options.Value;
            BucketName = options.Value.BucketName;
            _client = (MinioClient?)new MinioClient()
                             .WithCredentials(options.Value.AccessKey, options.Value.SecretKey)
                             .WithEndpoint(options.Value.Endpoint)
                             .Build()
                             .WithSSL(options.Value.SSL);
        }

        public async Task<string> AddAsync(byte[] file, string fileName, string customPath = null)
        {
            PutObjectResponse result;
            using (var fileStream = new MemoryStream(file))
            {
                var poa = new PutObjectArgs()
                    .WithBucket(BucketName)
                    .WithObject(customPath + Guid.NewGuid().ToString() + GetExtension(fileName))
                    .WithStreamData(fileStream)
                    .WithObjectSize(fileStream.Length)
                    .WithContentType(GetContentType(file, fileName));
                result = await _client.PutObjectAsync(poa);
            }
            return result.ObjectName;
        }

        public async Task<string> AddAsync(string fileBase64, string fileName, string customPath = null)
        {
            PutObjectResponse result;
            var file = Convert.FromBase64String(fileBase64);
            using (var fileStream = new MemoryStream(file))
            {
                var poa = new PutObjectArgs()
                    .WithBucket(BucketName)
                    .WithObject(customPath + Guid.NewGuid().ToString() + GetExtension(fileName))
                    .WithStreamData(fileStream)
                    .WithObjectSize(fileStream.Length)
                    .WithContentType(GetContentType(file, fileName));
                result = await _client.PutObjectAsync(poa);
            }
            return result.ObjectName;
        }

        public async Task<string> GetUrlAsync(string key, int expiryTimeInMinutes = 24 * 60)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;

            var pgoa = new PresignedGetObjectArgs()
                .WithBucket(BucketName)
                .WithObject(key)
                .WithExpiry(expiryTimeInMinutes);
            //return (await _client.PresignedGetObjectAsync(pgoa)).Replace($"http://{_minioOptions.Endpoint}", _minioOptions.DocDomain);
            return await _client.PresignedGetObjectAsync(pgoa);
        }

        public async Task DeleteAsync(string key)
        {
            var roa = new RemoveObjectArgs().WithBucket(BucketName).WithObject(key);
            await _client.RemoveObjectAsync(roa);
        }

        private string GetExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        private string GetContentType(byte[] fileContent, string fileName)
        {
            string contentType = "";
            try
            {
                contentType = new FileExtensionContentTypeProvider().TryGetContentType(fileName, out string contentTypeResult)
                    ? contentTypeResult
                    : "application/octet-stream";
            }
            catch
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
