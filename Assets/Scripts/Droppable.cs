using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable : Interactable
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void OnDrop(Holdable item)
    {
        print($"Item {item.name} dropped into {name}");
    }
}
