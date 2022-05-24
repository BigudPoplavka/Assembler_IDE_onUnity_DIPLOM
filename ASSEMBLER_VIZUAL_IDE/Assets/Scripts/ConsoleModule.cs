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
    [SerializeField] private TMPro.TMP_InputField _console;
    [SerializeField] private GameObject _scrollViewContent;
    [SerializeField] private Transform _fieldTransform;
    [SerializeField] private TMPro.TMP_Text _template;
    [SerializeField] private TMPro.TMP_Text _errTemplate;
    [SerializeField] private ConsoleContentManager _contentManager;

    private void Start()
    {
        Core.console = _console;
        Render("CORE: Core initialized");
        Render("CORE: Console initialized");
    }

    public void Render(string line)
    {
        _template.text = line;
        Instantiate(_template, _fieldTransform);
    }

    public void RenderErr(string line)
    {
        _errTemplate.text = line;
        Instantiate(_errTemplate, _fieldTransform);
    }

    public TMPro.TMP_Text GetLogField()
    {
        return _consoleField;
    }

}