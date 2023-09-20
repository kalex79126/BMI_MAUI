using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Test3AlexKimMAUI.Models;
using Test3AlexKimMAUI.Utilities;


namespace Test3AlexKimMAUI.Data
{
    internal interface IAthleteRepository
    {
        Task<List<Athlete>> GetAthletes();
        Task<Athlete> GetAthlete(int ID);
        Task<List<Athlete>> GetAthletesBySport(int AthleteID);
    }
}
