using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test3AlexKimMAUI.Models;

namespace Test3AlexKimMAUI.Data
{
    internal interface ISportRepository
    {
        Task<List<Sport>> GetSports();
        Task<Sport> GetSport(int ID);

    }
}
