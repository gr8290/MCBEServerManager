using Microsoft.Owin.Hosting;
using System;
using WebApi.AppStart;

namespace WebApi
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
