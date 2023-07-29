using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace FishingSectorSimulation.Model
{
    public class FishNetwork : INetwork
    {
        public string Name { get ; set ; }
        public int population { get ; set; }
        public Subject<string> publisher { get ; set ; }

        public FishNetwork()
        {
            publisher = new Subject<string>();
        }
        public void addNodes(int quantity)
        {
            throw new NotImplementedException();
        }

        public void growByFactor(double factor)
        {
            double ans = population * factor;
            population =  (int)ans;
        }

        public void removeNodes(int quantity)
        {
            throw new NotImplementedException();
        }

        public void reportPopulation() {
            publisher.OnNext(population.ToString());
        }
    }
}
