using Assets.Scripts;
using UnityEngine;

public class Eat : MonoBehaviour
{
    public string Tag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag) && GameManager.food.ContainsKey(transform.position))
            ClientSend.EatFood(other.gameObject.transform.position);
    }
}
