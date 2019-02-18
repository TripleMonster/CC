using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragArrow : MonoBehaviour
{
    public Sprite _ArrowHead;
    public Sprite _ArrowLine;

    GameObject arrowObject;

    public void CreateArrowHead()
    {
        arrowObject = new GameObject("Army");
        arrowObject.transform.SetParent(transform, false);
        arrowObject.transform.SetAsLastSibling();
        arrowObject.transform.localScale = new Vector3(3, 3, 0);

        var spRenderer = arrowObject.AddComponent<SpriteRenderer>();
        spRenderer.sprite = _ArrowHead;
        spRenderer.color = new Color32(35, 183, 70, 255);
    }

    public void BeginDrag(Vector3 beginPosition)
    {
        CreateArrowHead();
    }

    public void OnDraging(float distance, float angle)
    {

    }

    public void EndDrag()
    {

    }
}
