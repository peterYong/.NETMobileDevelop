using Foundation;
using System;
using UIKit;

namespace PhonewordIOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        string translatedNumber = "";
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.

            
            TranslateButton.TouchUpInside += TranslateButton_TouchUpInside;
            CallButton.TouchUpInside += CallButton_TouchUpInside;
        }

       
        private void TranslateButton_TouchUpInside(object sender, EventArgs e)
        {
            translatedNumber = PhonewordIOS.Customize.PhoneTranslator.ToNumber(PhoneNumberText.Text);
            PhoneNumberText.ResignFirstResponder();
            if (translatedNumber == "")
            {
                CallButton.SetTitle("Call", UIControlState.Normal);
                CallButton.Enabled = false;
            }
            else
            {
                CallButton.SetTitle("Call " + translatedNumber, UIControlState.Normal);
                CallButton.Enabled = true;
            }
        }

        private void CallButton_TouchUpInside(object sender, EventArgs e)
        {
            var url = new NSUrl("tel:" + translatedNumber);

            // Use URL handler with tel: prefix to invoke Apple's Phone app,
            // otherwise show an alert dialog

            if (!UIApplication.SharedApplication.OpenUrl(url))
            {
                var alert = UIAlertController.Create("Not supported", "Scheme 'tel:' is not supported on this device", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                PresentViewController(alert, true, null);
            }
        }



        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}