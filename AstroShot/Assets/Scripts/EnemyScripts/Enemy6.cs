using UnityEngine;
using DG.Tweening;

public class Enemy6 : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] sprites;
    private void Start()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.DOFade(1, 0.7F);
        }
    }
    public void Kill(GameObject bulletObj)
    {
        Destroy(bulletObj);
        Destroy(gameObject);
    }
}   
