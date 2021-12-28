using Assets.Scripts;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();


        int foodSize = _packet.ReadInt();
        for (int i = 0; i < foodSize; i++)
            GameManager.instance.SpawnFood(_packet.ReadVector2());

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
        int _score = _packet.ReadInt();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _score);
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

        const float scaleIncrease = 0.1f;
        GameManager.players[_id].transform.localScale += new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);
        GameManager.players[_id].score = _score;

        if (_id == Client.instance.myId)
            GameManager.instance.ScoreText.text = "Score : " + _score;

        GameManager.posToFood[_position].gameObject.SetActive(false);
        Destroy(GameManager.posToFood[_position].gameObject);
    }

    public static void PlayerEaten(Packet _packet)
    {
        int _loserId = _packet.ReadInt();

        int _winnerId = _packet.ReadInt();
        int _winnerScore = _packet.ReadInt();

        GameManager.players[_winnerId].transform.localScale += GameManager.players[_loserId].transform.localScale;
        GameManager.players[_winnerId].score = _winnerScore;

        if (_loserId == Client.instance.myId)
            GameManager.instance.ScoreText.text += "; YOU LOSE";

        GameManager.players[_loserId].gameObject.SetActive(false);
        Destroy(GameManager.players[_loserId].gameObject);
    }
}