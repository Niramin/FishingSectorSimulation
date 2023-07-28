using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FishingSectorSimulation.Model
{
    public class FishingEconomy:INetwork
    {
        private static FishingEconomy _instance = null;
        private static readonly object _instanceLock = new object();
        private string _name="";
        private string _monthName = "";
        private Subject<string> _publisher = new Subject<string>();
        private Subject<string> _fishPopulationPublisher = new Subject<string>();
        private Dictionary<string,IFish> _fish = new Dictionary<string, IFish>();
        private int _month = 0;
        private int _fishCount = 0;


        public static FishingEconomy Instance
        {
            get {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                        {
                                _instance = new FishingEconomy();
                        }
                    }
                }
                return _instance;
            }
        
        }

        public int month => _month;

        public string monthName => _monthName;

        public Subject<string> networkPublisher { get => _publisher;}

        public Subject<string> fishPopulationPublisher { get=>_fishPopulationPublisher; }

        public void goNextMonth()
        {
            
            reportMessage("Going to next month!");
            fishNextMonth();
            _month++;
            reportFishPopulation();
        }

        public void goPreviousMonth()
        {
            reportMessage("Going to previous month!");
            _month--;
        }

        private void reportMessage(string message)
        {
            _publisher.OnNext(message);
        }

        private void addFish(IFish fish)
        {
            _fish.Add(fish.fishid,fish);
            _fishCount++;
            
        }

        private void removeFish(IFish fish)
        {
            _fish.Remove(fish.fishid);
            _fishCount--;

        }

        private void reportFishPopulation()
        {
            _fishPopulationPublisher.OnNext($"{_month},{_fishCount}");
        }

        private void fishNextMonth(bool breedingSeason = false)
        {
            //first kill all old fish, and young fish based on probability
            foreach (KeyValuePair<string, IFish> T in _fish)
            { 
                IFish cur_fish = T.Value;

                if (cur_fish.age > cur_fish.lifespan)
                {
                    removeFish(cur_fish);
                    //delete object cur_fish in cpp?
                   
                }

                //for fish that have just reached adulthood

                if (cur_fish.age == cur_fish.adult_age)
                {
                    Random random = new Random();

                    if (!(random.NextDouble() < cur_fish.probability_of_surviving_till_adult))
                    {
                        removeFish(cur_fish);
                        //delete object cur_fish in cpp?
                       
                    }
                    
                }

                //if female adult, (at this point it is not old and is an adult), spawn eggs if breeding season

                if (cur_fish.female == true && breedingSeason && cur_fish.age>=cur_fish.adult_age && cur_fish.age< cur_fish.lifespan)
                {
                    Random random = new Random();
                    //every day of month
                    for (int i = 0; i < 31; i++)
                    {
                        //spawn eggs
                        int j = random.Next(cur_fish.spawnMin, cur_fish.spawnMax);
                        for (int k = 0; k < j; k++)
                        {
                            IFish fish = new BluefinTuna();
                            addFish(fish);
                            
                        }
                    }
                }
            }

           
            

        }
    }
}
