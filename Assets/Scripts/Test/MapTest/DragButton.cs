using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEventUtils;

[RequireComponent(typeof(Image))]
public class DragButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas _Canvas;
    public GameObject _ArmysObj;
    public Sprite _ArmySprite;

    public UEvent_i DropEvent = new UEvent_i();

    private RectTransform mDragImageRT;
    private Vector3 mDragImagePosition;
    private GameObject mDragingObj;
    private Sprite mDragingSprite;

    private void Awake() 
    {
        mDragImageRT = GetComponent<Image>().rectTransform;    
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void GenerateDragingObj(PointerEventData eventData)
    {
        mDragingObj = new GameObject("DragingObj");
        mDragingObj.transform.SetParent(_Canvas.transform, false);
        mDragingObj.transform.SetAsLastSibling();
        var image = mDragingObj.AddComponent<Image>();
        var group = mDragingObj.AddComponent<CanvasGroup>();
        group.blocksRaycasts = false;

        image.sprite = GetComponent<Image>().sprite;
        //image.SetNativeSize();
        
        SetDraggedPosition(eventData);
    }

    void GenerateDragingWorldObj(PointerEventData eventData)
    {
        mDragingObj = new GameObject("Army");
        mDragingObj.transform.SetParent(_ArmysObj.transform, false);
        mDragingObj.transform.SetAsLastSibling();
        mDragingObj.transform.localScale = new Vector3(3, 3, 0);

        var spRenderer = mDragingObj.AddComponent<SpriteRenderer>();
        spRenderer.sprite = _ArmySprite;
        spRenderer.color = new Color32(35, 183, 70, 255);

        SetDraggedPosition2(eventData.position);
    }

    void SetDraggedPosition(PointerEventData eventData)
    {
        var rt = mDragingObj.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_Canvas.transform as RectTransform, eventData.position, _Canvas.worldCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
        }
    }

    void SetDraggedPosition2(Vector3 pos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(mDragingObj.transform.position);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, screenPos.z));
        mDragingObj.transform.position = new Vector3(worldPos.x, worldPos.y, -2);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mDragImagePosition = mDragImageRT.transform.position;

        //GenerateDragingObj(eventData);
        GenerateDragingWorldObj(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // if (mDragingObj != null)
        //     SetDraggedPosition(eventData);

        if (mDragingObj != null)
            SetDraggedPosition2(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (mDragingObj != null)
            DropEvent.Invoke(1);
    }
}
