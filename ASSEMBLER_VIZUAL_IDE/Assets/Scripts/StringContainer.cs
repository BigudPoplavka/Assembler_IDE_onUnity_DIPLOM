using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StringContainer : MonoBehaviour
{
    [SerializeField] private List<CodeString> Strings;
    [SerializeField] private CodeString _StringsTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _draggingParent;

    public UnityEvent ExistedItemAdd;

    public void OnEnable()
    {
        //Render(Strings);
    }

    private void Render(List<CodeString> items)
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
