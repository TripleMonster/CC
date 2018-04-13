using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardButtonControl : MonoBehaviour {

    public List<UIDragButton> _CardButtons;
    public Image _NextCardImage;

    private int lastSelectedIndex;

	void Start () {
        InitCardButtonList();
    }

    void Update () {
		
	}

    void InitCardButtonList() 
    {
        for (int i = 0; i < _CardButtons.Count; i++)
        {
            UIDragButton item = _CardButtons[i] as UIDragButton;
            item.index = i;
            item.cardSelectedEvent.AddListener(OnNoticeCardButtonSelected);
        }
    }

    void OnNoticeCardButtonSelected(int index) 
    {
        if (lastSelectedIndex != index) 
        {
            UIDragButton item = _CardButtons[lastSelectedIndex] as UIDragButton;
            if (item.currentCardSelectedState == CardSelectedState.SELECTED)
            {
                item.DoCardExitSelectedAction();
                lastSelectedIndex = index;
            }
        }
    }

    void PreviewNextCard()
    {

    }
}
