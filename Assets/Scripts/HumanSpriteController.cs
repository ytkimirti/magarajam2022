using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpriteController : MonoBehaviour
{
    public Vector2 movementDirection;
    public Vector2 lookDirection;
    public bool facing;

    [Header("State")]
    public bool isAlarmed = false;
    public bool isMoving = false;
    public bool isHolded = false;

    [Space]

    public float lookMagnitude = 0.1f;

    [Space]

    public bool isBlinking;
    public float blinkWaitTime;
    public float blinkWaitRandomness;
    public float blinkTime;
    float blinkTimer;


    [Header("References")]

    public Transform headTrans;
    public Transform faceTrans;
    public Transform bodyTrans;
    public Transform handrTrans;
    public Transform handlTrans;
    [Space]
    public SpriteRenderer faceRen;
    public SpriteRenderer headRen;
    [Space]
    public Sprite faceSprite;
    public Sprite blinkingSprite;
    [Space]
    public Animator animator;

    void Start()
    {

    }

    void Update()
    {
        transform.localScale = new Vector3(facing ? 1 : -1, 1, 1);
        lookDirection = (CameraController.main.mousePos - (Vector2)transform.position).normalized;

        // Staring

        Vector2 newFacePos = lookDirection * lookMagnitude;
        newFacePos.x *= facing ? 1 : -1;
        faceTrans.localPosition = newFacePos;

        // Blinking

        blinkTimer -= Time.deltaTime;

        if (blinkTimer <= 0)
        {
            if (isBlinking)
            {
                isBlinking = false;
                blinkTimer = Random.Range(blinkWaitTime - blinkWaitRandomness, blinkWaitTime + blinkWaitRandomness);
                faceRen.sprite = faceSprite;
            }
            else
            {
                isBlinking = true;
                blinkTimer = blinkTime;
                faceRen.sprite = blinkingSprite;
            }
        }

        // Update animation state

        animator.SetBool("isAlarmed", isAlarmed);
        animator.SetBool("isHolded", isHolded);
        animator.SetBool("isMoving", isMoving);
    }
}
