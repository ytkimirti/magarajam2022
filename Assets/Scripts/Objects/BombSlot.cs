using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSlot : HoldableSlot
{
    public override bool IsValidItem(Holdable item)
    {
        if (!(item is Bomb))
            print("Item is NOT bomb");

        return (item is Bomb);
        // return base.IsValidItem(item);
    }
}
