using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectUI : MonoBehaviour
{
    public Transform playerFace;
    private Canvas[] enemyCanvases;
    private Camera _cam;

    void Start()
    {
        _cam = Camera.main;
        enemyCanvases = FindObjectsOfType<Canvas>();
    }

    void Update()
    {
        RotateEnemyCanvasesTowardsPlayer();
    }

    void RotateEnemyCanvasesTowardsPlayer()
    {
        foreach (Canvas canvas in enemyCanvases)
        {
            if (canvas.CompareTag("EnemyCanvas"))
            {
                canvas.transform.rotation =
                    Quaternion.LookRotation(canvas.transform.position - _cam.transform.position);

            }
        }
    }
}
