using UnityEngine;

public class TrunkSpawnerScript : MonoBehaviour
{
    public GameObject[] enemyPaterns;

    private float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public float decTime;
    public float minTime = 0.65f;

    void FixedUpdate()
    {
        if (timeBtwSpawn <= 0f)
        {
            int rand = Random.Range(0, enemyPaterns.Length);
            Instantiate(enemyPaterns[rand], transform.position, Quaternion.identity);
            timeBtwSpawn = startTimeBtwSpawn;
            if (startTimeBtwSpawn > minTime)
            {
                startTimeBtwSpawn -= decTime;
            }
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
