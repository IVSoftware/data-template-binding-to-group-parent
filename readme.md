# Bind to group header

If the view contains some number of `AthletesPerSportsList` groups, and these in turn contain some number of `AthleteModel` items, then the xaml might look like so:

**XAML**

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


