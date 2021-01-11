using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Waiter : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navAgent;
    public NavMeshAgent _waiterWithFoodNav;
    private Vector3 _mainPosition = new Vector3(3.23f,0.01f,4.43f);
    private Vector3 _poolPosition = new Vector3(0, 0, 40);
    private Vector3 _foodPostition = new Vector3(0.366f,1.81f,0.543f); 
    private Quaternion _rotation = Quaternion.Euler(0f,180f,0f);
    private bool isGoingtoGiveOrder;
    private bool isGoingtomainPosition;
    private bool isGoingforFood;
    private bool isGoingToGiveFood;
    private bool isWaiterGoBack;
    private bool isWalkingForDish;
    private bool isWalkingBackWithDish;
    private Spawner _spawner;
    private string OrderName = "";
    private int _randomNum;
    private Transform _foodToTake;
    private SittingHuman _client;
    private string foodName;
    private emptyDish _dish;
    //private List<Action> _toDoList = new List<Action>();
    private Action action;
    private ButtonForWaiter _button;

    //пока не точно
    private void Start()
    {
        //_mainPosition = transform.position;
        _spawner = FindObjectOfType<Spawner>();
        _button = FindObjectOfType<ButtonForWaiter>();
    }

    private void Update()
    {
        // Идет давать заказ
        if (isGoingtoGiveOrder && !_navAgent.pathPending && _navAgent.remainingDistance < 0.2f)
        {
            _navAgent.SetDestination(_mainPosition);
            isGoingtomainPosition = true;
            _randomNum = Random.Range(0, Data.BouhgtFood.Count);
            OrderName = Data.BouhgtFood[_randomNum].TypeOfFood;
            Food _food =_spawner.CreateFood(OrderName);
            Action _action = new Action(_food.transform, action.Client, "GiveFood");
            Data.OrderList.Add(_action);
            isGoingtoGiveOrder = false;
        }

        // возвращается обратно
        if (isGoingtomainPosition && !_navAgent.pathPending && _navAgent.remainingDistance < 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _rotation,2f);
            isGoingtoGiveOrder = false;
            isGoingtomainPosition = false;
            _button.Interactable = true;
        }

        // Идет к стойку и берет еду
        if (isGoingforFood && !_waiterWithFoodNav.pathPending && _waiterWithFoodNav.remainingDistance < 0.1f)
        {
            action.Food.position = _foodPostition;
            action.Food.SetParent(_waiterWithFoodNav.transform,false);
            action.Food.rotation = Quaternion.Euler(0, 0, 0);
            
            _waiterWithFoodNav.SetDestination(action.Client.transform.position);
            isGoingforFood = false;
            isGoingToGiveFood = true;
        }

        // Идет с едой к клиенту
        if (isGoingToGiveFood && !_waiterWithFoodNav.pathPending && _waiterWithFoodNav.remainingDistance < 0.1f)
        {
            _waiterWithFoodNav.SetDestination(_mainPosition);
            action.Food.transform.parent = null;
            action.Food.rotation = Quaternion.Euler(0f,0f,0f);
            action.Food.transform.position = action.Client.FoodPlaceTransform.position;
            // корутина где ставится таймер на 5 секунд, заменяется посуда, человек уходит
            StartCoroutine(PlaceEmptyFood(action.Client, action.Food.GetComponent<Food>(), action.Food.transform.position));


            isGoingToGiveFood = false;
            isWaiterGoBack = true;
        }

        // Идет обратно 
        if (isWaiterGoBack && !_waiterWithFoodNav.pathPending && _waiterWithFoodNav.remainingDistance < 0.1f)
        {
            _waiterWithFoodNav.enabled = false;
            _waiterWithFoodNav.transform.position = _poolPosition;

            _navAgent.transform.position = _mainPosition;
            _navAgent.enabled = true;
            isWaiterGoBack = false;
            _button.Interactable = true;
        }

        // Идет за пустой посудой
        if (isWalkingForDish && !_waiterWithFoodNav.pathPending && _waiterWithFoodNav.remainingDistance < 0.1f)
        {
            action.Dish.transform.position = _foodPostition;
            action.Dish.transform.SetParent(_waiterWithFoodNav.transform, false);
            action.Dish.transform.rotation = Quaternion.Euler(0, 0, 0);
            _waiterWithFoodNav.SetDestination(_mainPosition);
            
            isWalkingForDish = false;
            isWalkingBackWithDish = true;

            // освобождаем место для нового клиента и запускаем корутину
            
            _spawner._freeSpots.Add(action.Client.Chair);
            _spawner._notFreeSpots.Remove(action.Client.Chair);
            Data.PeopleInCafe--;
            StartCoroutine(_spawner.PlaceHuman());

        }

        // Идет обратно
        if (isWalkingBackWithDish && !_waiterWithFoodNav.pathPending && _waiterWithFoodNav.remainingDistance < 0.1f)
        {
            _waiterWithFoodNav.enabled = false;
            _waiterWithFoodNav.transform.position = _poolPosition;
            _navAgent.transform.position = _mainPosition;
            _navAgent.enabled = true;
            action.Dish.transform.parent = null;
            action.Dish.transform.position = action.Dish.PoolPosition;
            action.Dish.isInGame = false;
            isWalkingBackWithDish = false;
            _button.Interactable = true;
        }
    }

    public void TakeOrder()
    {
        for (int i = 0; i < Data.OrderList.Count; i++)
        {
            if (!Data.OrderList[i].InProcess)
            {
                switch (Data.OrderList[i].Type)
                {
                    case "GiveMenu":
                        _button.Interactable = false;
                        isGoingtoGiveOrder = true;
                        _navAgent.SetDestination(Data.OrderList[i].HumanLocation);
                        _client = Data.OrderList[i].Client;
                        Data.OrderList[i].InProcess = true;
                        action = Data.OrderList[i];
                        break;
                    case "GiveFood":
                        _button.Interactable = false;
                        isGoingforFood = true;

                        //с меню в пул и отключить нэвмеш а с тарелкой выпустить и направить
                        _navAgent.enabled = false;
                        _navAgent.transform.position = _poolPosition;
                        _waiterWithFoodNav.transform.position = _mainPosition;
                        _waiterWithFoodNav.enabled = true;
                        _waiterWithFoodNav.SetDestination(Data.OrderList[i].Food.position);

                        _foodToTake = Data.OrderList[i].Food;
                        _client = Data.OrderList[i].Client;
                        Data.OrderList[i].InProcess = true;
                        action = Data.OrderList[i];
                        break;
                    case "TakeEmptyDish":
                        _button.Interactable = false;
                        _navAgent.enabled = false;
                        _navAgent.transform.position = _poolPosition;
                        _waiterWithFoodNav.transform.position = _mainPosition;
                        _waiterWithFoodNav.enabled = true;
                        _waiterWithFoodNav.SetDestination(Data.OrderList[i].Dish.transform.position);
                        _dish = Data.OrderList[i].Dish;
                        Data.OrderList[i].InProcess = true;
                        isWalkingForDish = true;
                        action = Data.OrderList[i];

                        break;
                    default:
                        break;
                }
                break;
            }
        }
    }

    private IEnumerator PlaceEmptyFood(SittingHuman client,Food food, Vector3 foodPlace)
    {
        yield return new WaitForSeconds(5f);
        //заменить посуду на пустую и отправить человека обратно, добавить Action с убиранием еды
        food.transform.position = food.PoolPosition;
        emptyDish dish = _spawner.CheckForEmptyFood(food.TypeOfFood, foodPlace);
        Action action = new Action(dish, "TakeEmptyDish", client); ;
        Data.OrderList.Add(action);
        client.GoAway();
    }

    private IEnumerator waitForfree()
    {
        Debug.Log("началось");
        yield return new WaitUntil(() => _spawner._freeSpots.Count > 0);

        Debug.Log("своодных мест:" + _spawner._freeSpots.Count);
        
        
    }
}
