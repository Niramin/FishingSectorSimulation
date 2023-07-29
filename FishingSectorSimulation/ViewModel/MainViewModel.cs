using FishingSectorSimulation.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Input;

namespace FishingSectorSimulation.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void Changed(string Name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        //For View to Bind to
        public string[]? TimeMonth { get; set; }
        public SeriesCollection SushiPriceSeries { get; set; }
        public SeriesCollection FishCaughtWeightSeries { get; set; }
        public SeriesCollection FishPopulationSeries { get; set; }
        public SeriesCollection FisherIncomeSeries { get; set; }

        public ICommand? StartSimulation { get; set; }

        //private local stuff
        private FishNetwork? _fishNetwork;
        private FisherNetwork? _fisherNetwork;
        private bool isSimulationRunning = false;
        private IObservable<long> clock;
        private IDisposable clockSubsription;
        private Subject<string> messageFromFish;
        private Subject<string> messageFromFisher;
        private Queue<int> _fishPopulationCache;
        private ActionBlock<int> executeCycle;
        private int month=1;

        public MainViewModel() {
           
            _fishPopulationCache = new Queue<int>();
            executeCycle = new ActionBlock<int>(val=>oneMonthAdvance());

            StartSimulation = new RelayCommand(startstopSimulation, (parameter) => { return true; });
            SushiPriceSeries = new SeriesCollection {

                new LineSeries{
                    Values = new ChartValues<int>()
                }
            };

            FishCaughtWeightSeries = new SeriesCollection {

                new LineSeries{
                    Values = new ChartValues<int>()
                }
            };

            FishPopulationSeries = new SeriesCollection {

                new LineSeries{
                    Values = new ChartValues<int>()
                }
            };

            FisherIncomeSeries = new SeriesCollection {

                new LineSeries{
                    Values = new ChartValues<int>()
                }
            };

            messageFromFish = new Subject<string>();
            messageFromFisher = new Subject<string>();

            
            

            clock = Observable.Interval(TimeSpan.FromSeconds(1), Scheduler.ThreadPool);
            //initialize fish and fisher populations
            //fish
            _fishNetwork = new FishNetwork();
            _fishNetwork.population = 1600000;
            _fishNetwork.publisher.Subscribe(messageFromFish);
            messageFromFish.Subscribe(updateFishPopulationSeries);

            //fishermen
            //_fisherNetwork = new FisherNetwork();
            //_fisherNetwork.population = 136000;

        }

        private void startstopSimulation(object parameter)
        {
            if (isSimulationRunning)
            {
                clockSubsription.Dispose();
                isSimulationRunning = false;

            }
            else {
                clockSubsription?.Dispose();
                clockSubsription = clock.Subscribe(val => executeCycle.Post(0));
                isSimulationRunning = true;
            }
        }

        private void updateFishPopulationSeries(string fishMessage)
        {
            int count = Int32.Parse(fishMessage);
            _fishPopulationCache.Enqueue(count);
            if (FishPopulationSeries[0].Values.Count > 6)
            {
                FishPopulationSeries[0].Values.RemoveAt(0);

            }
            FishPopulationSeries[0].Values.Add(count);

        }

        private void oneMonthAdvance()
        {
            int monthOfYear = month % 12;
            if (monthOfYear == 6 || monthOfYear == 7 || monthOfYear == 8)
            {
                _fishNetwork?.growByFactor(1.03228011546);
                
            }
            _fishNetwork?.reportPopulation();
            month++;
        }
    }
}
