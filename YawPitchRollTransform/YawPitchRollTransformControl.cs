using System;
using System.Numerics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace YawPitchRollTransform;

public class YawPitchRollTransformControl : Decorator
{
    public static readonly StyledProperty<double> YawProperty = 
        AvaloniaProperty.Register<YawPitchRollTransformControl, double>(nameof(Yaw));

    public static readonly StyledProperty<double> PitchProperty = 
        AvaloniaProperty.Register<YawPitchRollTransformControl, double>(nameof(Pitch));

    public static readonly StyledProperty<double> RollProperty = 
        AvaloniaProperty.Register<YawPitchRollTransformControl, double>(nameof(Roll));

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

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        if (change.Property == YawProperty
            || change.Property == PitchProperty
            || change.Property == RollProperty)
        {
            SetChildTransform();
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
        return new Matrix(m.M11, m.M12, m.M14, m.M21, m.M22, m.M24, m.M41, m.M42, m.M44);
    }
}
