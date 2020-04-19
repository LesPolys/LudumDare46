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
    List<GameObject> holoGrams;

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

    Dictionary<GameObject, GameObject> holoDictionary;

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

        holoDictionary = new Dictionary<GameObject, GameObject>();

        for (int i = 0; i < holoGrams.Count; i++)
        {
            holoDictionary.Add(holoOptions[i], holoGrams[i]);
        }

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

        UpdateCurrentGoal();
    }

    public void UpdateCurrentGoal(){
        goalList.RemoveAt(0);
        currentGoal = goalList[0];
        spawnHoloPair(goalList[0].left, goalList[0].right);
    }

    private void PopulateNextWave()
    {
        GameObjectPair newPair = new GameObjectPair();

        newPair.left = holoOptions[UnityEngine.Random.Range(0, holoOptions.Count)];
        newPair.right = holoOptions[UnityEngine.Random.Range(0, holoOptions.Count)];

        goalList.Add(newPair);
        AddElement(newPair.left);
        AddElement(newPair.right);
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

        bool isLeft = holoDictionary.TryGetValue(pillarL, out newL);
        bool isRight = holoDictionary.TryGetValue(pillarR, out newR);

        if (isLeft && isRight)
        {
            leftPillar.SpawnNewHolo(newL);
            rightPillar.SpawnNewHolo(newR);
        }
    }

    public void ReAddElement(GameObject earlyExit)
    {
        AddElement(earlyExit);
    }

    private void AddElement(GameObject obj)
    {
        if (holoOptions.Contains(obj))
        {
            foreach (GameObject objType in holoOptions)
            {
                if (obj == objType)
                {
                    toSpawn.Add(objType);
                    return;
                }
            }
        }
    }
}
