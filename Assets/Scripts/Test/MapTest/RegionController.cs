using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventUtils;
using SpriteGlow;

public class RegionController : MonoBehaviour
{
    public int _ID;
    public UEvent_i _SelectedEvent = new UEvent_i();
    public SpriteGlowEffect _SelectedEffect;
    [HideInInspector] public bool isSelected;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnMouseDown() 
    {
        if (isSelected)
            return;


        ShowSetectedEffect();

        if (_SelectedEvent != null)
            _SelectedEvent.Invoke(_ID);
    }

    void ShowSetectedEffect()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);

        _SelectedEffect.GlowBrightness = 4;
        _SelectedEffect.OutlineWidth = 1;
        _SelectedEffect.AlphaThreshold = 0.1f;
    }

    void HideSelectedEffect()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        _SelectedEffect.GlowBrightness = 1;
        _SelectedEffect.OutlineWidth = 0;
        _SelectedEffect.AlphaThreshold = 0;
    }

    public void LostSelectedState()
    {
        Debug.Log("失去选中效果的区域为 : " + _ID);
        HideSelectedEffect();
        isSelected = false;
    }
}
