using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class InstructionRenderer : MonoBehaviour
{
    [SerializeField] private GameObject _template;

    [SerializeField] private GameObject _ax;
    [SerializeField] private GameObject _bx;
    [SerializeField] private GameObject _cx;
    [SerializeField] private GameObject _dx;
    [SerializeField] private GameObject _si;
    [SerializeField] private GameObject _di;
    [SerializeField] private GameObject _sp;
    [SerializeField] private GameObject _bp;
    [SerializeField] private GameObject _ip;
    [SerializeField] private Color _panelNormalColor;
    [SerializeField] private Color _panelActiveColor;

    [SerializeField] private TMPro.TMP_Text[] _ax_val;
    [SerializeField] private TMPro.TMP_Text[] _bx_val;
    [SerializeField] private TMPro.TMP_Text[] _cx_val;
    [SerializeField] private TMPro.TMP_Text[] _dx_val;
    [SerializeField] private TMPro.TMP_Text[] _si_val;
    [SerializeField] private TMPro.TMP_Text[] _di_val;
    [SerializeField] private TMPro.TMP_Text[] _sp_val;
    [SerializeField] private TMPro.TMP_Text[] _bp_val;
    [SerializeField] private TMPro.TMP_Text[] _ip_val;

    private Dictionary<GameObject, TMPro.TMP_Text[]> _regPanelAndValuesText;

    void Start()
    {
        _panelNormalColor = _ax.GetComponent<Image>().color;

        _regPanelAndValuesText = new Dictionary<GameObject, TMPro.TMP_Text[]>();
    }

    void Update()
    {
        
    }

    public void RenderInstruction(Instruction instruction)
    {

    }
}
