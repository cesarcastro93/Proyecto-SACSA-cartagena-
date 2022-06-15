using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SACSA.Controller;

namespace SACSA.Resourses
{
    public static class Globales
    {
        public static readonly NLog.Logger Logger= NLog.LogManager.GetCurrentClassLogger();

        public static void InicializeLog() 
        {
            Logger.Info("Iniciaizando Virtual-Taxi Cartagena");
        }
    }


}
