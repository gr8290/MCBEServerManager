using BEServerManager.Utill;
using BEServerManager.WebApi.App_Start;
using Microsoft.Owin.Hosting;
using System;
using System.Windows;

namespace BEServerManager.WebApi
{
    public class BeServerWebApi
    {
        IDisposable wa = null;

        public bool IsRunning { get { return wa != null; } }

        public void Start()
        {
            string baseAddress = "http://localhost:1000/";
            wa = WebApp.Start<Startup>(url: baseAddress);
        }

        public void Stop()
        {
            wa?.Dispose();
            wa = null;
        }

    }
}
