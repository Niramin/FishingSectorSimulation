using FishingSectorSimulation.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Input;
using System.Windows.Navigation;

namespace FishingSectorSimulation.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private World _world;
        public event PropertyChangedEventHandler? PropertyChanged;
        private Subject<string> _fishWatcher;
        private Queue<string> _fishHistoryCache;
        private bool isWorldBusy = false;



        //Dataflow blocks
        private ActionBlock<int> _UpdateWorldQueue;

        //commands
        public ICommand UpdateWorld { get; set; }

       

        //charts
        public string[] fishMonths { get; set; }
        public SeriesCollection fishPopulationCollection { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public MainViewModel() 
        {
            FishingEconomy fishingEconomy = FishingEconomy.Instance;
            World world = new World(fishingEconomy);

            _world = world;
            _UpdateWorldQueue = new ActionBlock<int>((val) =>_world.UpdateMonth());
            _world.InitializeWorld();
            fishPopulationCollection = new SeriesCollection() {
                new LineSeries{
                    Title = "Bluefin Tuna Population",
                    Values = new ChartValues<int>()
                }
            };
            _fishWatcher = new Subject<string>();
            world.fishWatcher.Subscribe(_fishWatcher);
            _fishWatcher.Subscribe(UpdateFishSeries);
            _fishHistoryCache = new Queue<string>();
            fishMonths = new string[]{ };
            YFormatter = value => value.ToString("C");
            UpdateWorld = new RelayCommand(UpdateWorldMonthClick, (parameter) => { return true; });


            
        }

        private void UpdateFishSeries(string fishMessage)
        {
            string[] month_fishcount = fishMessage.Split(':');
            string[] months = month_fishcount[0].Split(',');
            int fishCount = Int32.Parse(month_fishcount[1]);
            _fishHistoryCache.Enqueue(fishMessage);

            //update charts
            fishPopulationCollection[0].Values.Add(fishCount);
            fishMonths = months;
        }


        private void UpdateWorldMonthClick(object paramater)
        {
            _UpdateWorldQueue.Post(0);
        }

        public void Changed(string Name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }


    }
}
