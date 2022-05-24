using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class OnToggleChanged: UnityEvent<bool> { }
public class SettingsToggle : MonoBehaviour, ISettingsValueChanger
{
    [SerializeField] private OnToggleChanged _toggleChanged = new OnToggleChanged();
    [SerializeField] private Toggle _toggle;
    public GameObject Changer { get => gameObject; }

    public void SetValue()
    {
        _toggleChanged?.Invoke(_toggle.isOn);
        Core.settings.SaveSettings();
    }
}
