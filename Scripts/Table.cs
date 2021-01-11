using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    public Spawner Spawner;
    public int PeopleOntable;
    public List<SittingHuman> People = new List<SittingHuman>();
    public bool isBought;
    public Image Stance;
    public int Cost;

    [SerializeField] private Chair[] _chairs;
    [SerializeField] private Image[] _chairsImages;

    private void Start()
    {
        Spawner = FindObjectOfType<Spawner>();
    }

    // при нажатии срабатывает проверка условий и покупается стол, если все условия выполнены
    private void OnMouseDown()
    {
        if (Data.Diamonds > Data.TablePrice && isBought == false && Data.isInShop && !Data.isBuying)
        {
            Data.ChangeDiamonds(Data.TablePrice);
            isBought = true;
            Stance.sprite = _gameManager.BoughtSprite;
            Data.TablesBought++;
            Data.CheckPrice();
            _gameManager.RefreshMoney();
            for (int i = 0; i < _chairs.Length; i++)
            {
                _chairs[i].isTableBought = true;
                _chairsImages[i].enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SittingHuman") // добавление человека в пул стола
        {
            People.Add(other.gameObject.GetComponent<SittingHuman>());
            People[PeopleOntable].Table = GetComponent<Table>();
            People[PeopleOntable].Number = PeopleOntable;
            People[PeopleOntable]._menuCanvas.enabled = true;
            PeopleOntable++;
        }
        else if (other.tag == "Menu") // проверка на наличие людей за столом, которые ждут меню 
        {
            for (int i = 0; i < People.Count; i++)
            {
                if (People[i].MenuGaven == false)
                {
                    People[i].MenuGaven = true;
                    People[i]._menuCanvas.enabled = false;
                    other.gameObject.transform.position = Data._menuPosition;
                    break;
                }
            }

            other.gameObject.transform.position = Data._menuPosition;
        }
        else if (other.tag == "Food") // проверка людей которые ожидают заказ и выставление еды
        {
            Food food = other.GetComponent<Food>();
            for (int i = 0; i < People.Count; i++)
            {
                if (People[i].OrderName == food.TypeOfFood && !People[i].OrderGaven)
                {
                    if (People[i].HaveEmptyDish)
                    {
                        People[i].OrderGaven = true;
                        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;food.isInGame = false;
                        other.gameObject.transform.position = People[i]._foodPlace;
                        other.gameObject.GetComponent<BoxCollider>().enabled = false;
                        Data.ChangeScore(-10);
                        StartCoroutine(Spawner.ShowMessage());
                        StartCoroutine(eat(food, People[i]._foodPlace));
                        break;
                    }
                    Data.ChangeScore(5);
                    Data.AddTime(food.BoughtNumber);
                    People[i].OrderGaven = true;
                    other.gameObject.transform.position = People[i]._foodPlace;
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.GetComponent<BoxCollider>().enabled = false;
                    StartCoroutine(eat(food, People[i]._foodPlace));
                    break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EmptyDish") // убираем пустую посуду у человека
        {
            for (int i = 0; i < People.Count; i++)
            {
                if (People[i].HaveEmptyDish)
                {
                    People[i].HaveEmptyDish = false;
                }
            }
        }
    }

    // Через 5 секунд, после того как заказ выдан мы возвращаем еду в пул и меняем ее на пустую посуду
    private IEnumerator eat(Food _food, Vector3 _foodPlace)
    {
        yield return new WaitForSeconds(5f);
        _food.transform.position = _food.PoolPosition;
        _food.isInGame = false;
        _food.GetComponent<BoxCollider>().enabled = true;
        _food.GetComponent<Rigidbody>().isKinematic = true;
        _food.GetComponent<Mover>().CanDrag = false;
        //  Замена на пустую порцию
        Spawner.CheckForEmptyFood(_food.TypeOfFood,_foodPlace);
    }
}
