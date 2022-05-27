using System.Numerics;

// ReSharper disable once CheckNamespace
namespace Avalonia.Media;

/// <summary>
///  Non-Affine 3D transformation for rotating a visual around a definable yaw, pitch, and roll.
/// </summary>
public class YawPitchRollTransform : Transform
{
    private readonly bool _isInitializing;

    /// <summary>
    /// Defines the <see cref="Yaw"/> property.
    /// </summary>
    public static readonly StyledProperty<double> YawProperty = 
        AvaloniaProperty.Register<YawPitchRollTransform, double>(nameof(Yaw));

    /// <summary>
    /// Defines the <see cref="Pitch"/> property.
    /// </summary>
    public static readonly StyledProperty<double> PitchProperty = 
        AvaloniaProperty.Register<YawPitchRollTransform, double>(nameof(Pitch));

    /// <summary>
    /// Defines the <see cref="Roll"/> property.
    /// </summary>
    public static readonly StyledProperty<double> RollProperty = 
        AvaloniaProperty.Register<YawPitchRollTransform, double>(nameof(Roll));

    /// <summary>
    /// Initializes a new instance of the <see cref="YawPitchRollTransform"/> class.
    /// </summary>
    public YawPitchRollTransform() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="YawPitchRollTransform"/> class.
    /// </summary>
    /// <param name="yaw">The angle of rotation, in degrees, around the Y axis.</param>
    /// <param name="pitch">The angle of rotation, in degrees, around the X axis.</param>
    /// <param name="roll">The angle of rotation, in degrees, around the Z axis.</param>
    public YawPitchRollTransform(double yaw, double pitch, double roll) : this()
    {
        _isInitializing = true;
        Yaw = yaw;
        Pitch = pitch;
        Roll = roll;
        _isInitializing = false;
    }

    /// <summary>
    /// Sets the angle of rotation, in degrees, around the Y axis.
    /// </summary>
    public double Yaw
    {
        get => GetValue(YawProperty);
        set => SetValue(YawProperty, value);
    }

    /// <summary>
    /// Sets the angle of rotation, in degrees, around the X axis.
    /// </summary>
    public double Pitch
    {
        get => GetValue(PitchProperty);
        set => SetValue(PitchProperty, value);
    }

    /// <summary>
    /// Sets the angle of rotation, in degrees, around the Z axis.
    /// </summary>
    public double Roll
    {
        get => GetValue(RollProperty);
        set => SetValue(RollProperty, value);
    }

    /// <summary>
    /// Gets the transform's <see cref="Matrix"/>. 
    /// </summary>
    public override Matrix Value
    {
        get
        {
            var (yaw, pitch, roll) = (Yaw, Pitch, Roll);

            var matrix44 = Matrix4x4.CreateFromYawPitchRoll(
                (float)Matrix.ToRadians(yaw), 
                (float)Matrix.ToRadians(pitch), 
                (float)Matrix.ToRadians(roll));

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
