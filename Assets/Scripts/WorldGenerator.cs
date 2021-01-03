using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    private const int SP_WEIGHT = 10;
    private const int MP_WEIGHT = 8;
    private const int LP_WEIGHT = 3;
    private const int SAF_WEIGHT = 80;
    private const int LAF_WEIGHT = 10;
    private const int NONE_WEIGHT = 100;

    public Transform player;
    public float playerOffset;

    public float minSpawnGap;
    public float maxSpawnGap;

    public GameObject[] smallPlanetPrefabs;
    public GameObject[] mediumPlanetPrefabs;
    public GameObject[] largePlanetPrefabs;
    public GameObject[] smallAsteroidFieldPrefabs;
    public GameObject[] largeAsteroidFieldPrefabs;

    public Transform[] smallPlanetSpawnpoints;
    public Transform[] mediumPlanetSpawnpoints;
    public Transform[] largePlanetSpawnpoints;
    public Transform[] smallAsteroidFieldSpawnpoints;
    public Transform[] largeAsteroidFieldSpawnpoints;

    private int totalWeight;
    private int spThreshold;
    private int mpThreshold;
    private int lpThreshold;
    private int safThreshold;
    private int lafThreshold;

    private int spSpawnIndex;
    private int mpSpawnIndex;
    private int lpSpawnIndex;
    private int safSpawnIndex;
    private int lafSpawnIndex;

    private float nextGapSqrMag;
    private Vector3 prevSpawnPosition;

    private Transform spawnParent;

    void Awake()
    {
        spawnParent = new GameObject("SpawnParent").transform;

        totalWeight = SP_WEIGHT + MP_WEIGHT + LP_WEIGHT + SAF_WEIGHT + LAF_WEIGHT + NONE_WEIGHT;
        spThreshold = SP_WEIGHT;
        mpThreshold = spThreshold + MP_WEIGHT;
        lpThreshold = mpThreshold + LP_WEIGHT;
        safThreshold = lpThreshold + SAF_WEIGHT;
        lafThreshold = safThreshold + LAF_WEIGHT;
    }

    void Start()
    {
        transform.Rotate(player.transform.eulerAngles);
    }

    void Update()
    {
        transform.position = player.transform.position + player.transform.forward * playerOffset;

        float gapSqrMag = (transform.position - prevSpawnPosition).sqrMagnitude;
        if (gapSqrMag >= nextGapSqrMag)
        {
            int randIndex = Random.Range(0, totalWeight);
            if (randIndex < spThreshold)
            {
                spSpawnIndex = (spSpawnIndex + 1) % smallPlanetSpawnpoints.Length;
                SpawnRoundRobin(smallPlanetPrefabs, smallPlanetSpawnpoints, spSpawnIndex);
            }
            else if (randIndex < mpThreshold)
            {
                mpSpawnIndex = (mpSpawnIndex + 1) % mediumPlanetSpawnpoints.Length;
                SpawnRoundRobin(mediumPlanetPrefabs, mediumPlanetSpawnpoints, mpSpawnIndex);
            }
            else if (randIndex < lpThreshold)
            {
                lpSpawnIndex = (lpSpawnIndex + 1) % largePlanetSpawnpoints.Length;
                SpawnRoundRobin(largePlanetPrefabs, largePlanetSpawnpoints, lpSpawnIndex);
            }
            else if (randIndex < safThreshold)
            {
                safSpawnIndex = (safSpawnIndex + 1) % smallAsteroidFieldSpawnpoints.Length;
                SpawnRoundRobin(smallAsteroidFieldPrefabs, smallAsteroidFieldSpawnpoints, safSpawnIndex);
            }
            else if (randIndex < lafThreshold)
            {
                lafSpawnIndex = (lafSpawnIndex + 1) % largeAsteroidFieldSpawnpoints.Length;
                SpawnRoundRobin(largeAsteroidFieldPrefabs, largeAsteroidFieldSpawnpoints, lafSpawnIndex);
            }

            prevSpawnPosition = transform.position;
            nextGapSqrMag = Random.Range(minSpawnGap, maxSpawnGap);
            nextGapSqrMag *= nextGapSqrMag;
        }
    }

    private void SpawnRoundRobin(GameObject[] prefabs, Transform[] spawnpoints, int spawnIndex)
    {
        GameObject randPrefab = prefabs[Random.Range(0, prefabs.Length)];
        Transform randSpawnpoint = spawnpoints[spawnIndex];
        Quaternion randRotation = Quaternion.LookRotation(Random.onUnitSphere);
        Instantiate(randPrefab, randSpawnpoint.position, randRotation, spawnParent);
    }
}
