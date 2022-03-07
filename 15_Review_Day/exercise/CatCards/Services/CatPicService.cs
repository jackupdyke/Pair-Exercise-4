using CatCards.Models;
using RestSharp;
using System;

namespace CatCards.Services
{
    public class CatPicService : ICatPicService
    {
        private readonly string API_URL = "https://cat-data.netlify.app/api/pictures/random";
        RestClient client = new RestClient();
        public CatPic GetPic()
        {
            CatPic pic = new CatPic();

            RestRequest request = new RestRequest(API_URL);
            IRestResponse<CatPic> response = client.Get<CatPic>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");

            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }
        
    }
}
