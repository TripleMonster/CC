using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardButtonControl : MonoBehaviour {
    public List<UIDragButton> _CardButtons;
    public Image _NextCardImage;

    private int lastSelectedIndex;
    int completeCount;

	void Start () 
    {
        InitCardButtonList();
    }

    void InitCardButtonList() 
    {
        for (int i = 0; i < _CardButtons.Count; i++)
        {
            UIDragButton item = _CardButtons[i] as UIDragButton;
            Sprite sprite = DataManager.Instance.getARandomCard();
            item.ChangeButtonImage(sprite);
            item.SetIndexAndCardName(i, sprite.name);
            item.cardSelectedEvent.AddListener(OnNoticeCardButtonSelected);
            item.cardPlacedEvent.AddListener(OnNoticeCardPlaced);
        }
        PreviewNextCard();
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
            Debug.Log("下一张卡牌是 : " + _NextCardImage.sprite.name);
            item.ChangeButtonImage(_NextCardImage.sprite);
            item.SetIndexAndCardName(index, _NextCardImage.sprite.name);
            PreviewNextCard();
        }
    }

    void PreviewNextCard()
    {
        _NextCardImage.sprite = DataManager.Instance.getARandomCard();
    }

    void LoadCardsCompleted()
    {
       
    }
}
