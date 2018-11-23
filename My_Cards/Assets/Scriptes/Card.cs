using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Camera MainCam;
    Vector3 offset;
    public Transform defaultParent, defaultTempParent;
    GameObject TempCard;
    public bool IsDrag;

    void Awake(){
        TempCard = GameObject.Find("TempCard");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCam.ScreenToWorldPoint(eventData.position);

        defaultParent = defaultTempParent = transform.parent;

        IsDrag = defaultParent.GetComponent<Drop>()._type == FType.MY_HAND;

        if (!IsDrag)
            return;

        TempCard.transform.SetParent(defaultParent);
        TempCard.transform.SetSiblingIndex(transform.GetSiblingIndex());

        transform.SetParent(defaultParent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsDrag)
            return;

        Vector3 newPos = MainCam.ScreenToWorldPoint(eventData.position);   
        newPos.z = 0;
        transform.position = newPos + offset;

        if (TempCard.transform.parent != defaultTempParent)
            TempCard.transform.SetParent(defaultTempParent);

        TempPosition();
    } 
       
    public void OnEndDrag(PointerEventData eventData)
    {   
        if (!IsDrag)
            return;

        transform.SetParent(defaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.SetSiblingIndex(TempCard.transform.GetSiblingIndex());

        TempCard.transform.SetParent(GameObject.Find("Canvas").transform);
        TempCard.transform.localPosition = new Vector3(3210 , 0);

    }

    void TempPosition(){

        int Index = defaultTempParent.childCount;

        for (int i = 0; i < defaultTempParent.childCount; i++)
        {
            if (transform.position.x < defaultTempParent.GetChild(i).position.x)
            {
                Index = i;

                if (TempCard.transform.GetSiblingIndex() < Index)
                    Index--;

                break;
            }
        }

        TempCard.transform.SetSiblingIndex(Index);
    }

    /*void checking(){
        if (!IsDrag)
            return;
    }*/
   
}