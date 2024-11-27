using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private int _scoreMultiplier = 1;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        _scoreText.text = _score.ToString();
    }

    public void AddScore(int score)
    {
        _score += score * _scoreMultiplier;
        _scoreText.text = _score.ToString();
    }

    public int GetScore()
    {
        return _score;
    }
    
    public void ResetScore()
    {
        _score = 0;
        _scoreText.text = _score.ToString();
    }
    
    public void SetScoreMultiplier(int multiplier)
    {
        _scoreMultiplier = multiplier;
    }
}
