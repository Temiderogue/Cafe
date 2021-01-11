using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    [SerializeField] private Image _image;
    public string TypeOfFood;
    public Vector3 PoolPosition;
    public bool isInGame;
    public bool isOnTable;

    public int BoughtNumber;

    public bool isBought;
    
    public Vector3 TablePosition;

    private Spawner _spawner;



    private void Start()
    {
        _image.enabled = false;
        _spawner = FindObjectOfType<Spawner>();
        isInGame = false;
        isOnTable = false;
        PoolPosition = transform.position;
        //StartCoroutine(onTable());
        //StartCoroutine(onBought());
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor") // соприкосновение с полом
        {
            isInGame = false;
            isOnTable = false;
            Data.ChangeScore(-10);
            transform.position = PoolPosition;
            _spawner.RemoveFromQueue(gameObject.GetComponent<Food>());
        }
    }*/

    private IEnumerator onTable() // сохранение позиции на столе для того, чтоб поставить на это песто пустую посуду
    {
        yield return new WaitUntil(() => isOnTable == true);
        TablePosition = transform.position;
    }

    private IEnumerator onBought() // включение галочки при покупке
    {
        yield return new WaitUntil(() => isBought == true);
        _image.enabled = true;
    }

    public void BackToPool() // возвращение в пул
    {
        transform.position = PoolPosition;
        isInGame = false;
        isOnTable = false;
        //_food.isKinematic = true;
        _spawner.RemoveFromQueue(gameObject.GetComponent<Food>());
    }
}