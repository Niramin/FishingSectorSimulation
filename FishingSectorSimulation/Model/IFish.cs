using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingSectorSimulation.Model
{
    public interface IFish
    {
        string type { get; set; }

        int age { get; set; }

        int lifespan { get; set; }


    }
}
