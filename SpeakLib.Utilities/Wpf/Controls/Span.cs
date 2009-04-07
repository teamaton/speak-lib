using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpeakFriend.Utilities.Wpf
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Frontend.ServiceController.Wpf.ControlsCommon"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Frontend.ServiceController.Wpf.ControlsCommon;assembly=Frontend.ServiceController.Wpf.ControlsCommon"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:SpanClickable/>
    ///
    /// </summary>
    public class Span : System.Windows.Documents.Span
    {
        public static readonly RoutedEvent ClickEvent;

        static Span()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Span), new FrameworkPropertyMetadata(typeof(Span)));
            ClickEvent = ButtonBase.ClickEvent.AddOwner(typeof(Span));
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
