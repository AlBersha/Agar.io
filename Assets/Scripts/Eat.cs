using Assets.Scripts;
using UnityEngine;

public class Eat : MonoBehaviour
{
    const string foodTag = "Food";
    const string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(foodTag) && GameManager.localPosToFood.ContainsKey(other.gameObject.transform.position))
            ClientSend.EatFood(GameManager.localPosToFood[other.gameObject.transform.position].position);
        else if (other.gameObject.CompareTag(playerTag))
            ClientSend.EatPlayer(int.Parse(other.name));
    }
}
