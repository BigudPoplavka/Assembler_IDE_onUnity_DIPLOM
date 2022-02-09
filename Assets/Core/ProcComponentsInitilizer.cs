using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LogEvent : UnityEvent<string> { }

public class ProcComponentsInitilizer: MonoBehaviour
{
    private void Start()
    {
        Core.InitCore();
        Core.GetConsole();
    }

    private void Update()
    {
        
    }
}
