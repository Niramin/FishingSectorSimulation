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
        public int month => throw new NotImplementedException();

        public string monthName => throw new NotImplementedException();

        public Subject<string> networkPublisher => throw new NotImplementedException();

        public void goNextMonth()
        {
            throw new NotImplementedException();
        }

        public void goPreviousMonth()
        {
            throw new NotImplementedException();
        }
    }
}
