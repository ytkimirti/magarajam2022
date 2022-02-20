using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable : Interactable
{
    public virtual bool IsValidItem(Holdable item)
    {
        return true;
    }

    public virtual void OnDrop(Holdable item)
    {
        print($"Item {item.name} dropped into {name}");

        if (!IsValidItem(item))
        {
            print("Item rejected");
            return;
        }
    }
}
