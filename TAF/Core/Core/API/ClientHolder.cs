using RestSharp;

namespace Core.Core.API
{
    public class ClientHolder
    {
        private static RestClient _instance;

        public static RestClient Client
        {
            get
            {
                if (_instance is null)
                {
                    throw new NullReferenceException($"The Client instance was not initialized. " +
                        $"You should first call the method {nameof(GenerateClient)}.");
                }

                return _instance;
            }
        }

        public static void GenerateClient(string url)
        {
            _instance = new RestClient(url);
        }
    }
}
