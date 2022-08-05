using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace data_template_binding.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TappableStack : ContentView
    {
        public TappableStack()
        {
            InitializeComponent();
        }
    }
}