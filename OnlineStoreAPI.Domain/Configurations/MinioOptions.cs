namespace OnlineStoreAPI.Domain.Configurations
{
    public class MinioOptions
    {
        public string Endpoint { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public bool SSL { get; set; }
        public string BucketName { get; set; }
        public string DocDomain { get; set; }
        public string[] Extensions { get; set; }
    }
}
