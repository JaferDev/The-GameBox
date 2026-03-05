using UnityEngine;
using DG.Tweening;
using System.Threading;

public class Enemy1 : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;

    private float timer = 0;
    private bool moved = false;
    private int movedCnt = 0;
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
        if (!gameObject) return;
        if (timer % 3f < 0.2f) moved = false; //Reset
        timer += Time.deltaTime;
        if (timer % 3 > 2.98f && ! moved) MoveEnemy();
    }
     
    private void MoveEnemy()
    {
        moved = true;  
        if (movedCnt % 2 == 0) transform.DOMoveX(transform.position.x - 0.7f, 0.5f);
        else transform.DOMoveX(transform.position.x + 0.7f, 0.5f);
        movedCnt++;
    }

    public void BounceOff(Rigidbody2D bulletRb, float speed)
    {
        bulletRb.velocity = new Vector2(0f, -speed);
    }

    public void Kill(GameObject bulletObj)
    { 
        Destroy(bulletObj);
        Destroy(gameObject);
    }

    private void StopDisplay()
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }
    }
}
