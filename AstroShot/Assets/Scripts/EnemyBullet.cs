using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] PlayerClass player;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -1) * 5f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        TakeDamage takeDmg = collision.GetComponent<TakeDamage>();
        takeDmg.DamagePlayer();
    }
}
