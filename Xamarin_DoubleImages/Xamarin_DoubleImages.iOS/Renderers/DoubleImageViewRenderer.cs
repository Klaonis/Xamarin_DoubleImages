using CoreGraphics;
using Foundation;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin_DoubleImages.iOS.Renderers;
using Xamarin_DoubleImages.Views;

[assembly: ExportRenderer(typeof(DoubleImageView), typeof(DoubleImageViewRenderer))]
namespace Xamarin_DoubleImages.iOS.Renderers
{
    public class DoubleImageViewRenderer : ViewRenderer
    {
        private ICommand _updateUICommand;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if(Control == null)
            {

                var array = NSBundle.MainBundle.LoadNib("DoubleImageView", null, null);
                UIView nativeControl = Runtime.GetNSObject<UIView>(array.ValueAt(0));
                UIImageView leftImage = (UIImageView)nativeControl.ViewWithTag(1);
                UIImageView rightImage = (UIImageView)nativeControl.ViewWithTag(2);
                leftImage.Image  = UIImage.FromBundle("firstImage");
                rightImage.Image  = UIImage.FromBundle("rightImage");
                SetNativeControl(nativeControl);

            }

            if (Control != null)
            {
                DoubleImageView formView = (e.NewElement as DoubleImageView);
                UIView view = Control;

                ExtractFormData(formView);

                //MANAGE touch events
                if (_updateUICommand != null)
                {

                    view.UserInteractionEnabled = true;

                    DoubleImageViewGestureRecognizer imageGesture = new DoubleImageViewGestureRecognizer(_updateUICommand);
                    view.AddGestureRecognizer(imageGesture);
                }
            }
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
