using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] TextMeshProUGUI questionText;
    QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> quenstionList = new List<QuestionSO>();

    [Header("Answer")]
    [SerializeField] GameObject[] answerButton;
    int correctAnswerIndex;
    bool hasAnswerEarly;

    [Header("Button Color")]
    [SerializeField] Sprite correctAnswerImage;
    [SerializeField] Sprite defaultAnswerImage;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer; 

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = quenstionList.Count;
        progressBar.value = 0;
    }

    void Update() 
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            hasAnswerEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } else if(!hasAnswerEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    void GetNextQuestion()
    {
        if(quenstionList.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRamdomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
        
    }

    void GetRamdomQuestion()
    {
        int index = Random.Range(0, quenstionList.Count);
        currentQuestion = quenstionList[index];
        if(quenstionList.Contains(currentQuestion))
        {
            quenstionList.Remove(currentQuestion);
        }
    }
    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        
        for(int i=0;i<answerButton.Length;i++)
        {
            TextMeshProUGUI buttonText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>(); // Bằng con của cái answerButton mà con của cái answer là Text
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    public void OnAnswerSelect(int index)
    {
        hasAnswerEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

        if(progressBar.value == progressBar.maxValue)
        {
            isComplete = true;
            return;
        }
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;
        if(index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButton[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerImage;
            scoreKeeper.IncrementCorrectAnswer();
        }
        else 
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was; \n" + correctAnswer;
            buttonImage = answerButton[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerImage;
        }
    }

    void SetButtonState(bool state)
    {
        for(int i=0;i<answerButton.Length;i++)
        {
            Button button = answerButton[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprite()
    {
        for(int i=0;i<answerButton.Length;i++)
        {
            Image buttonImage = answerButton[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerImage;
        }
    }



}
