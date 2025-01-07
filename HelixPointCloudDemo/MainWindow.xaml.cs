using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using HelixPointCloudDemo.Views;

namespace HelixPointCloudDemo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ButtonOpenCoPickViewer_Click(object sender, RoutedEventArgs e)
    {
        new CoPickViewerWindow().ShowDialog();
    }

    private void ButtonOpenMultiPointcloudDemo_Click(object sender, RoutedEventArgs e)
    {
        new MultiPointCloudDemo().ShowDialog();
    }
}