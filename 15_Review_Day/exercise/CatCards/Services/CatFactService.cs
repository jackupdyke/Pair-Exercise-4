using CatCards.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;

namespace CatCards.Services
{
    public class CatFactService : ICatFactService
    {
        private readonly string API_URL = "https://cat-data.netlify.app/api/facts/random";
        RestClient client = new RestClient();
        
        public CatFact GetFact()
        {
            CatFact fact = new CatFact();

            RestRequest request = new RestRequest(API_URL);
            IRestResponse<CatFact> response = client.Get<CatFact>(request);
            if(response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");

            }
            else if(!response.IsSuccessful)
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
