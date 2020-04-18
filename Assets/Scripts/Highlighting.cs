using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighting : MonoBehaviour
{
    [SerializeField]
    Shader normalShader;

    [SerializeField]
    Shader highLightShader;

    public void Highlight()
    {
        gameObject.GetComponent<MeshRenderer>().material.shader = highLightShader;
    }

    public void UnHighlight()
    {
        gameObject.GetComponent<MeshRenderer>().material.shader = normalShader;
    }


}
