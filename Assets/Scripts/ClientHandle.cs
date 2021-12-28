using Assets.Scripts;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector2 _position = _packet.ReadVector2();

        GameManager.instance.SpawnPlayer(_id, _username, _position);
    }
    
    public static void SpawnFood(Packet _packet)
    {
        Vector2 _position = _packet.ReadVector2();

        GameManager.instance.SpawnFood(_position);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector2 _position = _packet.ReadVector2();

        GameManager.players[_id].transform.position = _position;
    }

    public static void FoodEaten(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector2 _position = _packet.ReadVector2();
        int _score = _packet.ReadInt();

        GameManager.players[_id].transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        GameManager.players[_id].score = _score;

        if (_id == Client.instance.myId)
            GameManager.ScoreText.text = "Score : " + _score;

        Destroy(GameManager.food[_position].gameObject);
    }
}