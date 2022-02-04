using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YawPitchRollTransform;

public class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}

