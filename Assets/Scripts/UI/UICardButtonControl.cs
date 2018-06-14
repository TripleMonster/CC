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
    int completeCount;
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

    void OnDripSliderChanged(float value)
    {
        float nValue = _dripSlider.realValue * 10;
        _dripNumberText.text = Mathf.FloorToInt(nValue).ToString();
        currentDripCount = Mathf.FloorToInt(nValue);
    }
}
