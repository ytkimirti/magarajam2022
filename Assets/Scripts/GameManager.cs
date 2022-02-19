using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("God Mode")]
    public bool godMode;
    public bool controlHumans;

    [Space]

    public List<Human> humans;

    public LayerMask obstacleLayer;
    
    public static GameManager main;

    private void Awake() {
        main = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (godMode)
        {
            if (Input.GetKeyDown(KeyCode.C))
                controlHumans = !controlHumans;
        }
    }

    public Vector2 findTargetPos()
    {
        int tries = 0;
        Vector2 randomPos = Vector2.zero;

        do
        {
            randomPos = new Vector2(
                Random.Range(-CameraController.main.sceneSize.x, CameraController.main.sceneSize.x),
                Random.Range(-CameraController.main.sceneSize.y, CameraController.main.sceneSize.y)
            );

            if (tries++ > 100000)
            {
                Debug.LogError("Too many tries, skipping...");
                break; 
            }
        } while (Physics2D.OverlapCircleAll(randomPos, 0.4f, obstacleLayer).Length > 0);

        return randomPos;
    }
}
