using UnityEngine;
using UnityEngine.UI;

public class Eat : MonoBehaviour
{
    public string Tag;
    public float Increase;

    private int Score = 0;
    public Text ScoreText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag))
        {
            transform.localScale += new Vector3(Increase, Increase, Increase);
            Score += 10;
            ScoreText.text = "Score : " + Score;
            
            Destroy(other.gameObject);
            
        }
    }
}
