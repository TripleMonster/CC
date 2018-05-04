using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardButtonControl : MonoBehaviour {
    public List<UIDragButton> _CardButtons;
    public Image _NextCardImage;

    private int lastSelectedIndex;
    private List<Image> cardPools;

	void Start () {
        InitCardButtonList();
        //SpriteManager.Instance.loadSuc.AddListener(PreviewNextCard);
        //StartCoroutine(SpriteManager.Instance.LoadSpriteFromLocalPath());
        PreviewNextCard();
    }

    void Update () {
		
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
        Debug.Log("PreviewNextCard---------------1");
        //Sprite sprite = SpriteManager.Instance.GetSprite();

        //if (sprite != null)
        //{
        //    Debug.Log("PreviewNextCard---------------2");
        //    _NextCardImage.sprite = sprite;    
        //}

        Sprite sprite = SpriteManager.Instance.TestSprite();
        if (sprite != null)
        {
            _NextCardImage.sprite = sprite;
        }
    }
}
