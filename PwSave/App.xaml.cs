using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace PwSave
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        
        public App()
        {

        }

        protected override void OnExit(ExitEventArgs e)
        {
            //controller?.Dispose();
            //controller = null;
            base.OnExit(e);
        }
    }
}
