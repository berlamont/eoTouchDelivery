using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;

namespace eoTouchDelivery.UWP;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseSharedMauiApp();

        return builder.Build();
    }
}
