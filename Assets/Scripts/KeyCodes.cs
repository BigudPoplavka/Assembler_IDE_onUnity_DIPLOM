using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class KeyCodes: MonoBehaviour
{
    [SerializeField] private Dictionary<KeyCode, KeyCode> keyCodesPairs;
    [SerializeField] private Dictionary<(KeyCode, KeyCode), Action> hotKeysActionsPairs;

    [SerializeField] private UnityEvent _ctrlPlus, _ctrlMin;
    [SerializeField] private UnityEvent _ctrlA, _ctrlD, _ctrlS, _ctrlN, _ctrlB, _ctrlO;
    private UnityEvent[] _hotKeysEvents;

    public void Start()
    {
        keyCodesPairs = new Dictionary<KeyCode, KeyCode>();
        hotKeysActionsPairs = new Dictionary<(KeyCode, KeyCode), Action>();

        InitDictWithEvents();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                CtrlO();
        if (Input.GetKeyDown(KeyCode.S))
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                CtrlS();
        if (Input.GetKeyDown(KeyCode.B))
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                CtrlB();

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.O))
                CtrlO();
            if (Input.GetKeyDown(KeyCode.S))
                CtrlS();
            if (Input.GetKeyDown(KeyCode.B))
                CtrlB();
        }

    }

    public void InitDictWithEvents()
    {
        _hotKeysEvents = new UnityEvent[] { _ctrlPlus, _ctrlMin, _ctrlA, _ctrlD, _ctrlS, _ctrlN, _ctrlB, _ctrlO };
        for (int i = 0; i < _hotKeysEvents.Length; i++)
            _hotKeysEvents[i] = new UnityEvent();

        hotKeysActionsPairs.Add((KeyCode.LeftControl, KeyCode.Plus), delegate { CtrlPlus(); });
        hotKeysActionsPairs.Add((KeyCode.LeftControl, KeyCode.Minus), delegate { CtrlMinus(); });
        hotKeysActionsPairs.Add((KeyCode.LeftControl, KeyCode.A), delegate { CtrlA(); });
        hotKeysActionsPairs.Add((KeyCode.LeftControl, KeyCode.D), delegate { CtrlD(); });
        hotKeysActionsPairs.Add((KeyCode.LeftControl, KeyCode.S), delegate { CtrlS(); });
        hotKeysActionsPairs.Add((KeyCode.LeftControl, KeyCode.N), delegate { CtrlN(); });
        hotKeysActionsPairs.Add((KeyCode.LeftControl, KeyCode.B), delegate { CtrlB(); });
        hotKeysActionsPairs.Add((KeyCode.LeftControl, KeyCode.O), delegate { CtrlO(); });
    }

    public void ApplyHotKeyAction()
    {
        
    }

    public void CtrlPlus()
    {
        Debug.Log("Ctrl+");
        _ctrlPlus?.Invoke();
    }

    public void CtrlMinus()
    {
        Debug.Log("Ctrl-");
        _ctrlMin?.Invoke();
    }

    public void CtrlA()
    {

    }
    public void CtrlD()
    {

    }
    public void CtrlS()
    {
        _ctrlS?.Invoke();
    }
    public void CtrlN()
    {

    }
    public void CtrlB()
    {

    }
    public void CtrlO()
    {

    }
}
