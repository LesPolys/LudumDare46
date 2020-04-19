using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connectable : MonoBehaviour
{
    [SerializeField]
    public Transform connectionPoint;

    public List<GameObject> containtedTypes;

    public bool isConnected = false;

    void Awake()
    {
        containtedTypes = new List<GameObject>();
    }

}
