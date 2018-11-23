using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FType { MY_FIELD, MY_HAND, ENEMY_FIELD, ENEMY_HAND };  

public class Drop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public FType _type;

    public void OnDrop (PointerEventData eventData)
    {
        if (_type != FType.MY_FIELD)
            return;

        Card ThisCard = eventData.pointerDrag.GetComponent<Card>();

        if (ThisCard)
            ThisCard.defaultParent = transform;  
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_type != FType.MY_FIELD) 
            return;

        if (eventData.pointerDrag == null)
            return;

        Card ThisCard = eventData.pointerDrag.GetComponent<Card>(); 

        if (ThisCard)
            ThisCard.defaultTempParent = transform;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Card ThisCard = eventData.pointerDrag.GetComponent<Card>();

        if (ThisCard && ThisCard.defaultTempParent == transform)
            ThisCard.defaultTempParent = ThisCard.defaultParent;

    }

}
