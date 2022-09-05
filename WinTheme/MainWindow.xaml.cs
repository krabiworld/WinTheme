using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace WinTheme;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
            
        using var key = Registry.CurrentUser
            .OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")!;
            
        var isLightTheme = (int) key.GetValue("AppsUseLightTheme")! == 1;
        var transparencyEnabled = (int) key.GetValue("EnableTransparency")! == 1;

        if (isLightTheme)
        {
            LightButton.IsChecked = true;
        }
        else
        {
            DarkButton.IsChecked = false;
        }

        TransparencyButton.IsChecked = transparencyEnabled;
    }

    private void ChangeTheme(object sender, RoutedEventArgs e)
    {
        var key = Registry.CurrentUser
            .OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", true)!;

        key.SetValue("AppsUseLightTheme", LightButton.IsChecked == true ? 1 : 0);
        
        key.Close();
    }

    private void ChangeTransparency(object sender, RoutedEventArgs e)
    {
        var isChecked = ((CheckBox) sender).IsChecked == true;
        
        var key = Registry.CurrentUser
            .OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", true)!;

        key.SetValue("EnableTransparency", isChecked ? 1 : 0);
            
        key.Close();
    }
}
