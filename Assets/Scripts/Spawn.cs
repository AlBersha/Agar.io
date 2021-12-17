using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    public GameObject Food;
    public float Speed;

    void Generate()
    {
        var x = Random.Range(0, Camera.main.pixelWidth);
        var y = Random.Range(0, Camera.main.pixelHeight);

        var target = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
        target.z = 0;

        Instantiate(Food, target, Quaternion.identity);
    }

    private void Start()
    {
        InvokeRepeating(nameof(Generate), 0, Speed);
    }
}
