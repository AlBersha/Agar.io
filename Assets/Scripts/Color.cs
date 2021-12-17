using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Color : MonoBehaviour
{
    public List<Material> Materials = new List<Material>();

    private void Awake()
    {
        GetComponent<Renderer>().material = Materials[Random.Range(0, Materials.Count)];
    }
}
