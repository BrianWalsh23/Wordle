﻿using Mopups.Hosting;
using Wordle.ViewModel;

namespace Wordle;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureMopups()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddTransient<GameViewModel>();
        builder.Services.AddTransient<MainPage>();

        return builder.Build();
    }
}