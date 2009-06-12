using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace SpeakFriend.Utilities.Wpf
{
    public class Image : System.Windows.Controls.Image
    {
        public static readonly RoutedEvent ClickEvent;

        static Image()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Image), new FrameworkPropertyMetadata(typeof(Image)));
            ClickEvent = ButtonBase.ClickEvent.AddOwner(typeof(Image));
        }

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            CaptureMouse();
        }

 
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (IsMouseCaptured)
            {
                ReleaseMouseCapture();

                if (IsMouseOver)
                    RaiseEvent(new RoutedEventArgs(ClickEvent, this));
            }
        }
    }
}
