using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum HandState
{
    holdable,
    holding,
    clickable,
    cursor
}

public class Hand : MonoBehaviour
{
    public HandState state;
    public float height;
    public Holdable currHolded;
    public Interactable currTarget;
    [Space]
    public float liftHeight;
    // public float heightLerp;
    [Space]
    public LayerMask holdableLayer;

    [Header("References")]
    public Sprite holdableSprite;
    public Sprite holdingSprite;
    public Sprite clickableSprite;
    public Sprite cursorSprite;
    public SpriteRenderer handSpriteRen;
    public SpriteRenderer handShadowSpriteRen;

    public static Hand main;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {

    }


    void LateUpdate()
    {
        UpdateInteractables();

        UpdateVisuals();

        transform.position = CameraController.main.mousePos;

        if (currHolded)
        {
            currHolded.transform.position = transform.position;
        }
    }

    private void UpdateVisuals()
    {
        Sprite targetSprite = state switch
        {
            HandState.holdable => holdableSprite,
            HandState.holding => holdingSprite,
            HandState.clickable => clickableSprite,
            HandState.cursor => cursorSprite,
            _ => null
        };

        handSpriteRen.sprite = targetSprite;
        handShadowSpriteRen.sprite = targetSprite;

        handShadowSpriteRen.transform.localPosition = Vector3.down * height;
    }

    private void UpdateInteractables()
    {
        if (currTarget)
        {
            currTarget.isHolded = false;
            currTarget.isColliding = false;
        }

        if (currHolded)
        {
            currHolded.isHolded = false;
            currHolded.isColliding = false;
        }

        Collider2D[] cols = Physics2D.OverlapPointAll(transform.position, holdableLayer);

        currTarget = null;

        foreach (Collider2D c in cols)
        {
            Interactable h = c.GetComponent<Interactable>();
            if (h != currHolded)
            {
                currTarget = h;
                break;
            }
        }

        bool isKey = Input.GetKey(KeyCode.Mouse0);
        bool isKeyDown = Input.GetKeyDown(KeyCode.Mouse0);

        // If item under is a holdable, don't highlight it.
        if (currHolded && currTarget && currTarget is Holdable)
            currTarget = null;

        // If hand empty, pick one up.
        if (isKeyDown && !currHolded && currTarget && currTarget is Holdable)
        {
            currHolded = (Holdable)currTarget;
            currTarget = null;
        }

        // If hand full, drop it
        if (currHolded && !isKey)
        {
            // If there is a droppable underneath
            if (currTarget is Droppable)
            {
                Droppable d = (Droppable)currTarget;

                d.OnDrop(currHolded as Holdable);
            }
            currHolded = null;
        }

        // Update state
        if (currTarget)
        {
            currTarget.isHolded = false;
            currTarget.isColliding = true;
        }

        if (currHolded)
        {
            currHolded.isHolded = true;
            currHolded.isColliding = true;
        }

        if (currHolded)
            state = HandState.holding;
        else if (currTarget)
        {
            if (currTarget is Clickable)
                state = HandState.clickable;
            else if (currTarget is Holdable)
                state = HandState.holdable;
        }
        else
            state = HandState.cursor;
    }
}
