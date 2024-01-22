using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public static int Score;
    
    [SerializeField] private TMP_Text scoreText;

    private float _time = 0;
    
    private void OnEnable()
    {
        _time = 0;
        Score = 0;
        
        InvokeRepeating(nameof(IncreaseScore), .1f, .1f);
    }

    private void IncreaseScore()
    {
        Score++;
        scoreText.text = Score.ToString();
    }

    private void OnDisable()
    {
        scoreText.text = "0";
        CancelInvoke(nameof(IncreaseScore));
    }
}
