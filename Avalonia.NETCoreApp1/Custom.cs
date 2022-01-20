using System;
using System.Numerics;
using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.NETCoreApp1
{
    public class Custom : Decorator
    {
        public static readonly StyledProperty<double> YawProperty = 
            AvaloniaProperty.Register<Custom, double>(nameof(Yaw));

        public static readonly StyledProperty<double> PitchProperty = 
            AvaloniaProperty.Register<Custom, double>(nameof(Pitch));

        public static readonly StyledProperty<double> RollProperty = 
            AvaloniaProperty.Register<Custom, double>(nameof(Roll));

        public double Yaw
        {
            get => GetValue(YawProperty);
            set => SetValue(YawProperty, value);
        }

        public double Pitch
        {
            get => GetValue(PitchProperty);
            set => SetValue(PitchProperty, value);
        }

        public double Roll
        {
            get => GetValue(RollProperty);
            set => SetValue(RollProperty, value);
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            SetChildTransform();

            base.OnAttachedToVisualTree(e);
        }

        protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
        {
            if (change.Property == YawProperty
                || change.Property == PitchProperty
                || change.Property == RollProperty)
            {
                SetChildTransform();
                //InvalidateVisual();
            }
            
            base.OnPropertyChanged(change);
        }

        private void SetChildTransform()
        {
            if (Child is { })
            {
                var matrix = CrateTransform(Yaw, Pitch, Roll);
                Child.RenderTransform = new MatrixTransform(matrix);
            }
        }

        private static Matrix CrateTransform(double yaw, double pitch, double roll)
        {
            static double DegreeToRadian(double degrees) => Math.PI * degrees / 180.0;
            var yawRadians = (float)DegreeToRadian(yaw);
            var pitchRadians = (float)DegreeToRadian(pitch);
            var rollRadians = (float)DegreeToRadian(roll);

            var q = Quaternion.CreateFromYawPitchRoll(yawRadians, pitchRadians, rollRadians);
            var m = Matrix4x4.CreateFromQuaternion(q);

            return new Matrix(m.M11, m.M12, m.M21, m.M22, m.M31, m.M32);
        }

        public override void Render(DrawingContext context)
        {
            //var op = new CustomDrawOp(Yaw, Pitch, Roll, Bounds);
            //context.Custom(op);
            
            base.Render(context);
        }
    }
}
