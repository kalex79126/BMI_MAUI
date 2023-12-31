﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Test3AlexKimMAUI.Utilities;
using Test3AlexKimMAUI.Models;

namespace Test3AlexKimMAUI.Data
{
    public class SportRepository : ISportRepository
    {
        readonly HttpClient client = new HttpClient();

        public SportRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Sport>> GetSports()
        {
            var response = await client.GetAsync("api/Sports");
            if (response.IsSuccessStatusCode)
            {
                List<Sport> sports = await response.Content.ReadAsAsync<List<Sport>>();
                return sports;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }

        }

        public async Task<Sport> GetSport(int ID)
        {
            var response = await client.GetAsync($"api/Sports/{ID}");
            if (response.IsSuccessStatusCode)
            {
                Sport sport = await response.Content.ReadAsAsync<Sport>();
                return sport;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

    }
}
