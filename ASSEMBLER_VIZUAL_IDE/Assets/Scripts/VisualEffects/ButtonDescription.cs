using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string _description;
    [SerializeField] private TMPro.TMP_Text _textBox;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _textBox.text = _description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _textBox.text = string.Empty;
    }

    void Start()
    {
        _textBox = GameObject.FindGameObjectWithTag("Description").GetComponent<TMPro.TMP_Text>();
    }
}
