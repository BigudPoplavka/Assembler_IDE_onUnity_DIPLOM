using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class LoadingWindow : MonoBehaviour
{
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private float _scale;

    void Start()
    {
        _loadingBar.value = 0;
    }
    void Update()
    {
        _loadingBar.value += _scale * Time.deltaTime;
        if(_loadingBar.value == 1)
        {
            Application.LoadLevel(1);
        }
    }
}
