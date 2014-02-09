using UnityEngine;
using System.Collections;

/// <summary>
/// Workaround for not being able to set a particle system's sorting layer
/// in the editor.
/// </summary>
public class ParticleLayerScript : MonoBehaviour {

    public string sortingLayerName = "middle";

	void Start()
    {
        particleSystem.renderer.sortingLayerName = sortingLayerName;
    }	
}
