using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;



public class EventsBase
{
    public static UnityEvent onEmptyCode = new UnityEvent();
    public static UnityEvent onDebugStarted = new UnityEvent();
    public static UnityEvent onBuildStarted = new UnityEvent();
    public static UnityEvent onEmulatonStarted = new UnityEvent();
}
