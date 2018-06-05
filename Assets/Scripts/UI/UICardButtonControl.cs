using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardButtonControl : MonoBehaviour {
    public List<UIDragButton> _CardButtons;
    public Image _NextCardImage;

    private int lastSelectedIndex;
    private List<Sprite> cardPools = new List<Sprite>();
    string[] cards = { "archers", "arrows", "baby-dragon", "balloon", "bandit", "barbarian-hut", "barbarians", "bomb-tower"};
    int completeCount;

	void Start () 
    {
        InitCardButtonList();

        InitCardPools();
    }

	void InitCardPools()
    {
        
    }

    void InitCardButtonList() 
    {
        for (int i = 0; i < _CardButtons.Count; i++)
        {
            UIDragButton item = _CardButtons[i] as UIDragButton;
            item.index = i;
            item.cardSelectedEvent.AddListener(OnNoticeCardButtonSelected);
            item.cardPlacedEvent.AddListener(OnNoticeCardPlaced);
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

    void OnNoticeCardPlaced(int index)
    {
        UIDragButton item = _CardButtons[index] as UIDragButton;
        if (item != null)
        {
            item.ChangeButtonImage(_NextCardImage.sprite);
            PreviewNextCard();
        }
    }

    void PreviewNextCard()
    {
        
    }

    void LoadCardsCompleted()
    {
       
    }


}
