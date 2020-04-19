using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDestroyer : MonoBehaviour
{
    [SerializeField]
    Transform holdPosition;

    [SerializeField]
    BotDisposalGenerator disposal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyParts(GameObject toDestroy)
    {
    
        disposal.UpdateCurrentGoal();
        

        Destroy(toDestroy.transform.root.gameObject);
    }
}
