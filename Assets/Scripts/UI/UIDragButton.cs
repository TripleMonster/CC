using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using DG.Tweening;
using UnityEventUtils;

[RequireComponent(typeof(Image))]
public class UIDragButton : UIButton, IBeginDragHandler, IDragHandler, IEndDragHandler 
{

    public GameObject _HeroPrafab;

    [HideInInspector] public UEvent_i cardSelectedEvent = new UEvent_i();
    public CardSelectedState currentCardSelectedState{ get; private set; }
    public int index { get; set; }

    enum DragingPhase { SCALE_CARD, SCALE_HERO, DRAG_HERO }
    private GameObject dragingHero;
    private HeroControl heroControl;
    private RectTransform cardImageRT;
    private Vector3 originDragingImagePos;

    private DragingPhase currentDragingPhase;

	private void Start()
    {
        cardImageRT = GetComponent<Image>().rectTransform;
        currentCardSelectedState = CardSelectedState.NORMAL;
	}

    void CreateHeroInstance(Vector3 position) 
    {
        dragingHero = Instantiate<GameObject>(_HeroPrafab);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(dragingHero.transform.position);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, screenPos.z));
        dragingHero.transform.position = new Vector3(worldPos.x, 0, worldPos.z);
        //dragingHero.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        dragingHero.SetActive(false);
    }

	public void OnBeginDrag(PointerEventData eventData) 
    {
        originDragingImagePos = cardImageRT.transform.position;

        CreateHeroInstance(eventData.position);

        currentDragingPhase = DragingPhase.SCALE_CARD;
        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData) 
    {
        if (dragingHero != null)
            SetDraggedPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragingHero != null)
        {
            RaycastHit hit; 
            if (CheckIsPlaced(out hit))
            {
                heroControl = dragingHero.GetComponent<HeroControl>();
                heroControl.Init();
            }
            else
            {
                Destroy(dragingHero);
            }
        }

        cardImageRT.localScale = new Vector3(1f, 1f, 1f);
        cardImageRT.transform.position = originDragingImagePos;

        dragingHero = null;
        heroControl = null;
        currentDragingPhase = DragingPhase.SCALE_CARD;
    }

    private void SetDraggedPosition(PointerEventData eventData) 
    {
        if (currentDragingPhase == DragingPhase.SCALE_CARD)
        {
            HandleScaleCardPhase(eventData.position);
        }
        else if (currentDragingPhase == DragingPhase.SCALE_HERO)
        {
            HandleScaleHeroPhase(eventData.position);
        }
        else if (currentDragingPhase == DragingPhase.DRAG_HERO)
        {
            HandleDragHeroPhase(eventData.position);
        }
    }

    void HandleScaleCardPhase(Vector3 pos) 
    {
        Sequence dragCardSequence = DOTween.Sequence();
        dragCardSequence.Append(cardImageRT.DOScale(cardImageRT.localScale - new Vector3(0.1f, 0.1f, 0.1f), 0))
                        .Join(cardImageRT.DOMove(pos, 0))
                        .AppendCallback(() =>
                        {
                            if (cardImageRT.localScale.x < 0.1f)
                            {
                                currentDragingPhase = DragingPhase.SCALE_HERO;
                            }
                        });
    }

    void HandleScaleHeroPhase(Vector3 pos) 
    {
        dragingHero.SetActive(true);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(dragingHero.transform.position);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, screenPos.z));

        Sequence dragHeroSequence = DOTween.Sequence();
        dragHeroSequence.Append(dragingHero.transform.DOScale(dragingHero.transform.localScale + new Vector3(0.1f, 0.1f, 0.1f), 0))
                        .Join(dragingHero.transform.DOMove(new Vector3(worldPos.x, 0, worldPos.z), 0))
                        .AppendCallback(() =>
                        {
                            if (dragingHero.transform.localScale.x >= 1.0f)
                                currentDragingPhase = DragingPhase.DRAG_HERO;
                        });   
    }

    void HandleDragHeroPhase(Vector3 pos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(dragingHero.transform.position);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, screenPos.z));
        dragingHero.transform.position = new Vector3(worldPos.x, 0, worldPos.z);
    }

    bool CheckIsPlaced (out RaycastHit hit) 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Plane"))) 
        {
            return true;
        }
        return false;
    }

    void DoCardSelectedAction() 
    {
        Sequence selectedSequence = DOTween.Sequence();
        selectedSequence.Append(cardImageRT.DOScale(1.2f, 0.05f))
                        .Append(cardImageRT.DOScale(1.3f, 0.05f))
                        .Append(cardImageRT.DOScale(1.2f, 0.05f))
                        .Join(cardImageRT.DOMoveY(cardImageRT.position.y + 20f, 0.05f))
                        .AppendCallback(() => 
                        {
                            cardSelectedEvent.Invoke(index);
                            currentCardSelectedState = CardSelectedState.SELECTED;
                        });
    }

    public void DoCardExitSelectedAction() 
    {
        Sequence exitSelectedSequence = DOTween.Sequence();
        exitSelectedSequence.Append(cardImageRT.DOScale(1f, 0.05f))
                            .Join(cardImageRT.DOMoveY(cardImageRT.position.y - 20f, 0.05f))
                            .AppendCallback(()=>
                            {
                                currentCardSelectedState = CardSelectedState.NORMAL;
                            });
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (currentCardSelectedState == CardSelectedState.NORMAL) 
        {
            DoCardSelectedAction();
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
    }
}
