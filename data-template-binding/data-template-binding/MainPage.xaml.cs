using data_template_binding.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace data_template_binding
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new MainPageBindings();
            InitializeComponent();
        }
    }
    class MainPageBindings
    {
        int countGroup;
        int countItem;
        public MainPageBindings()
        {
            var group = new AthletesPerSportsList { Name = $"AthletesPerSports Group {countGroup++}" };
            group.Add(new AthleteModel { Name = $"Athlete {countItem++}" });
            group.Add(new AthleteModel { Name = $"Athlete {countItem++}" });
            Groups.Add(group);

            group = new AthletesPerSportsList { Name = $"AthletesPerSports Group {countGroup++}" };
            group.Add(new AthleteModel { Name = $"Athlete {countItem++}" });
            group.Add(new AthleteModel { Name = $"Athlete {countItem++}" });
            Groups.Add(group);
        }

        public ObservableCollection<AthletesPerSportsList> Groups { get; } = new ObservableCollection<AthletesPerSportsList>();
    }
}

namespace data_template_binding.Model
{ 
    class AthletesPerSportsList : ObservableCollection<AthleteModel>, INotifyPropertyChanged
    {
        public new void Add(AthleteModel baseModel)
        {
            baseModel.Parent = this;
            base.Add(baseModel);
        }
        bool _IsExpanded = false;
        public bool IsExpanded
        {
            get => _IsExpanded;
            set
            {
                if (!Equals(_IsExpanded, value))
                {
                    _IsExpanded = value;
                    OnPropertyChanged();
                }
            }
        }
        public AthletesPerSportsList() => base.PropertyChanged += (sender, e) => OnPropertyChanged(e.PropertyName);
        string _Name = string.Empty;
        public string Name
        {
            get => _Name;
            // set => SetProperty(ref _Name, value);
            set
            {
                if (!Equals(_Name, value))
                {
                    _Name = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public new event PropertyChangedEventHandler PropertyChanged;
    }
    class AthleteModel : INotifyPropertyChanged
    {
        public AthleteModel() => onInitCommands();
        public AthletesPerSportsList Parent { get; internal set; }

        private void onInitCommands()
        {
            TappedCommand = new Command(OnTapped);
        }
        public ICommand TappedCommand { get; private set; }
        private async void OnTapped(object o)
        {
            var rsp = await App.Current.MainPage.DisplayPromptAsync("Edit Label", "Enter new text");
            if(rsp != null)
            {
                Name = rsp;
            }
        }

        public object TappedCommandParameter { get; set; }
        bool _IsVisible = false;
        public bool IsVisible
        {
            get => _IsVisible;
            set
            {
                if (!Equals(_IsVisible, value))
                {
                    _IsVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        string _Name = String.Empty;
        public string Name
        {
            get => _Name;
            set
            {
                if (!Equals(_Name, value))
                {
                    _Name = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
