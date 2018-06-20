using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardButtonControl : MonoBehaviour {
    [SerializeField] private List<UIDragButton> _CardButtons;
    [SerializeField] private Image _NextCardImage;
    [SerializeField] private TTSectionSlider _dripSlider;
    [SerializeField] private Text _dripNumberText;

    private int lastSelectedIndex;
    int currentDripCount;

	void Start () 
    {
        InitDrip();
        InitCardButtonList();
    }

    void InitDrip()
    {
        _dripSlider.onRealValueChanged.AddListener(OnDripSliderChanged);
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
            item.SwitchAvailableStatus(false);
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
            UseupHolyWaterByCardName(item.cardName);
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

    void OnDripSliderChanged(float value)
    {
        float nValue = _dripSlider.realValue * 10;
        _dripNumberText.text = Mathf.FloorToInt(nValue).ToString();
        currentDripCount = Mathf.FloorToInt(nValue);
        CheckCardButtonsStatesByDripCount();
    }

    void CheckCardButtonsStatesByDripCount()
    {
        foreach (var item in _CardButtons)
        {
            int cost = DataManager.Instance.GetCardCostByCardName(item.cardName);
            if (currentDripCount < cost)
                item.SwitchAvailableStatus(false);
            else
                item.SwitchAvailableStatus(true);
        }
    }

    void UseupHolyWaterByCardName(string cardName)
    {
        int cost = DataManager.Instance.GetCardCostByCardName(cardName);
        float realCost = cost * 0.1f;
        Debug.Log("name : " + cardName + "; realcost : " + realCost);
        _dripSlider.value -= realCost;
    }
}
