
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    // Модели людей
    [SerializeField] private GameObject[] _humanPrefab;
    [SerializeField] private GameObject[] _sittingHumanPrefab;
    [SerializeField] private GameObject _waiterWithFood;
    [SerializeField] private GameObject _waiterWithOrder;

    // Модели еды
    [SerializeField] private GameObject[] _foodPrefab;

    [SerializeField] private GameObject[] _emptyDishPrefab;

    // столы и стулья
    [SerializeField] private Table[] _tables;
    [SerializeField] private Chair[] _chairs;

    //private List<Food> _boughtFood = new List<Food>();
    // Позиции стульев
    public List<Chair> _freeSpots = new List<Chair>();
    public List<Chair> _notFreeSpots = new List<Chair>();

    public List<bool> PeopleInGame = new List<bool>();
    public bool[] FoodOnBar = {false,false,false,false};

    [SerializeField] private Text _message;

    private int _randomNum;

    // Пулы с людьми
    private List<GameObject> _standingPeople = new List<GameObject>();
    public List<GameObject> _sittingPeople = new List<GameObject>();
    private List<NavMeshAgent> _peopleNavMeshes = new List<NavMeshAgent>();

    // Пулы с едой
    private List<Food> _pieList = new List<Food>();
    private List<Food> _sushiList = new List<Food>();
    private List<Food> _coffeeList = new List<Food>();
    private List<Food> _cocktailList = new List<Food>();
    private List<Food> _eggsList = new List<Food>();
    private List<Food> _fishList = new List<Food>();
    private List<Food> _iceCreamList = new List<Food>();
    private List<Food> _mojitoList = new List<Food>();
    private List<Food> _orangeJuiceList = new List<Food>();
    private List<Food> _pizzaList = new List<Food>();
    private List<Food> _soupList = new List<Food>();

    // Пул с пустой посудой
    public List<emptyDish> _emptyPieList = new List<emptyDish>();
    public List<emptyDish> _emptySushiList = new List<emptyDish>();
    public List<emptyDish> _emptyCoffeeList = new List<emptyDish>();
    public List<emptyDish> _emptCocktailList = new List<emptyDish>();
    public List<emptyDish> _emptyEggsList = new List<emptyDish>();
    public List<emptyDish> _emptyFishList = new List<emptyDish>();
    public List<emptyDish> _emptyIceCreamList = new List<emptyDish>();
    public List<emptyDish> _emptyMojitoList = new List<emptyDish>();
    public List<emptyDish> _emptyOrangeJuiceList = new List<emptyDish>();
    public List<emptyDish> _emptyPizzaList = new List<emptyDish>();
    public List<emptyDish> _emptySoupList = new List<emptyDish>();

    // Конвеер с едой
    private List<Food> _foodInQue = new List<Food>();

    public Food[] FoodForSale;

    // Поизиции пула всех видов еды
    private Vector3 _foodPosiition = new Vector3(6f, 1.5f, 22f);
    private Vector3 _emptyDishPosiition = new Vector3(20f, 1.5f, 22f);


    // Позиции на столе
    private Vector3 _deskPos = new Vector3(2, 2, 4.4f);
    private Vector3 _deskPos1 = new Vector3(2, 2, 4.4f);

    private Quaternion rotation = Quaternion.Euler(0f,180f,0f);

    [SerializeField] private ButtonForWaiter _button;

    public void SpawnFood()
    {
        for(int i = 0; i < 20; i++)
        {
            // Порции с едой
            // Берем данные из класса Data, и спавним только ту еду, которую надо
            for (int j = 0; j < Data.BouhgtFood.Count; j++)
            {
                switch (Data.BouhgtFood[j].TypeOfFood)
                {
                    case "Pie":
                        GameObject _food1 = Instantiate(Data.foodList[0].gameObject, _foodPosiition, Quaternion.identity);
                        _foodPosiition.x++;
                        _pieList.Add(_food1.GetComponent<Food>());
                        break;
                    case "Sushi":
                        GameObject _food2 = Instantiate(Data.foodList[1].gameObject, _foodPosiition, Quaternion.identity);
                        _foodPosiition.x++;
                        _sushiList.Add(_food2.GetComponent<Food>());
                        break;
                    case "Coffee":
                        GameObject _food3 = Instantiate(Data.foodList[2].gameObject, _foodPosiition, Quaternion.identity);
                        _foodPosiition.x++;
                        _coffeeList.Add(_food3.GetComponent<Food>());
                        break;
                    case "Cocktail":
                        GameObject _food4 = Instantiate(Data.foodList[3].gameObject, _foodPosiition, Quaternion.identity);
                        _foodPosiition.x++;
                        _cocktailList.Add(_food4.GetComponent<Food>());
                        break;
                    case "Eggs":
                        GameObject _food5 = Instantiate(Data.foodList[4].gameObject, _foodPosiition, Quaternion.identity);
                        _foodPosiition.x++;
                        _eggsList.Add(_food5.GetComponent<Food>());
                        break;
                    case "Fish":
                        GameObject _food6 = Instantiate(Data.foodList[5].gameObject, _foodPosiition, Quaternion.identity);
                        _foodPosiition.x++;
                        _fishList.Add(_food6.GetComponent<Food>());
                        break;
                    case "IceCream":
                        GameObject _food7 = Instantiate(Data.foodList[6].gameObject, _foodPosiition, Quaternion.identity);
                        _foodPosiition.x++;
                        _iceCreamList.Add(_food7.GetComponent<Food>());
                        break;
                    case "Mojito":
                        GameObject _food8 = Instantiate(Data.foodList[7].gameObject, _foodPosiition, Quaternion.identity);
                        _foodPosiition.x++;
                        _mojitoList.Add(_food8.GetComponent<Food>());
                        break;
                    case "OrangeJuice":
                        GameObject _food9 = Instantiate(Data.foodList[8].gameObject, _foodPosiition, Quaternion.identity);
                        _foodPosiition.x++;
                        _orangeJuiceList.Add(_food9.GetComponent<Food>());
                        break;
                    case "Pizza":
                        GameObject _food10 = Instantiate(Data.foodList[9].gameObject, _foodPosiition, Quaternion.identity);
                        _foodPosiition.x++;
                        _pizzaList.Add(_food10.GetComponent<Food>());
                        break;
                    case "Soup":
                        GameObject _food11 = Instantiate(Data.foodList[10].gameObject, _foodPosiition, Quaternion.identity);
                        _foodPosiition.x++;
                        _soupList.Add(_food11.GetComponent<Food>());
                        break;

                }
            }
            _foodPosiition.x = 6f;
            _foodPosiition.z += 2f;


            // Пустая посуда
            for (int j = 0; j < Data.BouhgtFood.Count; j++)
            {
                
                switch (Data.BouhgtFood[j].TypeOfFood)
                {
                    case "Pie":
                        GameObject _dish1 = Instantiate(Data.emptyFoodList[0].gameObject, _emptyDishPosiition, Quaternion.identity);
                        _emptyDishPosiition.x++;
                        _emptyPieList.Add(_dish1.GetComponent<emptyDish>());
                        break;
                    case "Sushi":
                        GameObject _dish2 = Instantiate(Data.emptyFoodList[1].gameObject, _emptyDishPosiition, Quaternion.identity);
                        _emptyDishPosiition.x++;
                        _emptySushiList.Add(_dish2.GetComponent<emptyDish>());
                        break;
                    case "Coffee":
                        GameObject _dish3 = Instantiate(Data.emptyFoodList[2].gameObject, _emptyDishPosiition, Quaternion.identity);
                        _emptyDishPosiition.x++;
                        _emptyCoffeeList.Add(_dish3.GetComponent<emptyDish>());
                        break;
                    case "Cocktail":
                        GameObject _dish4 = Instantiate(Data.emptyFoodList[3].gameObject, _emptyDishPosiition, Quaternion.identity);
                        _emptyDishPosiition.x++;
                        _emptCocktailList.Add(_dish4.GetComponent<emptyDish>());
                        break;
                    case "Eggs":
                        GameObject _dish5 = Instantiate(Data.emptyFoodList[4].gameObject, _emptyDishPosiition, Quaternion.identity);
                        _emptyDishPosiition.x++;
                        _emptyEggsList.Add(_dish5.GetComponent<emptyDish>());
                        break;
                    case "Fish":
                        GameObject _dish6 = Instantiate(Data.emptyFoodList[5].gameObject, _emptyDishPosiition, Quaternion.identity);
                        _emptyDishPosiition.x++;
                        _emptyFishList.Add(_dish6.GetComponent<emptyDish>());
                        break;
                    case "IceCream":
                        GameObject _dish7 = Instantiate(Data.emptyFoodList[6].gameObject, _emptyDishPosiition, Quaternion.identity);
                        _emptyDishPosiition.x++;
                        _emptyIceCreamList.Add(_dish7.GetComponent<emptyDish>());
                        break;
                    case "Mojito":
                        GameObject _dish8 = Instantiate(Data.emptyFoodList[7].gameObject, _emptyDishPosiition, Quaternion.identity);
                        _emptyDishPosiition.x++;
                        _emptyMojitoList.Add(_dish8.GetComponent<emptyDish>());
                        break;
                    case "OrangeJuice":
                        GameObject _dish9 = Instantiate(Data.emptyFoodList[8].gameObject, _emptyDishPosiition, Quaternion.identity);
                        _emptyDishPosiition.x++;
                        _emptyOrangeJuiceList.Add(_dish9.GetComponent<emptyDish>());
                        break;
                    case "Pizza":
                        GameObject _dish10 = Instantiate(Data.emptyFoodList[9].gameObject, _emptyDishPosiition, Quaternion.identity);
                        _emptyDishPosiition.x++;
                        _emptyPizzaList.Add(_dish10.GetComponent<emptyDish>());
                        break;
                    case "Soup":
                        GameObject _dish11 = Instantiate(Data.emptyFoodList[10].gameObject, _emptyDishPosiition, Quaternion.identity);
                        _emptyDishPosiition.x++;
                        _emptySoupList.Add(_dish11.GetComponent<emptyDish>());
                        break;

                }
            }
            _emptyDishPosiition.x = 20f;
            _emptyDishPosiition.z += 2f;
        }
    }

    public void CreatePeople()
    {
        // Официанты

        for (int i = 0; i < Data.WaiterSAmount; i++)
        {
            GameObject _waiter1 = Instantiate(_waiterWithOrder, Data.WaiterPosition,Quaternion.identity);
            GameObject _waiter2 = Instantiate(_waiterWithFood, Data.WaiterPoolPosition, Quaternion.identity);

            _waiter1.GetComponent<Waiter>()._waiterWithFoodNav = _waiter2.GetComponent<NavMeshAgent>();
            _waiter1.transform.rotation = rotation;
            _waiter2.GetComponent<NavMeshAgent>().enabled = false;
            _button.Waiter = _waiter1.GetComponent<Waiter>();
            Data.WaiterPosition.x += 1;
        }

        // для каждого свободного места создаем стоячего и сиядчего человека, связываем их по парам
        for (int i = 0; i < _freeSpots.Count; i++)
        {
            _randomNum = Random.Range(0, _humanPrefab.Length);
            PeopleInGame.Add(false);
            GameObject _human1 = Instantiate(_humanPrefab[_randomNum], Data._spawnPosiition1, Quaternion.identity);
            _standingPeople.Add(_human1);
            _peopleNavMeshes.Add(_human1.GetComponent<NavMeshAgent>());

            GameObject _human2 = Instantiate(_sittingHumanPrefab[_randomNum], Data._spawnPosiition2, Quaternion.identity);
            _sittingPeople.Add(_human2);
        }

        for (int i = 0; i < _freeSpots.Count; i++) 
        {
            _standingPeople[i].GetComponent<Human>().Sit = _sittingPeople[i];
            _sittingPeople[i].GetComponent<SittingHuman>().StandingHuman = _standingPeople[i];
            _sittingPeople[i].GetComponent<SittingHuman>().NavStandingHuman = _peopleNavMeshes[i];
        }
        Data.PeopleInCafeAmount = _freeSpots.Count;
    }

    public IEnumerator PlaceHuman() // Достаем человека из пул и направляем к столу
    {
        yield return new WaitForSeconds(5f);

        for (int i = 0; i < Data.PeopleInCafeAmount; i++)
        {
            if (PeopleInGame[i] == false)
            {
                PeopleInGame[i] = true;
                _standingPeople[i].transform.position = Data._doorPosition;
                //_standingPeople[i].GetComponent<Human>().isGoing = true;
                
                _standingPeople[i].GetComponent<NavMeshAgent>().enabled = true;
                _randomNum = Random.Range(0, _freeSpots.Count);

                //_standingPeople[i].GetComponent<Human>().Way = _freeSpots[_randomNum].transform.position;
                //_standingPeople[i].GetComponent<Human>().Rotation = _freeSpots[_randomNum].transform.rotation.eulerAngles;
                _sittingPeople[i].GetComponent<SittingHuman>().Num = i;
                _sittingPeople[i].GetComponent<SittingHuman>().Way = _freeSpots[_randomNum].Point.position;
                _sittingPeople[i].GetComponent<SittingHuman>().SitLocation = _freeSpots[_randomNum].transform.position;
                _sittingPeople[i].GetComponent<SittingHuman>().Chair = _freeSpots[_randomNum];
                _sittingPeople[i].GetComponent<SittingHuman>().Rotation = _freeSpots[_randomNum].transform.rotation.eulerAngles;
                _sittingPeople[i].GetComponent<SittingHuman>().isGoing = true;
                _peopleNavMeshes[i].enabled = true;
                _peopleNavMeshes[i].SetDestination(_freeSpots[_randomNum].Point.position);
                _notFreeSpots.Add(_freeSpots[_randomNum]);
                _freeSpots.Remove(_freeSpots[_randomNum]);
                Data.PeopleInCafe++;

                break;
            }
        }

        if (_freeSpots.Count == 0)
        {
            Debug.Log("началось");
            yield return new WaitUntil(() => _freeSpots.Count > 0);
            Debug.Log("своодных мест:" + _freeSpots.Count);
            StartCoroutine(PlaceHuman());
        }

        if (_freeSpots.Count > 0)
        {
            StartCoroutine(PlaceHuman());
        }
    }

    public void RefreshList() // обновление позиций и занесение данных в Data
    {
        // Свободные слоты 

        for (int i = _freeSpots.Count; i > 0; i--)
        {
            _freeSpots.RemoveAt(i);
        }

        for (int i = 0; i < _chairs.Length; i++)
        {
            if (_chairs[i].isBought)
            {
                _freeSpots.Add(_chairs[i]);
            }
        }


        // Столы и стулья

        for (int i = Data._chairList.Count - 1; i >= 0; i--)
        {
            Data._chairList.RemoveAt(i);
        }

        for (int i = 0; i < _chairs.Length; i++)
        {
            Data._chairList.Add(_chairs[i]);
        }
        // Еда

        for (int i = Data.foodList.Count - 1; i >= 0; i--)
        {
            Data.foodList.RemoveAt(i);
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

    public Food CreateFood(string _foodName)
    {
        switch (_foodName)
        {
            case "Pie":
                for (int i = 0; i < 20; i++)
                {
                    if (_pieList[i].isInGame == false)
                    {
                        AddToQueue(_pieList[i]);
                        return _pieList[i];
                    }
                }
                break;
            case "Sushi":
                for (int i = 0; i < 20; i++)
                {
                    if (_sushiList[i].isInGame == false)
                    {
                        AddToQueue(_sushiList[i]);
                        return _sushiList[i];
                    }
                }
                break;
            case "Coffee":
                for (int i = 0; i < 20; i++) 
                {
                    if (_coffeeList[i].isInGame == false)
                    {
                        AddToQueue(_coffeeList[i]);
                        return _coffeeList[i];
                    }
                }
                break;
            case "Cocktail":
                for (int i = 0; i < 20; i++)
                {
                    if (_cocktailList[i].isInGame == false)
                    {
                        AddToQueue(_cocktailList[i]);
                        return _cocktailList[i];
                    }
                }
                break;
            case "Eggs":
                for (int i = 0; i < 20; i++)
                {
                    if (_eggsList[i].isInGame == false)
                    {
                        AddToQueue(_eggsList[i]);
                        return _eggsList[i];
                    }
                }
                break;
            case "Fish":
                for (int i = 0; i < 20; i++)
                {
                    if (_fishList[i].isInGame == false)
                    {
                        AddToQueue(_fishList[i]);
                        return _fishList[i];
                    }
                }
                break;
            case "IceCream":
                for (int i = 0; i < 20; i++)
                {
                    if (_iceCreamList[i].isInGame == false)
                    {
                        AddToQueue(_iceCreamList[i]);
                        return _iceCreamList[i];
                    }
                }
                break;
            case "Mojito":
                for (int i = 0; i < 20; i++)
                {
                    if (_mojitoList[i].isInGame == false)
                    {
                        AddToQueue(_mojitoList[i]);
                        return _mojitoList[i];
                    }
                }
                break;
            case "OrangeJuice":
                for (int i = 0; i < 20; i++)
                {
                    if (_orangeJuiceList[i].isInGame == false)
                    {
                        AddToQueue(_orangeJuiceList[i]);
                        return _orangeJuiceList[i];
                    }
                }
                break;
            case "Pizza":
                for (int i = 0; i < 20; i++)
                {
                    if (_pizzaList[i].isInGame == false)
                    {
                        AddToQueue(_pizzaList[i]);
                        return _pizzaList[i];
                    }
                }
                break;
            case "Soup":
                for (int i = 0; i < 20; i++)
                {
                    if (_soupList[i].isInGame == false)
                    {
                        AddToQueue(_soupList[i]);
                        return _soupList[i];
                    }
                }
                break;
        }
        return _soupList[0];
    }

    private void AddToQueue(Food _food)
    {
        _foodInQue.Add(_food);
        _food.isInGame = true;
        PlaceOnEmptySlots(_food.transform);

    }

    public void MoveQueue(Transform _food,Vector3 _freePos, int _spot)
    {
        _foodInQue.Remove(_food.gameObject.GetComponent<Food>());
        FoodOnBar[_spot] = false;

        for (int i = 0; i < _foodInQue.Count; i++)
        {
            if (_foodInQue[i].isOnTable == false)
            {
                _foodInQue[i].transform.position = _freePos;
                _foodInQue[i].isOnTable = true;
                FoodOnBar[_spot] = true;
                break;
            }
        }
    }

    public emptyDish CheckForEmptyFood(string _food, Vector3 _position)
    {
        switch (_food)
        {
            case "Pie":
                for (int i = 0; i < _emptyPieList.Count; i++)
                {
                    if (_emptyPieList[i].isInGame == false)
                    {
                        _emptyPieList[i].transform.position = _position;
                        _emptyPieList[i].isInGame = true;
                        return _emptyPieList[i];
                    }
                }
                break;
            case "Sushi":
                for (int i = 0; i < _emptySushiList.Count; i++)
                {
                    if (_emptySushiList[i].isInGame == false)
                    {
                        _emptySushiList[i].transform.position = _position;
                        _emptySushiList[i].isInGame = true;
                        return _emptySushiList[i];
                    }
                }
                break;
            case "Coffee":
                for (int i = 0; i < _emptyCoffeeList.Count; i++)
                {
                    if (_emptyCoffeeList[i].isInGame == false)
                    {
                        _emptyCoffeeList[i].transform.position = _position;
                        _emptyCoffeeList[i].isInGame = true;
                        return _emptyCoffeeList[i];
                    }
                }
                break;
            case "Cocktail":
                for (int i = 0; i < _emptCocktailList.Count; i++)
                {
                    if (_emptCocktailList[i].isInGame == false)
                    {
                        _emptCocktailList[i].transform.position = _position;
                        _emptCocktailList[i].isInGame = true;
                        return _emptCocktailList[i];
                    }
                }
                break;
            case "Eggs":
                for (int i = 0; i < _emptyEggsList.Count; i++)
                {
                    if (_emptyEggsList[i].isInGame == false)
                    {
                        _emptyEggsList[i].transform.position = _position;
                        _emptyEggsList[i].isInGame = true;
                        return _emptyEggsList[i];
                    }
                }
                break;
            case "Fish":
                for (int i = 0; i < _emptyFishList.Count; i++)
                {
                    if (_emptyFishList[i].isInGame == false)
                    {
                        _emptyFishList[i].transform.position = _position;
                        _emptyFishList[i].isInGame = true;
                        return _emptyFishList[i];
                    }
                }
                break;
            case "IceCream":
                for (int i = 0; i < _emptyIceCreamList.Count; i++)
                {
                    if (_emptyIceCreamList[i].isInGame == false)
                    {
                        _emptyIceCreamList[i].transform.position = _position;
                        _emptyIceCreamList[i].isInGame = true;
                        return _emptyIceCreamList[i];
                    }
                }
                break;
            case "Mojito":
                for (int i = 0; i < _emptyMojitoList.Count; i++)
                {
                    if (_emptyMojitoList[i].isInGame == false)
                    {
                        _emptyMojitoList[i].transform.position = _position;
                        _emptyMojitoList[i].isInGame = true;
                        return _emptyMojitoList[i];
                    }
                }
                break;
            case "OrangeJuice":
                for (int i = 0; i < _emptyOrangeJuiceList.Count; i++)
                {
                    if (_emptyOrangeJuiceList[i].isInGame == false)
                    {
                        _emptyOrangeJuiceList[i].transform.position = _position;
                        _emptyOrangeJuiceList[i].isInGame = true;
                        return _emptyOrangeJuiceList[i];
                    }
                }
                break;
            case "Pizza":
                for (int i = 0; i < _emptyPizzaList.Count; i++)
                {
                    if (_emptyPizzaList[i].isInGame == false)
                    {
                        _emptyPizzaList[i].transform.position = _position;
                        _emptyPizzaList[i].isInGame = true;
                        return _emptyPizzaList[i];
                    }
                }
                break;
            case "Soup":
                for (int i = 0; i < _emptySoupList.Count; i++)
                {
                    if (_emptySoupList[i].isInGame == false)
                    {
                        _emptySoupList[i].transform.position = _position;
                        _emptySoupList[i].isInGame = true;
                        return _emptySoupList[i];
                    }
                }
                break;
                
        }
        return _emptySoupList[0];
    }

    private void PlaceOnEmptySlots(Transform _food)
    {
        for (int i = 0; i < 4; i++)
        {
            if (FoodOnBar[i] == false)
            {
                _food.position = _deskPos1;
                _food.GetComponent<Food>().isOnTable = true;
                FoodOnBar[i] = true;
                break;
            }
            _deskPos1.x -= 1f;
        }
        _deskPos1 = _deskPos;
    }

    public void RemoveFromQueue(Food _food)
    {
        _foodInQue.Remove(_food);
    }

    public void BackToStartPositions()
    {
        for (int i = 0; i < _sittingPeople.Count; i++)
        {
            _sittingPeople[i].GetComponent<SittingHuman>().BackToPool();
        }

        for (int i = 0; i < _pieList.Count; i++)
        {
            _pieList[i].BackToPool();
            _sushiList[i].BackToPool(); 
            _coffeeList[i].BackToPool();
            _cocktailList[i].BackToPool(); 
            _eggsList[i].BackToPool(); 
            _fishList[i].BackToPool();
            _iceCreamList[i].BackToPool();
            _mojitoList[i].BackToPool();
            _orangeJuiceList[i].BackToPool();
            _pizzaList[i].BackToPool();
            _soupList[i].BackToPool();
        }
    }

    public IEnumerator ShowMessage()
    {
        _message.enabled = true;
        yield return new WaitForSeconds(2f);
        _message.enabled = true;
    }
}
