using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using HelixPointCloudDemo.Loader;
using HelixPointCloudDemo.ViewModels;

using HelixToolkit.SharpDX.Core;
using HelixToolkit.SharpDX.Core.Model;
using HelixToolkit.SharpDX.Core.Model.Scene;
using HelixToolkit.Wpf.SharpDX;

using SharpDX;

namespace HelixPointCloudDemo.Views;

/// <summary>
/// Interaction logic for DynamicPointCloudDemo.xaml
/// </summary>
public partial class DynamicPointCloudDemo : Window
{
    public DynamicPointCloudDemoViewModel ViewModel { get; }

    public DynamicPointCloudDemo()
    {
        DataContext = ViewModel = new DynamicPointCloudDemoViewModel();

        InitializeComponent();
    }
}
