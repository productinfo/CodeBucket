﻿using System;
using UIKit;
using CodeBucket.ViewControllers.Application;
using System.Reactive.Disposables;

namespace CodeBucket.ViewControllers.Repositories
{
    public partial class PrivateRepositoryViewController : BaseViewController
    {
        public PrivateRepositoryViewController() 
            : base("PrivateRepositoryViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            Button.BackgroundColor = UIColor.FromRGB(0x27, 0xae, 0x60);
            Button.SetTitleColor(UIColor.White, UIControlState.Normal);
            Button.Layer.CornerRadius = 6f;

            OnActivation(d => Button.GetClickedObservable().Subscribe(_ => this.PresentUpgradeViewController()).AddTo(d));
        }
    }

    public static class PrivateRepositoryViewControllerExtensions
    {
        public static IDisposable ShowPrivateView(this UIViewController @this)
        {
            var vc = new PrivateRepositoryViewController();
            vc.View.Frame = new CoreGraphics.CGRect(0, 0, @this.View.Bounds.Width, @this.View.Bounds.Height);
            vc.View.AutoresizingMask = UIViewAutoresizing.All;

            vc.View.Alpha = 0;
            @this.View.Add(vc.View);
            UIView.Animate(0.4, 0, UIViewAnimationOptions.CurveEaseIn, () => vc.View.Alpha = 1, null);
            @this.AddChildViewController(vc);

            vc.View.BackgroundColor = Theme.CurrentTheme.PrimaryColor;

            if (@this.NavigationItem.RightBarButtonItem != null)
                @this.NavigationItem.RightBarButtonItem.Enabled = false;

            return Disposable.Create(() =>
            {
                vc.View.RemoveFromSuperview();
                vc.RemoveFromParentViewController();
            });
        }
    }
}


