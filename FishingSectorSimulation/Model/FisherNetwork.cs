using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace FishingSectorSimulation.Model
{
    public class FisherNetwork : INetwork
    {
        public string? Name { get ; set ; }
        public int population { get ; set ; }
        public Subject<string> publisher { get ; set; }

        public int income { get; set; }
        public FisherNetwork()
        {
            this.publisher = new Subject<string>();
        }
        public void addNodes(int quantity)
        {
            throw new NotImplementedException();
        }

        public void growByFactor(double factor)
        {
            double ans = population * factor;
            population = (int)ans;
        }

        public void removeNodes(int quantity)
        {
            throw new NotImplementedException();
        }

        public void reportIncome()
        {
            double ans = income /population;
            int ax = (int)ans;
            publisher.OnNext(ax.ToString());
        }
    }
}
