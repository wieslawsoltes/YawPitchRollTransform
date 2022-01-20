using System;
using System.IO;
using System.Numerics;
using SkiaSharp;

namespace Avalonia.NETCoreApp1
{
    public class Demo
    {
        public static void Draw(SKCanvas canvas, SKRect bounds, double yaw, double pitch, double roll)
        {
            canvas.Save();

            canvas.Translate(bounds.Width / 2, bounds.Height / 2);

            var rect = new SKRect(-100, -100, 100, 100);

            var skMatrix = CrateTransform(yaw, pitch, roll);

            canvas.Concat(ref skMatrix);

            var paint = new SKPaint
            {
                Color = SKColors.Red,
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            canvas.DrawRoundRect(rect, 25, 25, paint);

            canvas.Restore();
        }

        private static SKMatrix CrateTransform(double yaw, double pitch, double roll)
        {
            var yawRadians = (float)DegreeToRadian(yaw);
            var pitchRadians = (float)DegreeToRadian(pitch);
            var rollRadians = (float)DegreeToRadian(roll);

            var q = Quaternion.CreateFromYawPitchRoll(yawRadians, pitchRadians, rollRadians);
            var m = Matrix4x4.CreateFromQuaternion(q);

            var skMatrix44 = ToSKMatrix44(ref m);
            var skMatrix = skMatrix44.Matrix;

            return skMatrix;
        }

        public static void Save(string path, Action<SKCanvas> draw, int width, int height)
        {
            var skImageInfo = new SKImageInfo(width, height, SKImageInfo.PlatformColorType, SKAlphaType.Unpremul,  SKColorSpace.CreateSrgb());
            var skBitmap = new SKBitmap(skImageInfo);
            using var skCanvas = new SKCanvas(skBitmap);
            draw(skCanvas);
            using var skImage = SKImage.FromBitmap(skBitmap);
            using var skData = skImage.Encode(SKEncodedImageFormat.Png, 100);
            if (skData is null) return;
            using var stream = File.OpenWrite(path);
            skData.SaveTo(stream);
        }

        public static SKMatrix44 ToSKMatrix44(ref Matrix4x4 m)
        {
            var rowMajor = new float[] {
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
            };
            return SKMatrix44.FromRowMajor(rowMajor);
        }

        public static double DegreeToRadian(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }

        public static double RadianToDegree(double radians)
        {
            return radians * (180.0 / Math.PI);
        }
    }
}
