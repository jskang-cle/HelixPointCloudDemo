using System.IO;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using HelixPointCloudDemo.Loader;

using SharpDX;


namespace HelixPointCloudDemo.ViewModels;

public partial class CoPickViewerViewModel : ObservableObject
{
    [ObservableProperty]
    private PointNormal[]? _pointNormals1;

    [ObservableProperty]
    private Color4[]? _pointColors1;

    public CoPickViewerViewModel()
    {
        (PointNormals1, PointColors1) = ModelLoaderManager.LoadFile("assets/frame_0034_IMG_Texture_8Bit.png");
    }

    [RelayCommand]
    public void OpenFile()
    {
        string assetsPath = Path.GetFullPath("assets");

        var dialog = new Microsoft.Win32.OpenFileDialog
        {
            InitialDirectory = assetsPath,
            Filter = ModelLoaderManager.GetFileFilter(true)
        };

        if (dialog.ShowDialog() == true)
        {
            var (pointNormals, pointColors) = ModelLoaderManager.LoadFile(dialog.FileName);
            PointNormals1 = pointNormals;
            PointColors1 = pointColors;
        }
    }

    [RelayCommand]
    public void Exit()
    {
        App.Current.Shutdown();
    }
}
