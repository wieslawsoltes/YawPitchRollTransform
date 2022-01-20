using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;

namespace Avalonia.NETCoreApp1
{
    public class CustomDrawOp : ICustomDrawOperation
    {
        private readonly double _yaw;
        private readonly double _pitch;
        private readonly double _roll;
        private readonly Rect _bounds;

        public CustomDrawOp(double yaw, double pitch, double roll, Rect bounds)
        {
            _yaw = yaw;
            _pitch = pitch;
            _roll = roll;
            _bounds = bounds;
        }

        public Rect Bounds => _bounds;

        public void Dispose()
        {
        }

        public bool HitTest(Point p) => _bounds.Contains(p);

        public bool Equals(ICustomDrawOperation? other) => false;

        public void Render(IDrawingContextImpl context)
        {
            if (context is not ISkiaDrawingContextImpl contextImpl)
            {
                return;
            }

            Demo.Draw(contextImpl.SkCanvas, _bounds.ToSKRect(), _yaw, _pitch, _roll);
        }
    }
}
