using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _currShowingPanel;
    [SerializeField] private GameObject _defaultTab;

    void Start()
    {
        _currShowingPanel = _defaultTab;
    }

    public void ShowPanel(GameObject panel)
    {
        _currShowingPanel.SetActive(false);
        _currShowingPanel = panel;
        panel.SetActive(true);
    }

    public void ShowSettings()
    {
        _settingsPanel.SetActive(!_settingsPanel.activeSelf);
    }

    public void CloseSetting()
    {
        _settingsPanel.SetActive(false);
    }
}
