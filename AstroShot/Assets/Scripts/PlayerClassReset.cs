using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClassReset : MonoBehaviour
{
    [SerializeField] PlayerClass player;
    private void Start()
    {
        player.enemiesKilled = 0;
        player.score = 0;
        player.hp = 10;
        player.timePlayed = 0;
    }
}