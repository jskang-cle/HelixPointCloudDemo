using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

using HelixPointCloudDemo.Loader;

using HelixToolkit.SharpDX.Core.Model.Scene;
using HelixToolkit.SharpDX.Core.Model;
using HelixToolkit.SharpDX.Core;
using HelixToolkit.Wpf.SharpDX;

using SharpDX;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;

namespace HelixPointCloudDemo.ViewModels;

public partial class DynamicPointCloudDemoViewModel : ObservableObject
{
    [ObservableProperty]
    private SceneNodeGroupModel3D _groupModel;

    public DynamicPointCloudDemoViewModel()
    {
        GroupModel = new SceneNodeGroupModel3D();

        Thread t = new Thread(AddPointClouds);
        t.Start();
    }

    void AddPointClouds()
    {
        Thread.Sleep(2000);

        string[] models = ["source.ply", "sourceAligned.ply", "target1.ply", "target2.ply"];
        Color4[] colors = [new(1f, 0f, 0f, 0f), new(0f, 1f, 0f, 1f), new(1f), new(1f, 1f, 0f, 1f)];

        for (int i = 0; i < 4; i++)
        {
            var model = ModelLoaderManager.LoadFile("./Assets/" + models[i]);

            var geom = new PointGeometry3D()
            {
                Positions = new Vector3Collection(model.Item1.Select(x => new Vector3(x.x, -x.y, x.z))),
            };

            var node = new PointNode()
            {
                Geometry = geom,
                Material = new PointMaterialCore() { PointColor = colors[i] },
            };

            GroupModel.AddNode(node);
        }
    }
}
