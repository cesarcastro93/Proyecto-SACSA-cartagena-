using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACSA.Resourses
{
    public class Shutdown
    {
        public void ResetSystem()
        {
            Process proceso = new Process();
            //proceso.StartInfo.FileName = "./ArchivosBat/Reset-PC.bat";
            proceso.StartInfo.FileName = "C:/Virtual/ArchivosBat/Reset-PC.bat";
            proceso.Start();
        }

        public void TurnOff()
        {
            Process proceso = new Process();
            //proceso.StartInfo.FileName = "./ArchivosBat/Reset.bat";
            proceso.StartInfo.FileName = "C:/Virtual/ArchivosBat/TurnOff-PC.bat";
            proceso.Start();
        }
    }
}
