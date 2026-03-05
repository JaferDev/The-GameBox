using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class EnemyGeneration : MonoBehaviour
{
    [SerializeField] Vector2[] positions;
    [SerializeField] GameObject[] enemiesObjs;
    [SerializeField] PlayerClass player;
    [SerializeField] GameObject bulletObj;
    [SerializeField] float enemyReloadTime = 5;
    [SerializeField] RectTransform scoreRect;

    private int currentEnemyAmt = 0;
    private float timer = 0;
    private int killedBefore = 0;
    private bool shot = false;
    [SerializeField] private int roundNo = 0;
    [SerializeField] private int difficulty = 0;
    private void Start()
    {
        NextRound();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (player.enemiesKilled == currentEnemyAmt + killedBefore) NextRound();
        if (timer % enemyReloadTime > enemyReloadTime - 0.01f && !shot) Shoot();   
        if (timer % enemyReloadTime + 0.5f > enemyReloadTime) shot = false;
    }

    private void NextRound()
    {
        player.roundNo++;
        roundNo++;
        if (roundNo % 2 == 0) difficulty++;
        if (difficulty < 7) enemyReloadTime = 5 - difficulty / 2;
        else enemyReloadTime = 1;
        player.enemiesAlive = new();
        killedBefore += currentEnemyAmt;

        PlaceRandomEnemies();
    }

    private void PlaceRandomEnemies()
    {
        GetEnemiesAmount();
        int[] enemyPositions = GetRandomArray();
        player.enemiesAlive = ArrayToList(enemyPositions);

        foreach(int i in enemyPositions)
        {
            Instantiate(GetRandomEnemy(), positions[i], Quaternion.identity);
        }
    }

    private void GetEnemiesAmount()
    {
        if (difficulty >= 7) currentEnemyAmt = Random.Range(10, 12);
        else if (difficulty >= 4) currentEnemyAmt = Random.Range(4 + difficulty, 6 + difficulty);
        else currentEnemyAmt = Random.Range(2 + difficulty, 3 + difficulty);
    }

    private GameObject GetRandomEnemy()
    {
        return enemiesObjs[Random.Range(0, enemiesObjs.Length)];
    }

    private int[] GetRandomArray()
    {
        int[] randomArray = new int[currentEnemyAmt];

        List<int> emptyPositions = new();
        for (int i = 0; i < 12; i++) emptyPositions.Add(i);

        for(int i = 0; i < currentEnemyAmt; i++)
        {
            int rng = Random.Range(0, emptyPositions.Count());
            randomArray[i] = emptyPositions[rng];
            emptyPositions.Remove(emptyPositions[rng]);
        }

        return randomArray;
    }

    private List<Vector2> ArrayToList(int[] array)
    {
        List<Vector2> arrayCopy = new();
        foreach (int index in array) arrayCopy.Add(positions[index]);
        return arrayCopy;
    }

    private Vector2[] RandomHalf()
    {
        int length = player.enemiesAlive.Count() / 2;
        if (player.enemiesAlive.Count() == 1) length = 1;
        Vector2[] shootingPoints = new Vector2[length];

        for(int i = 0; i < length; i++)
        {
            Vector2 randomPos = player.enemiesAlive[Random.Range(0, player.enemiesAlive.Count())];
            if (shootingPoints.Contains(randomPos)) i -= 1;
            else shootingPoints[i] = (randomPos);
        }

        return shootingPoints;
    }

    private void Shoot()
    {
        shot = true;

        Vector2[] shootingPoints = RandomHalf();
        foreach (Vector2 position in shootingPoints)
        {
            Instantiate(bulletObj, position, Quaternion.Euler(0, 0, -180));
        }
    }
}
