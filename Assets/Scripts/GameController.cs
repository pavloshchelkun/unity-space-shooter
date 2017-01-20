using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Hazard;
    public Vector3 SpawnValues;
    public int HazardCount;
    public float SpawnWait;
    public float StartWait;
    public float WaveWait;

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(StartWait);
        while (true)
        {
            for (int i = 0; i < HazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(Hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(SpawnWait);
            }

            yield return new WaitForSeconds(WaveWait);
        }
    }
}
