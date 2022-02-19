using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 mousePos;
    public Vector2 sceneSize;
    [Space]
    public Camera cam;

    public static CameraController main;

    private void Awake()
    {
        main = this;
        sceneSize = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    void Start()
    {

    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void LateUpdate()
    {

    }
}
