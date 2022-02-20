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
    public Holdable currHolded;
    public Interactable currTarget;
    [Header("Height")]
    public float height;
    public float liftHeight;
    public float defaultHeight;
    public float heightLerp;
    [Space]
    Vector2 currHoldOffset;
    public LayerMask holdableLayer;
    public LayerMask interactableStaticLayer;

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
            if (currHolded is HumanHead)
            {
                // ((HumanHead)currHolded).human.transform.position = currHoldOffset + (Vector2)transform.position + Vector2.up * ((HumanHead)currHolded).holdOffset;
                ((HumanHead)currHolded).human.transform.position = (Vector2)transform.position + currHoldOffset;
            }
            else
            {
                currHolded.transform.position = (Vector2)transform.position + currHoldOffset;
            }
        }
    }

    private void UpdateVisuals()
    {
        float targetHeight = (currHolded ? liftHeight : defaultHeight);
        height = Mathf.Lerp(height, targetHeight, Time.deltaTime * heightLerp);

        if (currHolded)
            currHolded.UpdateHeight(this);

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

    Interactable SearchForTarget(LayerMask layer)
    {
        Collider2D[] cols = Physics2D.OverlapPointAll(transform.position, layer);

        foreach (Collider2D c in cols)
        {
            Interactable h = c.GetComponent<Interactable>();
            if (h != currHolded)
            {
                return h;
            }
        }

        return null;
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

        currTarget = SearchForTarget(holdableLayer);
        if (!currTarget)
            currTarget = SearchForTarget(interactableStaticLayer);

        bool isKey = Input.GetKey(KeyCode.Mouse0);
        bool isKeyDown = Input.GetKeyDown(KeyCode.Mouse0);

        // If item under is a holdable, don't highlight it.
        if (currHolded && currTarget && currTarget is Holdable)
            currTarget = null;

        if (isKeyDown && !currHolded && currTarget)
        {
            // Picking up
            if (currTarget is Holdable)
            {
                currHolded = (Holdable)currTarget;
                currHoldOffset = currHolded.transform.position - transform.position;
                currHolded.OnPickedUp(this);
                currTarget = null;
            }
            // Clicking
            else if (currTarget is Clickable)
            {
                ((Clickable)currTarget).OnClick();
            }
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
            currHolded.OnDroppedDown(this);
            currHolded = null;
        }

        // Update highlighting
        if (currTarget)
        {
            currTarget.isHolded = false;

            if (currTarget is Droppable)
            {
                if (currHolded)
                    currTarget.isColliding = ((Droppable)currTarget).IsValidItem(currHolded);
            }
            else
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
