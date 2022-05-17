using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class CoreDirictories
{
    public static string defaultOpenFilePath = Application.dataPath;
    public static string runFlagPath = Application.dataPath + "/IOConfig.dat";
    public static string IOConfigPath = Application.dataPath + "/RUN.dat";
    public static string lastWorkDirectory;
    public static string settings = Application.dataPath + "/settings.json";
}
