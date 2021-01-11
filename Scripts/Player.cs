using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Text _date;
    [SerializeField] private Text _score;
    [SerializeField] private Text _name;

    public void SetData(string scoreDate, int score, string name) 
    {
        _date.text = scoreDate;
        _score.text = score.ToString();
        _name.text = name;
    }
}
