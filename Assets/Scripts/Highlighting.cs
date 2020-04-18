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
      
       if (gameObject.GetComponent<MeshRenderer>())
       {
           //gameObject.GetComponent<MeshRenderer>().material.shader = highLightShader;
            Material[] mats = gameObject.GetComponent<MeshRenderer>().materials;
            foreach (Material mat in mats)
            {
                mat.shader = highLightShader;
            }
       }

       foreach(MeshRenderer render in gameObject.GetComponentsInChildren<MeshRenderer>())
       {
           // render.material.shader = highLightShader;
            Material[] mats = render.materials;
            foreach (Material mat in mats)
            {
                mat.shader = highLightShader;
            }
        }
    }

    public void UnHighlight()
    {
        if (gameObject.GetComponent<MeshRenderer>())
        {
           // gameObject.GetComponent<MeshRenderer>().material.shader = normalShader;
            Material[] mats = gameObject.GetComponent<MeshRenderer>().materials;
            foreach (Material mat in mats)
            {
                mat.shader = normalShader;
            }
        }

        foreach (MeshRenderer render in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            //render.material.shader = normalShader;

            Material[] mats = render.materials;
            foreach (Material mat in mats)
            {
                mat.shader = normalShader;
            }
        }
    }
}
