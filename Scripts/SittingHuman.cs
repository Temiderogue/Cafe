using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SittingHuman : MonoBehaviour
{
    [SerializeField] private Text _foodText;
    [SerializeField] private Image _field;
    public Canvas _menuCanvas;
    public Canvas _foodCanvas;
    public Camera Camera;
    public GameObject StandingHuman;
    public NavMeshAgent NavStandingHuman;
    public Spawner Spawner;
    public Chair Chair;
    public Vector3 Way;
    public Vector3 SitLocation;
    public Vector3 Rotation;
    public int Num;

    public Table Table;
    public int Number;

    public bool isGoing = false;
    public bool isGoingAway = false;

    public bool HaveEmptyDish = false;
    public bool MenuGaven = false;
    public bool OrderGaven = false;

    public string OrderName = "";

    private float _timeLeft;
    private int _randomNum;
    public Transform FoodPlaceTransform;
    public Vector3 _foodPlace;

    private IEnumerator _waitMenu;
    private IEnumerator _waitFood;
    private IEnumerator _checkForLate;

    private void Start()
    {
        Spawner = FindObjectOfType<Spawner>();
        Camera = Camera.main;
        _timeLeft = Data.WaitingTime;
        _menuCanvas.enabled = true;
        _foodCanvas.enabled = false;
        _foodText.enabled = false;
        
        //_waitMenu = WaitForMenu();
        //_waitFood = WaitForFood();
        //_checkForLate = CheckForLate();
        //StartCoroutine(_waitMenu);
    }

    private void Update()
    {
        if (isGoing == true && !NavStandingHuman.pathPending && NavStandingHuman.remainingDistance < 0.2f)
        {
            _foodPlace = FoodPlaceTransform.position;
            TakeOrderAfterSit();
            Data.PeopleOnTable++;
            isGoing = false;
        }
    }

    public IEnumerator WaitForMenu() // берем новый заказ когда выдано меню
    {
        while (!MenuGaven)
        {
            yield return null;
        }
        _menuCanvas.enabled = false;
        //_foodPlace = _foodPlaceTransform.position;
        yield return new WaitForSeconds(Random.Range(3,10));
        //GetNewOrder();
    }

    private IEnumerator WaitForFood() // когда выдается заказ мы выключаем таймер и человек начинает есть, через 5 секунд появляется новый заказ
    {
        while (!OrderGaven)
        {
            yield return null;
        }
        
        _foodCanvas.enabled = false;
        StopCoroutine(_checkForLate);
        
        yield return new WaitForSeconds(5f);
        HaveEmptyDish = true;
        OrderGaven = false;

        GoAway();
        //GetNewOrder();
    }

    private IEnumerator CheckForLate() // если время вышло то человек выходит из кафе
    {
        
        yield return new WaitForSeconds(Data.WaitingTime);

        // GO AWAY

        GoAway();
    }

    public void BackToPool() // возвращение человека обратно в пулл, нужно при загрузке сцены
    {
        _foodCanvas.enabled = false;
        OrderGaven = false;
        MenuGaven = false;
        HaveEmptyDish = false;
        StandingHuman.transform.position = Data._spawnPosiition1;
        NavStandingHuman.enabled = false;
        transform.position = Data._spawnPosiition2;

        Spawner._freeSpots.Add(Chair);
        Spawner._notFreeSpots.Remove(Chair);
        Table.People.Remove(GetComponent<SittingHuman>());
        Table.PeopleOntable--;
        Data.PeopleInCafe--;
    }
    /*
    public void GetNewOrder() // рандомное выбираение заказа и появление еды на барной стойке
    {
        //_waitFood = WaitForFood();
        //StartCoroutine(_waitFood);
        //_checkForLate = CheckForLate();
        //StartCoroutine(_checkForLate);
        //_timeLeft = Data.WaitingTime;
        //_foodCanvas.enabled = true;
        //_foodText.enabled = true;
        //_foodText.text = OrderName;

        _randomNum = Random.Range(0, Data.BouhgtFood.Count);
        OrderName = Data.BouhgtFood[_randomNum].TypeOfFood;
        Spawner.CreateFood(OrderName);
    }*/

    public void GoAway()
    {
        //Data.PeopleOnTable--;
        _foodCanvas.enabled = false;
        OrderGaven = false;
        MenuGaven = false;
        HaveEmptyDish = false;
        StandingHuman.GetComponent<Human>().isGoingAway = true;
        //StandingHuman.transform.position = transform.position;
        StandingHuman.transform.position = Way;
        NavStandingHuman.enabled = true;
        NavStandingHuman.SetDestination(Data._doorPosition);
        transform.position = Data._spawnPosiition2;

        //Spawner._freeSpots.Add(Chair);
        //Spawner._notFreeSpots.Remove(Chair);

        OrderName = "";

    }

    private void TakeOrderAfterSit()
    {
        Action action = new Action(Way, "GiveMenu", gameObject.GetComponent<SittingHuman>());
        Data.OrderList.Add(action);

        NavStandingHuman.enabled = false;
        StandingHuman.transform.position = Data._spawnPosiition1;
        transform.position = SitLocation;
        transform.rotation = Quaternion.Euler(Rotation);
    }
}
