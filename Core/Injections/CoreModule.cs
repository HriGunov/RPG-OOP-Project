using System;
using AudioPlayerManager;
using AudioPlayerManager.Contracts;
using Autofac;
using Core.Logger;
using Core.Windows;
using Models;
using SunshineConsole;
using Module = Autofac.Module;

namespace Core.Injections
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // I experienced some problems with Autofac, so moved everything from here to Startup.

            //base.Load(builder);
        }
    }
}
