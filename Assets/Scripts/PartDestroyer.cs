using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDestroyer : MonoBehaviour
{
    [SerializeField]
    Transform holdPosition;

    [SerializeField]
    AudioSource success;

    [SerializeField]
    AudioSource fail;

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

        if (toDestroy.GetComponent<Connectable>().containtedTypes.Count >= 2)
        {
            //Debug.Log("asdf");
            List<GameObject> set = toDestroy.GetComponent<Connectable>().containtedTypes;
            BotDisposalGenerator.GameObjectPair pair = disposal.CurrentPair;

            if (pair.left.gameObject.name + "(Clone)" == set[0].name  && pair.right.gameObject.name + "(Clone)" == set[1].name  || pair.left.gameObject.name + "(Clone)" == set[1].name  && pair.right.gameObject.name + "(Clone)" == set[0].name )
            {
                //  Debug.Log("Good");
                success.Play();
                disposal.UpdateCurrentGoal();
            }
            else
            {
                //Debug.Log("Bad");
                fail.Play();

                GameObject a = set[0].gameObject;
                GameObject b = set[1].gameObject;

                disposal.ReAddElement(a);
                disposal.ReAddElement(b);
            }

        }
        else
        { 
            fail.Play();
             GameObject a = toDestroy;
            disposal.ReAddElement(a);
        }

        Destroy(toDestroy.transform.root.gameObject);

    }
}
