using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin_DoubleImages.Views;
using Xamarin_DoubleImages.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using System.Windows.Input;

[assembly: ExportRenderer(typeof(DoubleImageView), typeof(DoubleImageViewRenderer))]
namespace Xamarin_DoubleImages.Droid.Renderers
{
    class DoubleImageViewRenderer : ViewRenderer
    {
        private ICommand _updateUICommand;

        protected override void Dispose(bool disposing)
        {
            //MANAGE touch events
            if (_updateUICommand != null)
            {
                Control.Touch -= Image_Touch;
            }

            _updateUICommand = null;

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            DoubleImageView formView = (e.NewElement as DoubleImageView);
            if(e.NewElement != null)
            {
                if (Control == null)
                {
                    var view = (Context as Activity).LayoutInflater.Inflate(Resource.Layout.DoubleImageLayout, this, false);
                    var firstImage = view.FindViewById<ImageView>(Resource.Id.first_imageview);
                    var secondImage = view.FindViewById<ImageView>(Resource.Id.second_imageview);
                    int firstImageID = Resources.GetIdentifier(System.IO.Path.GetFileNameWithoutExtension(formView.FirstImageSource.ToLower()), "drawable", "Xamarin_DoubleImages.Droid");
                    int secondImageID = Resources.GetIdentifier(System.IO.Path.GetFileNameWithoutExtension(formView.SecondImageSource.ToLower()), "drawable", "Xamarin_DoubleImages.Droid");
                    firstImage.SetImageResource(firstImageID);
                    secondImage.SetImageResource(secondImageID);
                    SetNativeControl(view);
                }

                if (Control != null)
                {
                    ExtractFormData(formView);

                    //MANAGE touch events
                    if (_updateUICommand != null)
                    {
                        Control.Touch += Image_Touch;
                    }
                }
            }
        }

        private void Image_Touch(object sender, TouchEventArgs e)
        {
            var handled = false;
            if (e.Event.Action == MotionEventActions.Down)
            {
                _updateUICommand.Execute(true);
                handled = true;
            }
            else if (e.Event.Action == MotionEventActions.Up)
            {
                _updateUICommand.Execute(false);

                handled = true;
            }

            e.Handled = handled;
        }

        /// <summary>
        /// Extract form view data
        /// </summary>
        /// <param name="formImage">form view object</param>
        private void ExtractFormData(DoubleImageView formView)
        {
            _updateUICommand = formView.SwitchImageCommand;
        }
    }
}