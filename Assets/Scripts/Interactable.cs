using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interactable : MonoBehaviour
{
    public bool isColliding;
    public bool isHolded;

    public SpriteRenderer[] highlightSprites;
    int[] defaultLayers;
    public bool changeLayers;

    void Start()
    {
        defaultLayers = highlightSprites.Select(x => x.sortingOrder).ToArray();
    }

    void Update()
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

        GetComponent<SpriteRenderer>().color = isColliding ? (isHolded ? Color.green : Color.yellow) : Color.white;
    }

}
