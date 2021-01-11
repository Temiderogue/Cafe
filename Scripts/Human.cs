using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _human;
    private Spawner _spawner;
    public GameObject Sit;
    public int Num;

    public Vector3 Way;
    public Vector3 Rotation;

    public bool isGoing = false;
    public bool isGoingAway;

    private bool _isSitting;
    private IEnumerator _startCount;

    private void Start()
    {
        //_startCount = StartCount();
        //StartCoroutine(_startCount);
        _spawner = FindObjectOfType<Spawner>();
    }

    private void Update()
    {
        // Путь к столику
        if (isGoing)
        {
            if (!_human.pathPending && _human.remainingDistance < 0.2f)
            {
                _isSitting = true;
                Data.PeopleOnTable++;
            }
        }

        // Путь к двери
        if (isGoingAway)
        {
            if (!_human.pathPending && _human.remainingDistance < 0.2f)
            {
                _human.enabled = false;
                transform.position = Data._spawnPosiition1;
                isGoingAway = false;
                _spawner.PeopleInGame[Num] = false;

                Data.PeopleInCafe--;
            }
        }
    }

    /*
    private IEnumerator StartCount()
    {
        while (_isSitting == false)
        {
            yield return null;
        }

        // возвращение в пулл и замена модели на сидящего


        Action action = new Action();
        action.HumanLocation = Way;
        action.Type = "GiveMenu";
        action.Client = 
        Data.OrderList.Add(action);



        _human.enabled = false;
        isGoing = false;
        transform.position = Data._spawnPosiition1;
        Sit.transform.position = Way;
        Sit.transform.rotation = Quaternion.Euler(Rotation);
        
        _isSitting = false;
        _startCount = StartCount();
        StartCoroutine(_startCount);
    }
    */
}