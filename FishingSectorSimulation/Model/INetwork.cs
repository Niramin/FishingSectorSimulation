using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FishingSectorSimulation.Model
{
    public interface INetwork
    {
        public int month { get; }

        public string monthName { get; }

        public void goNextMonth();

        public void goPreviousMonth();

        public Subject<string> networkPublisher { get; }


    }
}
