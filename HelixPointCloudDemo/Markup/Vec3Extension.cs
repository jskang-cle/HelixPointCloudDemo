using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace HelixPointCloudDemo.Markup;
internal class Vec3Extension : MarkupExtension
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new SharpDX.Vector3(X, Y, Z);
    }
}
