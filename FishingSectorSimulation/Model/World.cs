using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace FishingSectorSimulation.Model
{
    public class World
    {
        private FishingEconomy _fishingEconomy { get; set; }
        public Subject<string> worldPublisher { get; set; }

        public Subject<string> fishWatcher { get; set; }
        public World(FishingEconomy FishingEconomy)
        {
             worldPublisher = new Subject<string>();
            fishWatcher = new Subject<string>();
            _fishingEconomy = FishingEconomy;
            FishingEconomy.networkPublisher.Subscribe(worldPublisher);
            FishingEconomy.fishPopulationPublisher.Subscribe(fishWatcher);

        }

        public void InitializeWorld()
        {
            //populate with fish, adult in the beginning
            for (int i = 0; i < 1600000; i++)
            {
                IFish adultBluefinTuna = new BluefinTuna();
                adultBluefinTuna.age = 5;
                _fishingEconomy.addFish(adultBluefinTuna);
            }
        }

        public void UpdateMonth() {
            _fishingEconomy.goNextMonth();
        }
    }
}
