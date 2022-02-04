using Avalonia.Web.Blazor;

namespace YawPitchRollTransform.Web;

public partial class App
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        WebAppBuilder.Configure<YawPitchRollTransform.App>()
            .SetupWithSingleViewLifetime();
    }
}
