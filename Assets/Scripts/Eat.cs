using Assets.Scripts;
using UnityEngine;

public class Eat : MonoBehaviour
{
    public string Tag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag) && GameManager.localPosToFood.ContainsKey(other.gameObject.transform.position))
            ClientSend.EatFood(GameManager.localPosToFood[other.gameObject.transform.position].position);
    }
}
