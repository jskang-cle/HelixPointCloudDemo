using System.Text;
using System.Windows;

using HelixPointCloudDemo.ViewModels;


namespace HelixPointCloudDemo.Views;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class CoPickViewerWindow : Window
{
    public CoPickViewerWindow()
    {
        InitializeComponent();

        DataContext = new CoPickViewerViewModel();
    }
}