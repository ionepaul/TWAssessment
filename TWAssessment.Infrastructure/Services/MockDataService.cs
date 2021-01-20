using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TWAssessment.Application.Common.Interfaces;
using TWAssessment.Domain.Entities;

namespace TWAssessment.Infrastructure.Services
{
    public class MockDataService : IMockDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public MockDataService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_configuration.GetValue<string>("MockDataBaseURL"))
            };
        }

        public async Task<IEnumerable<User>> GetData()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_configuration.GetValue<string>("MockDataURLPath"));

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<User>>();
                }
                else
                {
                    //Log exception
                    return null;
                }
            }
            catch (Exception)
            {
                //Log exception 
                return null;
            }
        }
    }
}
