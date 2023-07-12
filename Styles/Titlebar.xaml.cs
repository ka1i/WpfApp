using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Styles
{
    /// <summary>
    /// Titlebar.xaml 的交互逻辑
    /// </summary>
    public partial class Titlebar
    {
        /// <summary>
        /// 窗口标题栏主题自定义
        /// </summary>
        public static readonly DependencyProperty TitleBarButtonStateProperty = DependencyProperty.RegisterAttached(
            "TitleBarButtonState", typeof(WindowState?), typeof(Titlebar),
            new PropertyMetadata(null, OnButtonStateChanged));

        public static WindowState? GetTitleBarButtonState(DependencyObject element)
            => (WindowState?)element.GetValue(TitleBarButtonStateProperty);

        public static void SetTitleBarButtonState(DependencyObject element, WindowState? value)
            => element.SetValue(TitleBarButtonStateProperty, value);

        public static readonly DependencyProperty IsTitleBarCloseButtonProperty = DependencyProperty.RegisterAttached(
            "IsTitleBarCloseButton", typeof(bool), typeof(Titlebar),
            new PropertyMetadata(false, OnIsCloseButtonChanged));
        public static bool GetIsTitleBarCloseButton(DependencyObject element)
    => (bool)element.GetValue(IsTitleBarCloseButtonProperty);

        public static void SetIsTitleBarCloseButton(DependencyObject element, bool value)
            => element.SetValue(IsTitleBarCloseButtonProperty, value);

        private static void OnTitleBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is null) throw new NotSupportedException("TitleBar property should not be null.");
        }

        private static void OnButtonStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = (Button)d;

            if (e.OldValue is WindowState)
            {
                button.Click -= StateButton_Click;
            }

            if (e.NewValue is WindowState)
            {
                button.Click -= StateButton_Click;
                button.Click += StateButton_Click;
            }
        }
        private static void OnIsCloseButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = (Button)d;

            if (e.OldValue is true)
            {
                button.Click -= CloseButton_Click;
            }

            if (e.NewValue is true)
            {
                button.Click -= CloseButton_Click;
                button.Click += CloseButton_Click;
            }
        }
        private static void StateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (DependencyObject)sender;
            var window = Window.GetWindow(button);
            var state = GetTitleBarButtonState(button);
            if (window != null && state != null)
            {
                window.WindowState = state.Value;
            }
        }

        private static void CloseButton_Click(object sender, RoutedEventArgs e)
            => Window.GetWindow((DependencyObject)sender)?.Close();
    }
}
