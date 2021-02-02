using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppCoolerman : MonoBehaviour
{
    public const int MAX_QUANTITY = 150;
    public const int MAX_VELOCITY = 25;
    public const float MIN_SPAWN_TIME = 150;

    public List<Coolerman> enemies = new List<Coolerman>();
    public Transform enemiesContainer;
    public AudioClip gameOverAudio;
    public AudioClip gamePlayAudio;
    public bool isPlayerDead;
    public int score;

    internal float currentVelocity = 5;
    private AudioSource audioSource;
    private int currentQuantity = 5;
    private float currentSpawnTime = 1.5f;
    private GameObject enemy;
    private Player player;
    private UserInterface userInterface;

    public void Start()
    {
        enemy = Resources.Load<GameObject>("Prefab/Coolerman");
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();
        userInterface = FindObjectOfType<UserInterface>();
        StartCoroutine(SpawnEnemyCoroutine());
    }

    internal void KillEnemy(Coolerman enemy)
    {
        score++;
        RemoveEnemy(enemy);

        if (currentQuantity < MAX_QUANTITY)
        {
            currentQuantity = (int)(score * .025 + 5);
        }

        if (currentSpawnTime < MIN_SPAWN_TIME)
        {
            currentSpawnTime -= (currentSpawnTime * .05f);
        }

        if (currentVelocity < MAX_VELOCITY)
        {
            currentVelocity += (currentVelocity * .01f);
        }
    }

    internal void KillPlayer()
    {
        isPlayerDead = true;

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        player.Kill();
        userInterface.GameOver();

        audioSource.clip = gameOverAudio;
        audioSource.loop = false;

        audioSource.Play();

        StopAllCoroutines();

        enemies.ForEach(objCoolerman => objCoolerman.Destroy());
    }

    private void RemoveEnemy(Coolerman enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    private void SpawnEnemy()
    {
        if (enemies.Count < currentQuantity)
        {
            Instantiate(enemy, enemiesContainer);
        }
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(currentSpawnTime);
        }
    }
}