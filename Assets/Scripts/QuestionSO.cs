using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string question = "Enter question here";
    [SerializeField] string[] answer = new string[4];
    
    [SerializeField] int correctAnswerIndex;
    public string GetQuestion()
    {
        return question;
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

    public string GetAnswer(int index) 
    {
        return answer[index];
    }
}
