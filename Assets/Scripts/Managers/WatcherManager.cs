using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WatcherManager : MonoBehaviour
{
    private GameObject _player;

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }
    
    private void Update()
    {
        Vector3 playerPosition = _player.transform.position; // Позиція гравця
        Vector3 lookAtPosition = new Vector3(playerPosition.x, transform.position.y, playerPosition.z); // Нова позиція перегляду, змінивши тільки координату y

        transform.LookAt(lookAtPosition); // Зміна напрямку перегляду тільки по осі y
    }
    

}
