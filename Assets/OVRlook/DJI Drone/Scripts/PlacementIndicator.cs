using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementIndicator : MonoBehaviour
{
    //private Renderer rend;
    public Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        //Renderer rend = GetComponent<Renderer>();
        //rend.material.shader = Shader.Find("Universal Render Pipeline/Lit");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Color errorRed = new Color(1, 0, 0, 0.2f);

        rend.material.SetColor("_BaseColor", errorRed);
        //rend.material.SetColor("Color", errorRed);
    }
    private void OnCollisionExit(Collision collision)
    {
        Color goodGreen = new Color(0, 1, 0, 0.2f);

        rend.material.SetColor("_BaseColor", goodGreen);
        //rend.material.SetColor("Color", goodGreen);
    }
}
