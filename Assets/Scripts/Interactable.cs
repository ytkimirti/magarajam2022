using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isColliding;
    public bool isHolded;

    void Start()
    {

    }

    void Update()
    {
        GetComponent<SpriteRenderer>().color = isColliding ? (isHolded ? Color.green : Color.yellow) : Color.white;
    }

}
