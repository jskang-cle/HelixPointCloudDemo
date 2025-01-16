using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelixPointCloudDemo.Loader;

public class ModelLoaderManager
{
    public static IReadOnlyList<IModelLoader> Loaders { get; }

    static ModelLoaderManager()
    {
        Loaders =
        [
            new CoPick3DLoader(),
            new PlyLoader(),
        ];
    }

    public static string GetFileFilter(bool includeAllFilter)
    {
        var sb = new StringBuilder();
        
        if (includeAllFilter)
        {
            string allFilter = string.Join("; ", Loaders.Select(x => $"*{x.SupportedExtension}"));
            sb.Append($"All supported files ({allFilter})|{allFilter}|");
        }

        foreach (var loader in Loaders)
        {
            sb.Append(loader.Name);
            sb.Append($" (*{loader.SupportedExtension})|*{loader.SupportedExtension}|");
        }
        sb.Append("All files (*.*)|*.*");
        return sb.ToString();
    }

    public static (PointNormal[], SharpDX.Color4[]?) LoadFile(string filePath)
    {
        var loader = Loaders.FirstOrDefault(l => filePath.EndsWith(l.SupportedExtension))
            ?? throw new NotSupportedException($"File extension not supported: {filePath}");

        return loader.LoadFile(filePath);
    }
}
