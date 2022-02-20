using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;

public class Interactable : MonoBehaviour
{
    [ReadOnly]
    public bool isColliding;
    [ReadOnly]
    public bool isHolded;

    public SpriteRenderer[] highlightSprites;
    int[] defaultLayers;
    public bool changeLayers;

    protected virtual void Start()
    {
        defaultLayers = highlightSprites.Select(x => x.sortingOrder).ToArray();
    }

    protected virtual void Update()
    {
        for (int i = 0; i < highlightSprites.Length; i++)
        {
            SpriteRenderer sprite = highlightSprites[i];

            if (!sprite)
                continue;
            sprite.material = isColliding ? GameManager.main.outlineMaterial : GameManager.main.defaultMaterial;

            if (changeLayers)
                sprite.sortingOrder = defaultLayers[i] + (isHolded ? 10 : 0);
        }

        // GetComponent<SpriteRenderer>().color = isColliding ? (isHolded ? Color.green : Color.yellow) : Color.white;
    }

    public virtual void OnCollide()
    {

    }

}
