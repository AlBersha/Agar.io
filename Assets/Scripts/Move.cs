using UnityEngine;

public class Move : MonoBehaviour
{
    public float Speed;
    
    void Update()
    {
        var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var position = transform.position;
        target.z = position.z;

        position = Vector3.MoveTowards(position, target, Speed * Time.deltaTime / transform.localScale.x);
        transform.position = position;
    }
}
