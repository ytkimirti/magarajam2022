using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpriteController : MonoBehaviour
{
    public Vector2 movementDirection;
    public Vector2 lookDirection;

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

    void Start()
    {

    }

    void Update()
    {
        lookDirection = (CameraController.main.mousePos - (Vector2)transform.position).normalized;

        // Staring

        faceTrans.localPosition = lookDirection * lookMagnitude;

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
    }
}
