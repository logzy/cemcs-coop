namespace COOP.Banking.Data
{
    public class AppSettings
    {
        public class Flutterwave
        {
            public string PublicKey { get; set; }
            public string SecretKey { get; set; }
            public string EncryptionKey { get; set; }
        }
        public class SiteInfo
        {
            public string Url { get; set; }
        }
    }
}
