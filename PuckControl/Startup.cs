﻿using System;
using System.IO;

[assembly: CLSCompliant(true)]
namespace PuckControl
{
    internal static class Startup
    {
        [System.STAThreadAttribute()]
        public static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            PuckControl.App app = new PuckControl.App();
            app.InitializeComponent();
            @app.Run();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                var ex = (Exception)e.ExceptionObject;

                Log(ex.Message, w);
                Log("Stack trace:", w);
                Log(ex.StackTrace, w);

                if (ex.InnerException != null)
                {
                    Log("Inner Exceptions:", w);
                    Exception innerEx = ex;
                    while ((innerEx = innerEx.InnerException) != null)
                    {
                        Log(innerEx.Message, w);
                    }

                }
            }
        }

        public static void Log(string p, StreamWriter w)
        {
            w.WriteLine("{0} - {1}", DateTime.Now, p);
        }
    }
}