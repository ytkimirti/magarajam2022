using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [Header("State")]
    public bool isHolded;
    public bool isAlarmed;
    public bool facing;
    [Space]
    public Vector2 currTarget;
    float currDistToTarget;

    public Vector2 randomLocTime;
    public float randomLocTimer;

    [Header("Movement")]

    public bool isMoving;
    public float normalSpeed;
    public float alarmedSpeed;
    public float movementSmoothing;
    Vector2 smoothVel;

    [Header("References")]
    public Collider2D col;
    public HumanSpriteController spriteController;
    public Rigidbody2D rb;

    private void OnDestroy()
    {
        GameManager.main.humans.Remove(this);
    }

    void Start()
    {
        GameManager.main.humans.Add(this);
    }

    private void OnDrawGizmos()
    {
        if (!isMoving)
            return;
        Gizmos.color = isAlarmed ? Color.red : Color.cyan;
        Gizmos.DrawWireSphere(currTarget, 0.3f);
        Gizmos.DrawLine(transform.position, currTarget);
    }

    private void Update()
    {
        UpdateTargetPos();

        // Update animation
        spriteController.isHolded = isHolded;
        spriteController.isAlarmed = isAlarmed;
        spriteController.isMoving = isMoving;
    }

    void UpdateTargetPos()
    {
        col.enabled = !isHolded;
        if (isHolded)
            return;

        currDistToTarget = Vector2.Distance(transform.position, currTarget);
        isMoving = currDistToTarget > 0.4f;

        if (!isMoving)
        {
            randomLocTimer -= Time.deltaTime;
            if (isAlarmed)
                randomLocTimer = 0;
        }

        if (randomLocTimer <= 0)
        {
            randomLocTimer = Random.Range(randomLocTime.x, randomLocTime.y);
            currTarget = GameManager.main.findTargetPos();
        }
    }

    void FixedUpdate()
    {
        if (isHolded)
        {
            isMoving = false;
            spriteController.movementDirection = Vector2.zero;
            return;
        }

        Vector2 input = Vector2.zero;

        if (isMoving)
            input = (currTarget - (Vector2)transform.position);

        if (GameManager.main.controlHumans)
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.x != 0)
            facing = input.x > 0;

        spriteController.facing = facing;

        float newSpeed = isAlarmed ? alarmedSpeed : normalSpeed;

        input.Normalize();

        Vector2 targetVel = input * newSpeed;

        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVel, ref smoothVel, movementSmoothing);

        spriteController.movementDirection = rb.velocity.normalized;
    }

}

