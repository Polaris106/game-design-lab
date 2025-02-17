using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    public enum SpawnState
    {
        SPAWNING,
        WAITING
    }

    [System.Serializable]
    public class Wave
    {
        public string name;

        public Transform coins;

        public int count;

        public float rate;

    }

    public Wave wave;
    public Transform[] spawnPoints;
    public float timeUntilStart = 3f;
    public bool gameEnd = false;

    private SpawnState state = SpawnState.WAITING;
    private float searchCountDown = 1f;
    private float waveCountdown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilStart -= Time.deltaTime;
        if (timeUntilStart <= 0)
        {
            if (state == SpawnState.WAITING && !gameEnd)
            {
                StartCoroutine(SpawnWave(wave));
            }
            else if (gameEnd)
            {
                StopAllCoroutines();
            }

        }

    }

    IEnumerator SpawnWave(Wave _wave)
    {

        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnCoins(_wave.coins);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnCoins(Transform coins)
    {
        Debug.Log("Spawning Coins");
        Transform _sp = spawnPoints[Random.Range(0, 4)];
        Instantiate(coins, _sp.position, _sp.rotation);

    }
}
