using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerClass")]
public class PlayerClass : ScriptableObject
{
    public int enemiesKilled = 0;
    public int score = 0;
    public int hp = 0;
    public float timePlayed = 0;
    public int highScore;
    public int roundNo;

    public List<Vector2> enemiesAlive;
}