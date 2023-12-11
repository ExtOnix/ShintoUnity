using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatform : Platform
{
    [SerializeField] Material initMaterial = null;
    [SerializeField] Material fadeMaterial = null;
    private void Awake()
    {
        OnActive += UnFade;
        OnDisable += Fade;
    }

    void UnFade()
    {
            gameObject.layer = LayerMask.NameToLayer("Wall");
            gameObject.GetComponent<Renderer>().material = initMaterial;
    }

    void Fade()
    {
            gameObject.layer = LayerMask.NameToLayer("Transparent");
            gameObject.GetComponent<Renderer>().material = fadeMaterial;
    }
}
