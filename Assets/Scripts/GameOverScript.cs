using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    private Button[] buttons;

    void Awake() {
        buttons = GetComponentsInChildren<Button>();

        HideButtons();
    }

    public void ShowButtons() {
        foreach (Button button in buttons) {
            button.gameObject.SetActive(true);
        }
    }
    public void HideButtons() {
        foreach (Button button in buttons) {
            button.gameObject.SetActive(false);
        }
    }

    public void ExitToMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame() {
        SceneManager.LoadScene("Stage-1");
    }
}

