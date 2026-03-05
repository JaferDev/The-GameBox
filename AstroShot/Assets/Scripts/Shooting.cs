using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bulletPrefab;
    [SerializeField] private float reloadTime = 1.5f;
    [SerializeField] private float reloadTimer = 0;
    [SerializeField] GameObject enemyDeathEffect;

    private void Update()
    {
        reloadTimer += Time.deltaTime;

        if (reloadTimer < reloadTime) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) Shoot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) Shoot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) Shoot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) Shoot(3);
    }

    private void Shoot(int bulletIndex)
    {
        reloadTimer = 0;
        GameObject bullet = Instantiate(bulletPrefab[bulletIndex], firePoint.position, firePoint.rotation);
        Bullet bulletComp = bullet.GetComponent<Bullet>();
        bulletComp.deathEffect = enemyDeathEffect;
    }
}