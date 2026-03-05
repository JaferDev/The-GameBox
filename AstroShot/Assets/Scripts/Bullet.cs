using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerClass player;
    public GameObject deathEffect;

    private Enemy1 enemy1;
    private Enemy5 enemy5;
    private Enemy6 enemy6;
    private Enemy4 enemy4;
    private Enemy3 enemy3;

    private float playerDmgTimer = 0;
    private void Start()
    {
        FindFirstObjectByType<AudioManager>().PlayOnce("ShootBullet");
        rb.velocity = transform.up * speed;
    }

    private void Update() { playerDmgTimer += Time.deltaTime; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) DamagePlayer(collision);
        if (!collision.CompareTag("Enemy")) return;

        GetEnemyType(collision);
            
        bool checkBounce = collision.GetComponent<BoxCollider2D>();
        bool checkDmg = collision.GetComponent<CircleCollider2D>();
        if (checkBounce) BounceAndBreak();
        else if (checkDmg) DamageEnemy(collision);
    }

    private void DamagePlayer(Collider2D collision)
    {
        if (playerDmgTimer <= 0.5f) return;

        TakeDamage takeDmg = collision.GetComponent<TakeDamage>();
        playerDmgTimer = 0;
        takeDmg.DamagePlayer();
        Destroy(gameObject);
    }

    private void BounceAndBreak()
    {
        FindFirstObjectByType<AudioManager>().PlayOnce("DoDmg");
        transform.rotation = Quaternion.Euler(0, 0, 180);
        if (enemy1) enemy1.BounceOff(rb, speed);
        else if (enemy3) enemy3.BounceOff(rb, speed);
        else if (enemy4) enemy4.BounceOff(rb, speed);
        else if (enemy5)
        {
            if (rb.velocity.y == 4) enemy5.BreakShield(rb, speed);
            else enemy5.BounceOff(rb, speed);
        }
    }

    private void DamageEnemy(Collider2D collision)
    {
        FindFirstObjectByType<AudioManager>().PlayOnce("DoDmg");
        player.enemiesAlive.Remove(collision.transform.position);
        player.enemiesKilled++;
        Instantiate(deathEffect, collision.transform.position, Quaternion.identity);
        if (enemy1) { player.score += 40; enemy1.Kill(gameObject); }
        else if (enemy3) { player.score += 50; enemy3.Kill(gameObject); }
        else if (enemy4) { player.score += 30; enemy4.Kill(gameObject); }
        else if (enemy5) { player.score += 20;  enemy5.Kill(gameObject); }
        else if (enemy6) { player.score += 10; enemy6.Kill(gameObject); }
    }

    private void GetEnemyType(Collider2D collision)
    {
        enemy1 = collision.GetComponent<Enemy1>();
        if (!enemy1) enemy1 = collision.GetComponentInParent<Enemy1>();

        enemy5 = collision.GetComponentInParent<Enemy5>();
        if (!enemy5) enemy5 = collision.GetComponentInParent<Enemy5>();

        enemy6 = collision.GetComponentInParent<Enemy6>();
        if (!enemy6) enemy6 = collision.GetComponentInParent<Enemy6>();

        enemy4 = collision.GetComponentInParent<Enemy4>();
        if (!enemy4) enemy4 = collision.GetComponentInParent<Enemy4>();

        enemy3 = collision.GetComponentInParent<Enemy3>();
        if (!enemy3) enemy3 = collision.GetComponentInParent<Enemy3>();
    }
}
