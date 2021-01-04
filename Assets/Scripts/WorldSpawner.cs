using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpawner : MonoBehaviour
{
    public Player player;
    public Planet[] planetPrefabs;
    public Transform[] spawnpoints;

    public float minPlanetSize;
    public float maxPlanetSize;
    public float minPlanetOffset;
    public float maxPlanetOffset;
    public float minPlanetSpawnGap;
    public float maxPlanetSpawnGap;

    public float maxGameDuration;
    public EndingMessage survivalEnding;

    private int spawnIndex = 0;
    private int planetIndex = 0;
    private float maxDistance;
    private float curDistance;
    private Vector3 refPosition;
    private Vector3 refForward;
    private Vector3 refRight;
    private Vector3 refUp;
    
    private Transform spawnParent;

    void Start()
    {
        spawnParent = new GameObject("SpawnParent").transform;

        maxDistance = player.movementSpeed * maxGameDuration;
        refPosition = player.transform.position;
        refForward = player.transform.forward;
        refRight = player.transform.right;
        refUp = player.transform.up;

        ShufflePlanets();
        ShuffleSpawnpoints();
        SpawnPlanets();
    }

    void Update()
    {
        if (Time.time > maxGameDuration && player.IsAlive())
        {
            EndingMenu.instance.PlayEnding(survivalEnding.message);
            AchievementSystem.instance.AcquireAchievement(survivalEnding.endingNumber);
        }
    }

    private void SpawnPlanets()
    {
        while (curDistance < maxDistance)
        {
            curDistance += Random.Range(minPlanetSpawnGap, maxPlanetSpawnGap);

            Vector3 horOffset = Random.Range(minPlanetOffset, maxPlanetOffset) * RandomSign() * refRight;
            Vector3 verOffset = Random.Range(minPlanetOffset, maxPlanetOffset) * RandomSign() * refUp;
            Vector3 spawnPos = spawnpoints[spawnIndex].position + curDistance * refForward + horOffset + verOffset;
            spawnIndex++;
            if (spawnIndex >= spawnpoints.Length)
            {
                spawnIndex = 0;
                ShuffleSpawnpoints();
            }

            spawnIndex = (spawnIndex + 1) % spawnpoints.Length;
            SpawnPlanet(spawnPos);
        }
    }

    private void SpawnPlanet(Vector3 spawnPos)
    {
        // Choose planet prefab (round robin with shuffle)
        Planet planetPrefab = planetPrefabs[planetIndex];
        planetIndex++;
        if (planetIndex >= planetPrefabs.Length)
        {
            planetIndex = 0;
            ShufflePlanets();
        }

        // Determine spawn modifiers
        float randSize = Random.Range(minPlanetSize, maxPlanetSize);
        Quaternion randRotation = Quaternion.LookRotation(Random.onUnitSphere);
        
        // Spawn planet
        Planet planet = Instantiate(planetPrefab, spawnPos, randRotation);
        planet.transform.localScale = Vector3.one * randSize;
    }

    private void ShuffleSpawnpoints()
    {
        for (var i = 0; i < spawnpoints.Length - 1; ++i) {
            var r = Random.Range(i, spawnpoints.Length);
            var temp = spawnpoints[i];
            spawnpoints[i] = spawnpoints[r];
            spawnpoints[r] = temp;
        }
    }

    private void ShufflePlanets()
    {
        for (var i = 0; i < planetPrefabs.Length - 1; ++i) {
            var r = Random.Range(i, planetPrefabs.Length);
            var temp = planetPrefabs[i];
            planetPrefabs[i] = planetPrefabs[r];
            planetPrefabs[r] = temp;
        }
    }

    private int RandomSign()
    {
        return Random.value < .5? 1 : -1;
    }
}
