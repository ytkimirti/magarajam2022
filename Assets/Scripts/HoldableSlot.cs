using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableSlot : Droppable
{
    public Holdable currHoldable;

    public override void OnDrop(Holdable item)
    {
        base.OnDrop(item);
        item.transform.position = transform.position;
        currHoldable = item;
    }

    public virtual void OnRelease(Holdable item)
    {
        if (item == currHoldable)
            currHoldable = null;
    }
}
