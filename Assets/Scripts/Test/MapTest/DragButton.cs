using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class DragButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas _Canvas;

    private RectTransform mDragImageRT;
    private Vector3 mDragImagePosition;
    private GameObject mDragingObj;

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

    void SetDraggedPosition(PointerEventData eventData)
    {
        var rt = mDragingObj.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_Canvas.transform as RectTransform, eventData.position, _Canvas.worldCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mDragImagePosition = mDragImageRT.transform.position;

        GenerateDragingObj(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (mDragingObj != null)
            SetDraggedPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // if (mDragingObj != null)
        //     Destroy(mDragingObj);
        
        // mDragingObj = null;
    }
}
