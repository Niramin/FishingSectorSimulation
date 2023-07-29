using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace FishingSectorSimulation.Model
{
    public interface INetwork
    {
        string Name { get; set; }

        int population { get; set; }

        void growByFactor(double factor);

        void addNodes(int quantity);

        void removeNodes(int quantity);

        Subject<string> publisher { get; set; }
    }
}
