using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connectable : MonoBehaviour
{
    [SerializeField]
    public Transform connectionPoint;

    public bool isConnected = false;

}
