using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public float spawnInterval = 1.5f;
    public float minX = -2.5f;
    public float maxX = 2.5f;
    public float spawnY = 6f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 0.5f, spawnInterval);
    }

    void SpawnAsteroid()
    {
        if (asteroidPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, asteroidPrefabs.Length);
            GameObject selectedAsteroid = asteroidPrefabs[randomIndex];

            float randomX = Random.Range(minX, maxX);
            Vector2 spawnPosition = new Vector2(randomX, spawnY);

            Instantiate(selectedAsteroid, spawnPosition, Quaternion.identity);
        }
    }
}