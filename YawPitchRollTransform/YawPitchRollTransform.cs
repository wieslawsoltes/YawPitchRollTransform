using System.Numerics;

// ReSharper disable once CheckNamespace
namespace Avalonia.Media;

public class YawPitchRollTransform : Transform
{
    private readonly bool _isInitializing;
    
    public static readonly StyledProperty<double> YawProperty = 
        AvaloniaProperty.Register<YawPitchRollTransform, double>(nameof(Yaw));

    public static readonly StyledProperty<double> PitchProperty = 
        AvaloniaProperty.Register<YawPitchRollTransform, double>(nameof(Pitch));

    public static readonly StyledProperty<double> RollProperty = 
        AvaloniaProperty.Register<YawPitchRollTransform, double>(nameof(Roll));

    public YawPitchRollTransform() { }
    
    public YawPitchRollTransform(double yaw, double pitch, double roll) : this()
    {
        _isInitializing = true;
        Yaw = yaw;
        Pitch = pitch;
        Roll = roll;
        _isInitializing = false;
    }

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

    public override Matrix Value
    {
        get
        {
            var (yaw, pitch, roll) = (Yaw, Pitch, Roll);

            var quaternion = Quaternion.CreateFromYawPitchRoll(
                (float)Matrix.ToRadians(yaw), 
                (float)Matrix.ToRadians(pitch), 
                (float)Matrix.ToRadians(roll));

            var matrix44 = Matrix4x4.CreateFromQuaternion(quaternion);

            var matrix = new Matrix(
                matrix44.M11,
                matrix44.M12,
                matrix44.M14,
                matrix44.M21,
                matrix44.M22,
                matrix44 .M24,
                matrix44 .M41,
                matrix44 .M42,
                matrix44 .M44);

            return matrix;
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        if (!_isInitializing) RaiseChanged();
    } 
}
