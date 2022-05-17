using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StatusBarRenderer : MonoBehaviour
{
    [SerializeField] private GameObject _stringNoErrors;
    [SerializeField] private GameObject _stringErrors;
    [SerializeField] private GameObject _solveString;
    [SerializeField] private TMPro.TMP_Text _errCountText;
    [SerializeField] private TMPro.TMP_Text _solveText;

    void Start()
    {
        ShowNoErrors();
    }

    void Update()
    {
        
    }

    public void ShowErrors(int errCount)
    {
        Debug.Log("ShowErrors");
        _stringErrors.SetActive(true);
        _stringNoErrors.SetActive(false);
        _errCountText.text = errCount.ToString();
    }

    public void ShowNoErrors()
    {
        Debug.Log("ShowNoErrors");
        _stringErrors.SetActive(false);
        _stringNoErrors.SetActive(true);
        HideSolveText();
    }

    public void ShowSolveText(string text)
    {
        _solveString.SetActive(true);
        _solveText.text = text;
    }

    public void HideSolveText()
    {
        _solveString.SetActive(false);
    }
}
