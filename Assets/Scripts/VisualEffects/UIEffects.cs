using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIEffects : MonoBehaviour
{
    [SerializeField] private Color _changeColor;
    [SerializeField] private Sprite _changeSprite;
    [SerializeField] private GameObject _target;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Image _image;

    private float[] _fontSizeBounds = new float[] { 6.0f, 14.0f };
    private float _fontSizeStep = 1f;

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

    public void ChangeButtonInteractMode()
    {
        gameObject.GetComponent<Button>().interactable = !gameObject.GetComponent<Button>().interactable;
    }
}
