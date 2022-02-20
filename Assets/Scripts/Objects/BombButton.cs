using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombButton : Clickable
{
    public Sprite buttonRed;
    public Sprite buttonRedPress;
    public Sprite buttonYellow;
    public Sprite buttonYellowPress;
    [Space]
    public SpriteRenderer buttonRenderer;
    public bool isYellow = false;
    public float paintTime;

    public override void OnCollide()
    {
        base.OnCollide();
        paintTime -= Time.deltaTime;

        if (paintTime < 0)
        {
            paintTime = 0;
            isYellow = true;
        }
    }

    public override void OnClick()
    {
        base.OnClick();

        Invoke("GetUp", 0.15f);

        buttonRenderer.sprite = isYellow ? buttonYellowPress : buttonRedPress;
    }

    void GetUp()
    {
        buttonRenderer.sprite = isYellow ? buttonYellow : buttonRed;
    }
}
