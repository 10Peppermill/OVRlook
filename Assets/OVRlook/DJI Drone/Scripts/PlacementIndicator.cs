using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementIndicator : MonoBehaviour
{
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("HDRenderPipeline/Lit");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Color errorRed = new Color(255, 0, 0, 60);

        rend.material.SetColor("_BaseMap", errorRed);
    }
    private void OnCollisionExit(Collision collision)
    {
        Color goodGreen = new Color(0, 255, 0, 60);

        rend.material.SetColor("_BaseMap", goodGreen);
    }
}
