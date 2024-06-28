using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using UnityEngine;
using Random = UnityEngine.Random;

public class WatchersManager : MonoBehaviour
{
    private GameObject _player;
    private List<Transform> _watchers;

    void Start()
    {
        _player = GameObject.Find("Player");

        _watchers = new List<Transform>();

        var temp = gameObject.GetComponentsInChildren<WatcherManager>(); // Змініть на Transform

        foreach (var watchers in temp)
        {
            watchers.SetPlayer(_player);
            _watchers.Add(watchers.GetComponent<Transform>());
        }

        StartCoroutine(RandomJumpCoroutine());
    }
    

    IEnumerator RandomJumpCoroutine()
    {
        while (true)
        {
            float randomHeight = Random.Range(0.5f, 1f); // Випадкова висота підпригування
            float randomTime = Random.Range(1f, 2f) / 6f; // Випадковий час між підпригуваннями

            List<GameObject> temp = new List<GameObject>();

            for (int i = 0; i < 3; i++)
            {
                temp.Add(_watchers[Random.Range(0, _watchers.Count)].gameObject);
            }

            float halfHeight = randomHeight / 2f;

            iTween.MoveBy(temp[0], iTween.Hash(
                "y", halfHeight,
                "time", randomTime, // Швидший стрибок вгору
                "easeType", "easeOutQuad"
            ));

            yield return new WaitForSeconds(0.1f); // Очікування частини часу до стрибка вниз
            
            iTween.MoveBy(temp[1], iTween.Hash(
                "y", halfHeight,
                "time", randomTime, // Швидший стрибок вгору
                "easeType", "easeOutQuad"
            ));

            yield return new WaitForSeconds(0.1f); // Очікування частини часу до стрибка вниз
            
            iTween.MoveBy(temp[2], iTween.Hash(
                "y", halfHeight,
                "time", randomTime, // Швидший стрибок вгору
                "easeType", "easeOutQuad"
            ));
            
            iTween.MoveBy(temp[0], iTween.Hash(
                "y", -halfHeight,
                "time", randomTime, // Швидший стрибок вниз
                "easeType", "easeInQuad"
            ));
            
            yield return new WaitForSeconds(0.1f); // Очікування частини часу до стрибка вниз
            
            iTween.MoveBy(temp[1], iTween.Hash(
                "y", -halfHeight,
                "time", randomTime, // Швидший стрибок вниз
                "easeType", "easeInQuad"
            ));
            
            yield return new WaitForSeconds(0.1f); // Очікування частини часу до стрибка вниз

            iTween.MoveBy(temp[2], iTween.Hash(
                "y", -halfHeight,
                "time", randomTime, // Швидший стрибок вниз
                "easeType", "easeInQuad"
            ));
            yield return new WaitForSeconds(randomTime * 3); // Очікування частини часу до наступного стрибка
        }
    }
    
    
}
