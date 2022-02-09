using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

public class MouseHoverBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _objectToShow;
    [SerializeField] private bool _isAutonomicObject;
    [SerializeField] private bool _isOnlyHiddenObject;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _objectToShow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(_isOnlyHiddenObject == true)
            _objectToShow.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
