using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GlamourManager
{
    public class ApiClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        async private Task<List<FfxivItem>> callItemAPI(int page)
        {
            string apiUrl = $"https://cafemaker.wakingsands.com/item?limit=3000&page={page}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            ApiResponseObject apiResponseObject = await response.Content.ReadFromJsonAsync<ApiResponseObject>();

            return apiResponseObject.Results;
        }

        async public Task<List<FfxivItem>> getAllItems()
        {
            int pageNumber = 1;
            List<FfxivItem> allFfxivItems = new List<FfxivItem> { };

            while (true) 
            {
                try
                {
                    Console.WriteLine($"Page: {pageNumber}");
                    List<FfxivItem> newListItems = await callItemAPI(pageNumber);
                    allFfxivItems.AddRange(newListItems);
                    pageNumber++;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return allFfxivItems;
                }
            
            }
        }
    }
}