using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;

    public int score = 0;
    public ScoreView view;

    void Start() {
        if (instance != null) return;
        instance = this;
    }

    void FixedUpdate() {
        view.UpdateScore(score);
    }
}

[System.Serializable]
public class ScoreView {
    public Text scoreText;

    public void UpdateScore(int score) {
        scoreText.text = score.ToString();
    }
}
