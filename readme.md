# Bind to group header

In this minimal example, the `CheckBox` is my "expander" and the group members are shown or hidden when toggled because the `CheckBox` is bound to the `IsExpanded` property of `AthletesPerSportsList` and the `IsVisible` property of the `TappableStack` is bound to it.

![states](https://github.com/IVSoftware/data-template-binding-to-group-parent/blob/master/data-template-binding/data-template-binding/ReadMe/states.png)

***
**XAML**

The XAML is straightforward thanks to the addition of a `Parent` property to the `AthleteModel`.

    <ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
                 xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                 xmlns:controls="clr-namespace:data_template_binding.Controls"
                 xmlns:model="clr-namespace:data_template_binding.Model"
                 x:Class="data_template_binding.MainPage">

        <CollectionView IsGrouped="true"
                        ItemsSource="{x:Binding Groups}"
                        Margin="0,0,0,0">
            <CollectionView.GroupHeaderTemplate>
                <DataTemplate>
                    <Grid BackgroundColor="Cyan">
                        <Grid.RowDefinitions>
                            <RowDefinition Height="40" />
                        </Grid.RowDefinitions>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="40" />
                            <ColumnDefinition Width="*" />
                        </Grid.ColumnDefinitions>
                        <CheckBox IsChecked="{Binding IsExpanded}"/>
                        <Label Text="{Binding Name}"
                               Grid.Column="1"
                               VerticalTextAlignment="Center"/>
                    </Grid>
                </DataTemplate>
            </CollectionView.GroupHeaderTemplate>

            <CollectionView.ItemTemplate>
                <DataTemplate>
                    <Grid BackgroundColor="Aquamarine">
                        <Grid.RowDefinitions>
                            <RowDefinition Height="*" />
                        </Grid.RowDefinitions>
                        <controls:TappableStack IsVisible="{Binding Parent.IsExpanded}}" />
                    </Grid>
                </DataTemplate>
            </CollectionView.ItemTemplate>
        </CollectionView>
    </ContentPage>

    

***
**Items**

The `AthleteModel` now has the `Parent` property of type `AthletesPerSportsList` and also the `TappedCommand` in response to a tap on the `Label`.

    class AthleteModel : INotifyPropertyChanged
    {
        public AthletesPerSportsList Parent { get; internal set; }

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
        ...
    }

***
**Groups**
The view contains groups that are instances of `AthletesPerSportsList` class with a bindable `IsExpanded` property and a `new` version of the `Add` method that attaches itself as `Parent` to any `AthleteModel` added to the list.

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
        ...
    }

