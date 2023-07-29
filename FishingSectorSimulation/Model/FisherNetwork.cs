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
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int population { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Subject<string> publisher { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void addNodes(int quantity)
        {
            throw new NotImplementedException();
        }

        public void growByFactor(double factor)
        {
            throw new NotImplementedException();
        }

        public void removeNodes(int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
