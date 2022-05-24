using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Color Theme", menuName = "Color Theme", order = 51)]
public class ColorTheme: ScriptableObject
{
    [SerializeField] private Color _mainCameraBg;
    [SerializeField] private Color _topMenu;
    [SerializeField] private Color _procBg;
    [SerializeField] private Color _codePanel;
    [SerializeField] private Color _descriptionPanel;
    [SerializeField] private Color _bottomSmallPanel;

    public Color MainCameraBg { get => _mainCameraBg; set => _mainCameraBg = value; }
    public Color TopMenu { get => _topMenu; set => _topMenu = value; }
    public Color ProcBg { get => _procBg; set => _procBg = value; }
    public Color CodePanel { get => _codePanel; set => _codePanel = value; }
    public Color DescriptionPanel { get => _descriptionPanel; set => _descriptionPanel = value; }
    public Color BottomSmallPanel { get => _bottomSmallPanel; set => _bottomSmallPanel = value; }
}
