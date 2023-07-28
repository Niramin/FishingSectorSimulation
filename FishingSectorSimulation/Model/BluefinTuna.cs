using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingSectorSimulation.Model
{
    public class BluefinTuna : IFish
    {
        public string type { get ; set ; }
        public int age { get; set; } = 1;
        public int lifespan { get; set; } = 60;
        public int adult_age { get; set; } = 5;
        public double probability_of_surviving_till_adult { get; set; } =  0.00000006666;
        public bool female { get; set; } = false;
        public int spawnMin { get; set; } = 780000;
        public int spawnMax { get; set; } = 35000000;
        public string fishid { get; set ; }

        public BluefinTuna() {
            type = "bluefintuna";
            fishid = Guid.NewGuid().ToString(); 
            Random random = new Random();
            if (random.NextDouble() <= 0.5)
            {
                female = true;
            }



        }
    }
}
