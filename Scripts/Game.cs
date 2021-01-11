using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Canvas _afterGameMenu;
    [SerializeField] private Canvas _gameMenu;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Text _earnedMoney;
    [SerializeField] private Text _timerText;
    [SerializeField] private Image _timerBar;
    [SerializeField] private float _timer = 30;
    //[SerializeField] private float _timeLeft = 30;

    [SerializeField] private Table[] _tables;
    [SerializeField] private Chair[] _chairs;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _peopleOnTable;
    public Food[] FoodForSale;
    [SerializeField] private GameObject[] _foodPrefab;
    [SerializeField] private GameObject[] _emptyDishPrefab;

    public void Start()
    {
        Data.PeopleInCafe = 0;
        Data.PeopleOnTable = 0;
        //_spawner.RefreshList();
        
        Time.timeScale = 1;
        CheckOnActive();
        _spawner.SpawnFood();
        _spawner.CreatePeople();
        StartCoroutine(_spawner.PlaceHuman());
        Debug.Log(Data.PeopleInCafe + "|" + Data.PeopleInCafeAmount);
    }

    private void Update()
    {
        _scoreText.text = "Score: " + Data.Score.ToString();
        if (Data.isInGameWithTime)
        {
            _timerText.text = Mathf.RoundToInt(Data.TimeLeft).ToString();
            // Включаем таймер если включен режим игры       
            Data.TimeLeft -= Time.deltaTime;  
            _timerBar.fillAmount = Data.TimeLeft / _timer;
        }

        if (Data.isInGameWithoutTime)
        {
            _peopleOnTable.text = Data.PeopleOnTable.ToString() + "/" + Data.PeopleInCafeAmount.ToString();
        }
    }

    // Показываем экран окончания уровня когда время вышло
    private IEnumerator timeOver()
    {
        yield return new WaitUntil(() => Data.TimeLeft < 0);
        Time.timeScale = 0;
        _afterGameMenu.enabled = true;
        _earnedMoney.text = "Вы заработали: +" + Data.EarnedMoney.ToString();
        _gameMenu.enabled = false;
    }

    private IEnumerator GameWithoutTimeGameOver()
    {
        yield return new WaitUntil(() => Data.PeopleOnTable == Data.PeopleInCafeAmount);
        Time.timeScale = 0;
        _afterGameMenu.enabled = true;
        _earnedMoney.text = "Вы заработали: +" + Data.EarnedMoney.ToString();
        _gameMenu.enabled = false;
        Data.AddToLocalScoreList(Data.Score.ToString());
    }

    // загрузка меню из сцены с игрой
    public void LoadMenu()
    {
        Data.isInGameWithTime = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    // Берем данные из класса Data и в зависимости от них загружаем необходимые элементы
    private void CheckOnActive()
    {
        for (int i = 0; i < Data._chairList.Count; i++)
        {
            if (Data._chairList[i].isBought == true)
            {
                _chairs[i].gameObject.SetActive(true);
                _chairs[i].isBought = true;
            }
            else
            {
                _chairs[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < Data._tableList.Count; i++)
        {
            if (Data._tableList[i].isBought == true)
            {
                _tables[i].gameObject.SetActive(true);
                _tables[i].isBought = true;
            }
            else
            {
                _tables[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < _chairs.Length; i++)
        {
            if (_chairs[i].isBought == true)
            {
                _chairs[i].gameObject.SetActive(true);
                _spawner._freeSpots.Add(_chairs[i]);
            }
            else
            {
                _chairs[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < _tables.Length; i++)
        {
            if (_tables[i].isBought)
            {
                _tables[i].gameObject.SetActive(true);
            }
            else
            {
                _tables[i].gameObject.SetActive(false);
            }
        }

        for (int i = Data.BouhgtFood.Count - 1; i >= 0; i--)
        {
            Data.BouhgtFood.RemoveAt(i);
        }

        for (int i = 0; i < FoodForSale.Length; i++)
        {
            Data.foodList.Add(_foodPrefab[i].GetComponent<Food>());
            if (FoodForSale[i].isBought)
            {
                Data.BouhgtFood.Add(_foodPrefab[i].GetComponent<Food>());
            }
        }

        for (int i = 0; i < Data.BouhgtFood.Count; i++)
        {
            switch (Data.BouhgtFood[i].TypeOfFood)
            {
                case "Pie":
                    Data.emptyFoodList.Add(_emptyDishPrefab[0].GetComponent<emptyDish>());
                    break;
                case "Sushi":
                    Data.emptyFoodList.Add(_emptyDishPrefab[1].GetComponent<emptyDish>());
                    break;
                case "Coffee":
                    Data.emptyFoodList.Add(_emptyDishPrefab[2].GetComponent<emptyDish>());
                    break;
                case "Cocktail":
                    Data.emptyFoodList.Add(_emptyDishPrefab[3].GetComponent<emptyDish>());
                    break;
                case "Eggs":
                    Data.emptyFoodList.Add(_emptyDishPrefab[4].GetComponent<emptyDish>());
                    break;
                case "Fish":
                    Data.emptyFoodList.Add(_emptyDishPrefab[5].GetComponent<emptyDish>());
                    break;
                case "IceCream":
                    Data.emptyFoodList.Add(_emptyDishPrefab[6].GetComponent<emptyDish>());
                    break;
                case "Mojito":
                    Data.emptyFoodList.Add(_emptyDishPrefab[7].GetComponent<emptyDish>());
                    break;
                case "OrangeJuice":
                    Data.emptyFoodList.Add(_emptyDishPrefab[8].GetComponent<emptyDish>());
                    break;
                case "Pizza":
                    Data.emptyFoodList.Add(_emptyDishPrefab[9].GetComponent<emptyDish>());
                    break;
                case "Soup":
                    Data.emptyFoodList.Add(_emptyDishPrefab[10].GetComponent<emptyDish>());
                    break;
            }
        }
    }
}
