using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Threading;

public class TakeDamage : MonoBehaviour
{
    private float timer = 0;
    [SerializeField] PlayerClass player;
    private float whenDmg = 0;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject deathParticles;
    [SerializeField] Transform playerTransform;

    private bool isDead = false;
    private float deathTime;
    private float deathScreenTime;

    private void Start()
    {
        player.roundNo = 0;
        player.enemiesKilled = 0;
        player.score = 0;
        player.hp = 10;
        player.timePlayed = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer - whenDmg >= 0.4f) sprite.DOColor(Color.white, 0.4f);

        if (!isDead) return;
        if (timer - deathTime >= 1f && timer - deathTime <= 1.1f)
        {
            FindFirstObjectByType<AudioManager>().Stop("MainOST");
            FindFirstObjectByType<AudioManager>().PlayOnce("GameOver");
            deathScreen.SetActive(true);
            deathScreenTime = timer;
        }
        if (timer - deathScreenTime >= 3f && timer - deathScreenTime <= 3.1f)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void DamagePlayer()
    {
        if (!sprite.enabled) return;

        FindFirstObjectByType<AudioManager>().PlayOnce("TakeDmg");
        whenDmg = timer;
        player.hp--;
        if (player.hp == 0) PlayerDie();
        sprite.DOColor(Color.red, 0.4f);
    }

    private void PlayerDie()
    {
        isDead = true;
        if (player.score > player.highScore) player.highScore = player.score;
        sprite.enabled = false;
        Instantiate(deathParticles, playerTransform.position, Quaternion.identity);
        deathTime = timer;
    }
}
