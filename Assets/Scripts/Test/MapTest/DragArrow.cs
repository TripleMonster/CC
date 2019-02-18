using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragArrow : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Sprite _ArrowHead;
    public Sprite _ArrowLine;
    public GameObject _ParentObj;

    GameObject arrowObject;
    Vector3 mBeginPosition;

    public void CreateArrowHead()
    {
        arrowObject = new GameObject("Arrow");
        arrowObject.transform.SetParent(_ParentObj.transform, false);
        arrowObject.transform.SetAsLastSibling();
        arrowObject.transform.localScale = new Vector3(3, 3, 0);

        var spRenderer = arrowObject.AddComponent<SpriteRenderer>();
        spRenderer.sprite = _ArrowHead;
        spRenderer.color = new Color32(35, 183, 70, 255);
    }

    public void BeginDrag(Vector3 beginPosition)
    {
        mBeginPosition = beginPosition;
        CreateArrowHead();
        if (arrowObject != null)
            arrowObject.transform.position = beginPosition;
    }

    public void OnDraging(PointerEventData eventData)
    {
        float distance = Vector2.Distance(mBeginPosition, Input.mousePosition);

        Vector2 v2 = (Input.mousePosition - mBeginPosition).normalized;
        float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;

        if (arrowObject != null)
        {
            Vector3 agv3 = new Vector3(0, 0, 0);
            agv3.z = angle - 90f;
            arrowObject.transform.localRotation = Quaternion.Euler(agv3);
        }
    }

    public void EndDrag()
    {
        // if (arrowObject != null)
        //     Destroy(arrowObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag-------------");
        BeginDrag(transform.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag-------------");
        OnDraging(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag-------------");
        EndDrag();
    }
}
