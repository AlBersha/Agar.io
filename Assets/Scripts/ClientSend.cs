using Assets.Scripts;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }

    public static void PlayerMovement(bool[] _inputs)
    {
        if (!GameManager.players[Client.instance.myId].isAlive)
            return;

        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }

            SendUDPData(_packet);
        }
    }

    public static void EatFood(Vector2 _foodPosition)
    {
        using (Packet _packet = new Packet((int)ClientPackets.eatFood))
        {
            _packet.Write(_foodPosition);
            SendUDPData(_packet);
        }
    }

    public static void EatPlayer(int _idVictim)
    {
        using (Packet _packet = new Packet((int)ClientPackets.eatPlayer))
        {
            _packet.Write(_idVictim);
            SendUDPData(_packet);
        }
    }
    #endregion
}