using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[System.Serializable]
public class LogEvent : UnityEvent<string> { }

[System.Serializable]
public class ProcComponentsInitilizer: MonoBehaviour
{
    private void Start()
    {
        Core.InitCore();
        Core.GetConsole();
        Core.GetStateBars();
    }

    public void SaveSettings()
    {
        Core.settings.SaveSettings();
    }

    public void SaveSettingsToFile()
    {
        Core.settings.WriteSettingsToFile();
    }
}
