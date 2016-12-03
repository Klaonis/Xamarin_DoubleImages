using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xamarin_DoubleImages.Views
{
    public partial class DoubleImageView : ContentView
    {
        public static readonly BindableProperty FirstImageSourceProperty = BindableProperty.Create("FirstImageSource", typeof(string), typeof(DoubleImageView), string.Empty);
        public string FirstImageSource
        {
            set { SetValue(FirstImageSourceProperty, value); }
            get { return (string)GetValue(FirstImageSourceProperty); }
        }


        public static readonly BindableProperty ImageSizeProperty = BindableProperty.Create("ImageSize", typeof(int), typeof(DoubleImageView), 44);
        public int ImageSize
        {
            set { SetValue(ImageSizeProperty, value); }
            get { return (int)GetValue(ImageSizeProperty); }
        }


        public static readonly BindableProperty SecondImageSourceProperty = BindableProperty.Create("SecondImageSource", typeof(string), typeof(DoubleImageView), string.Empty);
        public string SecondImageSource
        {
            set { SetValue(SecondImageSourceProperty, value); }
            get { return (string)GetValue(SecondImageSourceProperty); }
        }


        public ICommand SwitchImageCommand { get; set; }

        public DoubleImageView()
        {
            BindingContext = this;
            SwitchImageCommand = new Command<bool>((bool isPressed) => UpdateImages(isPressed));
            InitializeComponent();
        }

        private void UpdateImages(bool isPressed)
        {
            string source;
            if (isPressed)
            {
                source = SecondImageSource;
                SecondImageSource = FirstImageSource;
                FirstImageSource = source;
            }
            else
            {
                source = FirstImageSource;
                FirstImageSource = SecondImageSource;
                SecondImageSource = source;
            }
        }
    }
}
