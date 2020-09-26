using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        setTextScore();
    }

    // Update is called once per frame
    void Update()
    {
        setTextScore();
    }

    private void setTextScore()
    {
        scoreText.text = gameSession.GetScore().ToString();
    }
}
