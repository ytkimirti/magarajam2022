using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassAnimator : MonoBehaviour
{
    public float timeOffset;
    public float speed;
    Vector2 defPos;

    void Start()
    {
        defPos = transform.position;
    }

    void Update()
    {
        // The remainder can be 0 - 1 or 1.99
        // If it's bigger than 1, it's half the time
        float diff = (((Time.time + timeOffset) * speed) % 2f) > 1f ? 0f : 0.1f;
        transform.position = defPos + Vector2.right * diff;
    }
}
