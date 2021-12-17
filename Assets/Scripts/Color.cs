using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Color : MonoBehaviour
{
    public List<Material> materials = new List<Material>();

    private void Awake()
    {
        GetComponent<Renderer>().material = materials[Random.Range(0, materials.Count)];
    }
}
