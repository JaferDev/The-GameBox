using UnityEngine;
using DG.Tweening;
public class Enemy3 : MonoBehaviour
{
    private bool isDead = false;
    private float rotation = 0;
    [SerializeField] private float rotAmt = 250;
    [SerializeField] SpriteRenderer[] sprites;
    private void Start()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.DOFade(1, 0.7F);
        }
    }

    private void Update()
    {
        if (isDead) return;
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        rotation += rotAmt * Time.deltaTime;
    }

    public void BounceOff(Rigidbody2D bulletRb, float speed)
    {
        bulletRb.velocity = new Vector2(0f, -speed);
    }

    public void Kill(GameObject bulletObj)
    {
        isDead = true;
        Destroy(bulletObj);
        Destroy(gameObject);
    }

}
