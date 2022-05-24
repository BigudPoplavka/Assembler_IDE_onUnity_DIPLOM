using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CodeString : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Light _err;
    [SerializeField] private TextMeshPro _string;
    [SerializeField] private Transform _diggingParent;
    [SerializeField] private Transform _originalParent;

    public string _tmp = string.Empty;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }
}
