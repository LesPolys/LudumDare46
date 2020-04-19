using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloPillarController : MonoBehaviour
{

    [SerializeField]
    GameObject holoRotatingSpawn;

    private GameObject currentHolo;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNewHolo(GameObject newHolo)
    {
        if (currentHolo != null)
        {
            Destroy(currentHolo.gameObject);
            currentHolo = null;
        }

        GameObject tempHolo = GameObject.Instantiate(newHolo, this.transform.position, Quaternion.identity);
        tempHolo.transform.rotation = holoRotatingSpawn.transform.rotation;
        tempHolo.transform.SetParent(holoRotatingSpawn.transform);
        tempHolo.transform.localPosition = Vector3.zero;
        currentHolo = tempHolo;
    }
}
