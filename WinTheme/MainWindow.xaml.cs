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
            
        var isLightAppTheme = (int) key.GetValue("AppsUseLightTheme")! == 1;
        var isLightSystemTheme = (int) key.GetValue("SystemUsesLightTheme")! == 1;
        var transparencyEnabled = (int) key.GetValue("EnableTransparency")! == 1;

        var appsThemeButton = isLightAppTheme ? LightAppsButton : DarkAppsButton;
        appsThemeButton.IsChecked = true;
        
        var systemThemeButton = isLightSystemTheme ? LightSystemButton : DarkSystemButton;
        systemThemeButton.IsChecked = true;

        TransparencyButton.IsChecked = transparencyEnabled;
    }

    private void ChangeAppsTheme(object sender, RoutedEventArgs e)
    {
        var key = Registry.CurrentUser
            .OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", true)!;

        key.SetValue("AppsUseLightTheme", LightAppsButton.IsChecked == true ? 1 : 0);
        
        key.Close();
    }
    
    private void ChangeSystemTheme(object sender, RoutedEventArgs e)
    {
        var key = Registry.CurrentUser
            .OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", true)!;

        key.SetValue("SystemUsesLightTheme", LightSystemButton.IsChecked == true ? 1 : 0);
        
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
