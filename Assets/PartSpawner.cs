using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> parts;

    [SerializeField]
    Transform spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject part = GameObject.Instantiate(parts[Random.Range(0,parts.Count)], spawnPosition.position, Quaternion.identity, null);
        }
    }
}
