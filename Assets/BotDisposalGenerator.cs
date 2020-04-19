using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDisposalGenerator : MonoBehaviour
{
    [SerializeField]
    HoloPillarController rightPillar;
    [SerializeField]
    HoloPillarController leftPillar;

    [SerializeField]
    Dictionary<GameObject,GameObject> holoDictionary;

    [SerializeField]
    List<GameObject> holoOptions;

    [SerializeField]
    PartDestroyer partsDestroyer;

    [SerializeField]
    PartSpawner partsSpawner;

    [SerializeField]
    float waveDelay;

    [SerializeField]
    float spawnDelay;

    [SerializeField]
    int backlogStartSize;

    [SerializeField]
    int waveSize;

    float currentWaveTime;

    float currentSpawnTime;

    List<GameObjectPair> goalList;

    GameObjectPair currentGoal;

    public GameObjectPair CurrentPair { get { return currentGoal; } }

    List<GameObject> toSpawn;


    public struct GameObjectPair {
        public GameObject left;
        public GameObject right;
    }

    // Start is called before the first frame update
    void Awake()
    {
        goalList = new List<GameObjectPair>();
        toSpawn = new List<GameObject>();
        PreLoadParts();
    }

    // Update is called once per frame
    void Update()
    {
        currentWaveTime += Time.deltaTime;
        if (currentWaveTime >= waveDelay)
        {
            currentWaveTime = 0;
            for (int i = 0; i < waveSize; i++)
            {
                PopulateNextWave();
            }
        }

        currentSpawnTime += Time.deltaTime;
        if (currentSpawnTime >= spawnDelay)
        {
            currentSpawnTime = 0;
            SpawnNextPart();
        }

    }

    private void PreLoadParts()
    {
        for (int i = 0; i < backlogStartSize; i++)
        {
            PopulateNextWave();
        }
    }

    public void UpdateCurrentGoal(){
        Debug.Log("YO");
        goalList.RemoveAt(0);
        currentGoal = goalList[0];
        spawnHoloPair(currentGoal.left, currentGoal.right);
    }

    private void PopulateNextWave()
    {
        GameObjectPair newPair = new GameObjectPair();

        newPair.left = holoOptions[UnityEngine.Random.Range(0, holoOptions.Count)];
        newPair.right = holoOptions[UnityEngine.Random.Range(0, holoOptions.Count)];

        goalList.Add(newPair);
        toSpawn.Add(newPair.left);
        toSpawn.Add(newPair.right);
        //Debug.Log(toSpawn.Count);

    }

    private void SpawnNextPart()
    {
        //Debug.Log(toSpawn.Count);
        if (toSpawn.Count > 0)
        {
            partsSpawner.SpawnPart(toSpawn[0]);
            toSpawn.RemoveAt(0);
        }
    }


    private void spawnHoloPair(GameObject pillarL, GameObject pillarR)
    {
        GameObject newL = new GameObject();
        GameObject newR = new GameObject();

        holoDictionary.TryGetValue(pillarL, out newL);
        holoDictionary.TryGetValue(pillarR, out newR);

        leftPillar.SpawnNewHolo(newL);
        rightPillar.SpawnNewHolo(newR);
    }
}
