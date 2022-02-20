using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Holdable : Interactable
{
    [ReadOnly]
    public HoldableSlot currSlot;
    public Transform shadowTrans;

    public virtual void UpdateHeight(Hand hand)
    {
        if (hand && isHolded && shadowTrans)
        {
            shadowTrans.transform.localPosition = Vector3.down * hand.height;
        }
    }

    public virtual void OnPickedUp(Hand hand)
    {
        if (currSlot)
        {
            currSlot.OnRelease(this);
            currSlot = null;
        }
    }

    public virtual void OnDroppedDown(Hand hand)
    {
        if (shadowTrans)
            shadowTrans.transform.localPosition = Vector3.down * 0.1f;
    }
}
