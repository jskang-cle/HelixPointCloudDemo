using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

using HelixToolkit.SharpDX.Core;
using HelixToolkit.SharpDX.Core.Cameras;
using HelixToolkit.Wpf.SharpDX;

using SharpDX;

namespace HelixPointCloudDemo.Controls;
/// <summary>
/// Interaction logic for PointCloudView.xaml
/// </summary>
public partial class PointCloudView : UserControl
{
    public PointCloudView()
    {
        InitializeComponent();
    }

    private void UpdatePointGeometry()
    {
        if (PointNormals is null || PointNormals.Length == 0)
        {
            PointGeometry.ClearAllGeometryData();
            return;
        }

        PointGeometry.Positions = new(PointNormals.Select(pn => new Vector3(pn.x, -pn.y, -pn.z)));

        UpdatePointColor();
        UpdateCameraTarget();
    }

    private void UpdatePointColor()
    {
        if (PointNormals is null || PointNormals.Length == 0)
            return;

        if (PointColors?.Length == PointNormals.Length)
        {
            PointGeometry.Colors = new(PointColors);
            PointGeometryModel.Color = Colors.White;
        }
        else if (PointColor.HasValue)
        {
            PointGeometry.Colors = null;
            PointGeometryModel.Color = PointColor.Value;
        }
        else
        {
            PointGeometry.Colors = null;
            PointGeometryModel.Color = Colors.White;
        }
    }

    private void UpdateCameraTarget()
    {
        Point3D newTargetPoint;

        if (!AutoCenterTargetPoint)
        {
            newTargetPoint = TargetPoint;
        }
        else if (PointNormals != null)
        {
            float avgZ = PointGeometry.Positions.AsParallel().Average(v => v.Z);
            newTargetPoint = new Point3D(0, 0, avgZ);
        }
        else
        {
            return;
        }

        // 반대로 Position을 0으로 설정하고 look direction을 계산해도 됨
        Vector3D lookDirection = new(0, 0, -CameraDistance);
        Point3D position = (Point3D)(newTargetPoint - lookDirection);

        Debug.WriteLine($"New target point: {newTargetPoint}");
        Debug.WriteLine($"Look direction: {lookDirection}");
        Debug.WriteLine($"New camera position: {position}");

        // DefaultCamera 의 값을 수정해야 R 키로 시점을 초기화했을 때 이 시점으로 복귀함
        DefaultCamera.LookDirection = lookDirection;
        DefaultCamera.Position = position;

        HelixViewPort.Reset();
    }
}
