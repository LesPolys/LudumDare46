using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> parts;

    [SerializeField]
    Transform spawnPosition;

    private List<int> sizes;

    // Start is called before the first frame update
    void Start()
    {
        sizes = new List<int> { 2, 3, 4 };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject part = GameObject.Instantiate(parts[Random.Range(0,parts.Count)], spawnPosition.position, Quaternion.identity, null);
            part.transform.localScale = Vector3.one * sizes[Random.Range(0,sizes.Count)];
        }
    }

    public void SpawnPart(GameObject newPart)
    {
        GameObject part = GameObject.Instantiate(newPart, spawnPosition.position, Quaternion.identity, null);
        part.transform.localScale = Vector3.one * sizes[Random.Range(0, sizes.Count)];
    }

    public void SpawnRandomPart()
    {
        GameObject part = GameObject.Instantiate(parts[Random.Range(0, parts.Count)], spawnPosition.position, Quaternion.identity, null);
        part.transform.localScale = Vector3.one * sizes[Random.Range(0, sizes.Count)];
    }
}
