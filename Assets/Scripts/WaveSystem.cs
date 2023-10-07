using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public enum SpawnStates { Spawning, Waiting, Counting }

    [System.Serializable]
    public class Wave
    {
        public string name;//nombre de la oleada
        public Transform[] enemy;//los tipos de enemigos que se van a crear
        public int Count;//la cantidad de enemigos
        public float rate = 1;//la diferencia de tiempo entre cada enemigo creado
    }
    //visual
    [SerializeField] int WaveCount;
    public Wave[] waves;
    [SerializeField] Transform[] SpawnPoints;
    private int nextWave = 0;
    public float TimerBetweenWaves;//puede ser 1 minuto entre cada oleada
    float WaveCountDown = 0f;
    private SpawnStates state = SpawnStates.Counting;
    private float searchCountDown = 1f;
    private int shipboss = 0;
    private void Start()
    {
        WaveCountDown = TimerBetweenWaves;
    }
    private void Update()
    {
        if (WaveCount < 10 && shipboss == 0)
        {
            if (state == SpawnStates.Waiting)
            {
                if (!EnemyisAlive())
                {
                    OleadaCompletada();
                }
                else
                {
                    Debug.Log("hay enemigos vivos");
                    return;
                }
            }
        }
        if (WaveCountDown <= 0)
        {
            if (state != SpawnStates.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            WaveCountDown -= Time.deltaTime;
        }


        if (WaveCount >= 10 && shipboss == 0)
        {
            StopAllCoroutines();

        }
    }

    bool EnemyisAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
    public void OleadaCompletada()
    {
        Debug.Log("oleada completada");
        state = SpawnStates.Counting;
        WaveCountDown = TimerBetweenWaves;
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }

    }
    IEnumerator SpawnWave(Wave _wave)
    {
        WaveCount++;
        state = SpawnStates.Spawning;
        for (int i = 0; i < _wave.Count; i++)
        {
            SpawnEnemys(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnStates.Waiting;
        yield break;
    }
    public void SpawnEnemys(Transform[] _enemy)//si queres spawnear otro enemigo arega otro if y pone enemy[al numero en el que esta el nuevo enemigo]
    {
        int numenemyProbability = Random.Range(0, SpawnPoints.Length);
        Instantiate(_enemy[0], SpawnPoints[numenemyProbability].transform.position, transform.rotation);
    }

}
