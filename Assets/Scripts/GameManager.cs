using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Question quiz;
    EndScreen endScreen;

    void Awake() {
        quiz = FindObjectOfType<Question>();
        endScreen = FindObjectOfType<EndScreen>();
    }
    
    void Start()
    {
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if(!quiz.isComplete) return;
        quiz.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(true);
        endScreen.ShowFinalScore();
        
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuGame()
    {
        SceneManager.LoadScene("Menu");
    }

    
}
