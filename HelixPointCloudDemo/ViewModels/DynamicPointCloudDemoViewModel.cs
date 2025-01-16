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
using HelixToolkit.SharpDX.Core.Assimp;
using System.IO;
using ReactiveUI;

namespace HelixPointCloudDemo.ViewModels;

public partial class DynamicPointCloudDemoViewModel : ObservableObject
{
    [ObservableProperty]
    private SceneNodeGroupModel3D _groupModel;

    [ObservableProperty]
    private bool _showGroup1 = true;

    [ObservableProperty]
    private bool _showGroup2 = true;

    public DynamicPointCloudDemoViewModel()
    {
        GroupModel = new SceneNodeGroupModel3D();

        Thread t = new Thread(AddPointClouds);
        t.Start();

        this.WhenAnyValue(x => x.ShowGroup1)
            .Subscribe(show =>
            {
                if (GroupModel.GroupNode.Items.FirstOrDefault(x => x.Name == "Group1") is SceneNode group1)
                {
                    group1.Visible = show;
                }
            });

        this.WhenAnyValue(x => x.ShowGroup2)
            .Subscribe(show =>
            {
                if (GroupModel.GroupNode.Items.FirstOrDefault(x => x.Name == "Group2") is SceneNode group2)
                {
                    group2.Visible = show;
                }
            });
    }

    void AddPointClouds()
    {
        Thread.Sleep(2000);

        Importer importer = new();
        SceneNode cp250s = importer.Load("./Assets/copick3d250s.obj").Root;
        cp250s.Name = "copick3d250s";
        cp250s.ModelMatrix = Matrix.RotationX(MathF.PI / 2f) * Matrix.Scaling(1000) * Matrix.Translation(0f, 0f, 0f);

        SceneNode source = LoadModel("./Assets/source.ply", new(1f, 0f, 0f, 0f));
        SceneNode sourceAligned = LoadModel("./Assets/sourceAligned.ply", new(0f, 1f, 0f, 1f));
        SceneNode target1 = LoadModel("./Assets/target1.ply", new(1f));
        SceneNode target2 = LoadModel("./Assets/target2.ply", new(1f, 1f, 0f, 1f));

        GroupNode group = new()
        {
            Name = "Group1",
        };
        group.AddChildNode(cp250s);
        group.AddChildNode(source);
        group.AddChildNode(sourceAligned);

        GroupNode group2 = new()
        {
            Name = "Group2",
        };
        group2.AddChildNode(target1);
        group2.AddChildNode(target2);

        GroupModel.AddNode(group);
        GroupModel.AddNode(group2);
    }

    private static SceneNode LoadModel(string modelPath, Color4 color)
    {
        var model = ModelLoaderManager.LoadFile(modelPath);

        var geom = new PointGeometry3D()
        {
            Positions = new Vector3Collection(model.Item1.Select(x => new Vector3(x.x, -x.y, x.z))),
        };

        var node = new PointNode()
        {
            Name = Path.GetFileNameWithoutExtension(modelPath),
            Geometry = geom,
            Material = new PointMaterialCore() { PointColor = color, Width = 0.2f, Height = 0.2f, FixedSize = false },
        };

        return node;
    }
}
