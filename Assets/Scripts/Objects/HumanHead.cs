using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanHead : Holdable
{
    public Human human;
    public float holdOffset;

    protected override void Update()
    {
        base.Update();

        human.isHolded = isHolded;
    }
}
