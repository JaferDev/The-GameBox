using UnityEngine;
using DG.Tweening;

public class Enemy5 : MonoBehaviour
{
    [SerializeField] BoxCollider2D shieldCollider;
    [SerializeField] BoxCollider2D[] spikesColliders;
    [SerializeField] GameObject forceFieldObj;
    [SerializeField] SpriteRenderer[] sprites;
    private void Start()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.DOFade(1, 0.7F);
        }
    }
    public void BounceOff(Rigidbody2D bulletRb, float speed)
    {
        bulletRb.velocity = new Vector2(0f, -speed);
    }

    public void BreakShield(Rigidbody2D bulletRb, float speed)
    {
        shieldCollider.enabled = false;
        if (spikesColliders.Length > 0) DisableSpikes();
        Destroy(forceFieldObj);
        BounceOff(bulletRb, speed);
    }

    public void Kill(GameObject bulletObj)
    {
        Destroy(bulletObj);
        Destroy(gameObject);
    }

    private void DisableSpikes()
    {
        foreach (BoxCollider2D collider in spikesColliders)
        {
            collider.enabled = false;
        }
    }
}
