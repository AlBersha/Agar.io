using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
        public static Dictionary<Vector2, Food> posToFood = new Dictionary<Vector2, Food>();
        public static Dictionary<Vector3, Food> localPosToFood = new Dictionary<Vector3, Food>();

        public GameObject localPlayerPrefab;
        public GameObject playerPrefab;
        public GameObject foodPrefab;

        [SerializeField]
        public Text ScoreText;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }
        }

        public void SpawnPlayer(int _id, string _username, Vector3 _position)
        {
            GameObject _player;
            if (_id == Client.instance.myId)
            {
                _player = Instantiate(localPlayerPrefab, _position, Quaternion.identity);
            }
            else
            {
                _player = Instantiate(playerPrefab, _position, Quaternion.identity);
            }

            _player.GetComponent<PlayerManager>().id = _id;
            _player.GetComponent<PlayerManager>().username = _username;
            _player.GetComponent<PlayerManager>().score = 0;
            players.Add(_id, _player.GetComponent<PlayerManager>());
        }

        public void SpawnFood(Vector3 _position)
        {
            GameObject _food;

            Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(_position.x, _position.y, 0));
            target.z = 0;
            _food = Instantiate(foodPrefab, target, Quaternion.identity);

            _food.GetComponent<Food>().position = _position;
            _food.GetComponent<Food>().gameObject = _food;
            posToFood.Add(_position, _food.GetComponent<Food>());
            localPosToFood.Add(target, _food.GetComponent<Food>());
        }
    }
}
