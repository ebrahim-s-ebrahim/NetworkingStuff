using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            Task dlTask = DownloadWebPage();

            Console.WriteLine("Holding for at least 5 seconds...");
            Thread.Sleep(TimeSpan.FromSeconds(5));

            dlTask.Wait();

        }

        public static string urlToDownload = "https://google.com/";
        public static string filename = "C:\\Users\\Ebrahim\\OneDrive\\DESKTOP\\index.html";

        public static async Task DownloadWebPage()
        {
            Console.WriteLine("Starting download...");

            // Setup the HttpClient
            using (HttpClient httpClient = new HttpClient())
            {
                // Get the webpage asynchronously
                HttpResponseMessage resp = await httpClient.GetAsync(urlToDownload);

                // If we get a 200 response, then save it
                if (resp.IsSuccessStatusCode)
                {
                    Console.WriteLine("Got it...");

                    // Get the data
                    byte[] data = await resp.Content.ReadAsByteArrayAsync();

                    // Save it to a file
                    FileStream fStream = File.Create(filename);
                    await fStream.WriteAsync(data, 0, data.Length);
                    fStream.Close();

                    Console.WriteLine("Done!");
                }
            }
        }

    }
}