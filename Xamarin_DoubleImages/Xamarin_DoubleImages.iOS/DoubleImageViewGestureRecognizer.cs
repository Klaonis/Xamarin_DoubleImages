using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UIKit;

namespace Xamarin_DoubleImages.iOS
{
    public class DoubleImageViewGestureRecognizer : UIGestureRecognizer
    {
        private ICommand _updateUICommand;

        public DoubleImageViewGestureRecognizer(ICommand command) : base()
        {
            _updateUICommand = command;
        }

        /// <summary>
        ///   Is called when the fingers touch the screen.
        /// </summary>
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            // we want one and only one finger
            if (touches.Count != 1)
            {
                base.State = UIGestureRecognizerState.Failed;
            }
            else
            {
                _updateUICommand.Execute(true);
            }

            Console.WriteLine(base.State.ToString());
        }

        /// <summary>
        ///   Called when the touches are cancelled due to a phone call, etc.
        /// </summary>
        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            // we fail the recognizer so that there isn't unexpected behavior 
            // if the application comes back into view

            _updateUICommand.Execute(false);

            base.State = UIGestureRecognizerState.Failed;
        }

        /// <summary>
        ///   Called when the fingers lift off the screen
        /// </summary>
        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            _updateUICommand.Execute(false);

            Console.WriteLine(base.State.ToString());
        }
    }
}
