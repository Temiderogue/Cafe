using UnityEngine;
using UnityEngine.UI;

public class LocalScore : MonoBehaviour
{
    [SerializeField] private Text _date;
    [SerializeField] private Text _score;

    public void SetData(string scoreDate, int score)
    {
        _date.text = scoreDate;
        _score.text = score.ToString();
    }
}
