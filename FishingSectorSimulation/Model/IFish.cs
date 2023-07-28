using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingSectorSimulation.Model
{
    public interface IFish
    {
        string fishid { get; set; }
        string type { get; set; }

        //in months
        int age { get; set; }
        //in months
        int lifespan { get; set; }

        int adult_age { get; set; }

        double probability_of_surviving_till_adult { get; set; }

        bool female { get; set; }

        int spawnMin { get; set; }
        int spawnMax { get; set; }  


    }
}
