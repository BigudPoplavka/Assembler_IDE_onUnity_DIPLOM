using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;



[System.Serializable]
public class ConsoleModule : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _consoleField;
  //  [SerializeField] public LogEvent logEvent = new LogEvent();


    public TMPro.TMP_Text GetLogField()
    {
        return _consoleField;
    }
}