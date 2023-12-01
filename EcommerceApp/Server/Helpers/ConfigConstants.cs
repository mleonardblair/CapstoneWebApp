namespace EcommerceApp.Server.Helpers
{
    /// <summary>
    /// This class is a handy class to store the names of the keys in the secrets.json file. That way
    /// even if something were to happen and the strings were changed, we would quickly have the compiler report an error, rather than have to encounter the error at runtime. This prevents a bad commit from breaking the app.
    /// </summary>
    public static class ConfigConstants
    {
        // Stripe Test - Publishable Key and Secret Key
        public const string StripeTest_PublishableKey = "StripeTest:PublishableKey";
        public const string StripeTest_SecretKey = "StripeTest:SecretKey";

        // Stripe Live - Publishable Key and Secret Key
        public const string StripeLive_PublishableKey = "StripeLive:PublishableKey";
        public const string StripeLive_SecretKey = "StripeLive:SecretKey";

        // Stripe Webhook Secrets
        public const string StripeTest_WebhookSecret = "StripeWebHooks:TestWebhookSecret";
        public const string StripeLive_WebhookSecret = "StripeWebHooks:LiveWebhookSecret";
    }
}
