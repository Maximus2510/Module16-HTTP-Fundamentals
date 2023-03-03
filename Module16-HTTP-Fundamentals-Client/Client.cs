namespace Module16_HTTP_Fundamentals_HTTP_Client
{
    class Client
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();

            string url = "http://localhost:8888/MyName/";
            HttpResponseMessage response = client.GetAsync(url).Result;
            Console.WriteLine("Status code: " + (int)response.StatusCode);

            string responseString = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Response content: " + responseString);
            Console.WriteLine("\n");

            MakeRequest(client, "http://localhost:8888/Information/");
            MakeRequest(client, "http://localhost:8888/Success/").Wait();
            MakeRequest(client, "http://localhost:8888/Redirection/").Wait();
            MakeRequest(client, "http://localhost:8888/ClientError/").Wait();
            MakeRequest(client, "http://localhost:8888/ServerError/").Wait();

            Console.ReadLine();
        }

        private static async Task MakeRequest(HttpClient client, string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                Console.WriteLine($"Status code for {url}: {response}");
                Console.WriteLine("\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error making request to {url}: {ex.Message}");
            }

        }
    }
}