using Microsoft.Maui.Controls;

namespace iquchnia;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // Ustawiamy główną stronę jako AppShell
        return new Window(new AppShell());
    }
}