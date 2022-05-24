using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegInfo : MonoBehaviour
{
    [SerializeField] private Register _register;
    [SerializeField] private string _preTitle;
    [SerializeField] private TMPro.TMP_Text _title;
    [SerializeField] private TMPro.TMP_Text _mainText;
    [SerializeField] private Image _image; 
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;

    public void ShowInfo()
    {
        _title.text = _preTitle + " " + _register.RegName.ToString().ToUpper();
        _description = Registers.GetRegByName(_register.RegName.ToString()).Description;
        _mainText.text = _description;
        _sprite = _register.DescrImage;

        if (_sprite != null)
        {
            _image.gameObject.SetActive(true);
            _image.sprite = _sprite;
        }
    }
}


