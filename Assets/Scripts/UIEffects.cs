using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEffects : MonoBehaviour
{
    [SerializeField] private Color _changeColor;
    [SerializeField] private Sprite _changeSprite;
    [SerializeField] private GameObject _target;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Image _image;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

  
    void Update()
    {
        
    }

    public void ChangeColor()
    {
        _image.color = _changeColor;
    }

    public void ChangeSprite()
    {
        _spriteRenderer.sprite = _changeSprite;
    }
}
