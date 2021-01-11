using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private bool IsInGame;
    private Camera _camera;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Canvas _mainMenu;
    [SerializeField] private Canvas _shopMenu;
    [SerializeField] private Canvas _settingsMenu;
    [SerializeField] private Canvas _showFoodPrice;
    [SerializeField] private Canvas _showChairPrice;
    [SerializeField] private Canvas _afterGameMenu;
    [SerializeField] private Canvas _netLostWindow;
    [SerializeField] private Canvas _wrongVersionWindow;
    [SerializeField] private GameObject _netList;
    [SerializeField] private GameObject _localList;
    [SerializeField] private Text _moneyFoodButton;
    [SerializeField] private Text _diamondFoodButton;

    [SerializeField] private Text _moneyChairButton;
    [SerializeField] private Text _diamondChairButton;
    [SerializeField] private Spawner _spawner;

    [SerializeField] private Chair[] _chairs;
    [SerializeField] private Table[] _tables;

    [SerializeField] private GameObject[] _walls;

    [SerializeField] private GameObject[] _foodForSale;
    //[SerializeField] private Image[] _foodForSaleImage;

    [SerializeField] private GameObject _wallColliders;

    [SerializeField] private List<Image> _chairMarks = new List<Image>();
    [SerializeField] private List<Image> _tablesMarks = new List<Image>();
    [SerializeField] private GameObject[] _buttons;

    [SerializeField] private Button _gameButton;
    [SerializeField] private Button _shopButton;

    [SerializeField] private Image _gameImage;
    [SerializeField] private Image _shopImage;

    [SerializeField] private GameObject _panelPrefab;

    //[SerializeField] private UserData _userData;
    //[SerializeField] private Net _net;

    private Vector3 _listPos;

    private void Start() // отключение значков покупки
    {
        _wallColliders.SetActive(false);
        _showFoodPrice.enabled = false;
        _showChairPrice.enabled = false;
        _settingsMenu.enabled = false;
        _netList.SetActive(false);
        _localList.SetActive(false);
        _netLostWindow.enabled = false;
        _wrongVersionWindow.enabled = false;

        for (int i = 0; i < Data._chairList.Count; i++)
        {
            if (Data._chairList[i].isBought)
            {
                _chairs[i].isBought = true;
            }

            if (Data._chairList[i].isTableBought)
            {
                _chairs[i].isTableBought = true;
            }
        }

        for (int i = 0; i < Data._tableList.Count; i++)
        {
            if (Data._tableList[i].isBought)
            {
                _tables[i].isBought = true;
            }
        }

        for (int i = 0; i < _chairs.Length; i++)
        {
            _chairMarks.Add(_chairs[i].Stance);
            _chairMarks[i].enabled = false;
        }

        for (int i = 0; i < _tables.Length; i++)
        {
            _tablesMarks.Add(_tables[i].Stance);
            _tablesMarks[i].enabled = false;
        }

        for (int i = 0; i < Data.foodList.Count; i++)
        {
            _foodForSale[i].SetActive(true);
            if (Data.foodList[i].isBought)
            {
                _spawner.FoodForSale[i].isBought = true;
            }
            _foodForSale[i].SetActive(false);
        }


        _shopMenu.enabled = false;
        _mainMenu.enabled = true;
        _camera = Camera.main;
        _cameraController.enabled = false;
    }

    public void LoadMenu() // загрузка меню
    {

        //_spawner.BackToStartPositions();
        _afterGameMenu.enabled = false;
        for (int i = 0; i < _walls.Length; i++)
        {
            _walls[i].SetActive(true);
        }
        //_camera.orthographic = false;
        Data.isInShop = false;
        _shopMenu.enabled = false;
        _mainMenu.enabled = true;
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, new Vector3(15.34f, 26.28f, -17.58f), 5f);
        _camera.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(45f, -35.7f, 0f)), 1);



        for (int i = 0; i < _chairs.Length; i++)
        {
            if (!_chairs[i].isBought)
            {
                _chairs[i].gameObject.SetActive(false);
            }
            _chairMarks[i].enabled = false;
        }

        for (int i = 0; i < _tables.Length; i++)
        {
            if (!_tables[i].isBought)
            {
                _tables[i].gameObject.SetActive(false);
            }
            _tablesMarks[i].enabled = false;
        }

    }

    public void LoadDefaultGame() // загрузка обычной игры с временем
    {
        _spawner.RefreshList();
        Data.TimeLeft = 30f;
        Data.isFirstGame = false;
        Data.isInGameWithoutTime = false;
        Data.isInGameWithTime = true;
        SceneManager.LoadScene(1);
    }

    public void LoadGameWihoutTime() // загузка игры без времени
    {
        _spawner.RefreshList();
        Data.isInGameWithoutTime = true;
        SceneManager.LoadScene(2);
    }

    public void LoadShop() // загрузка магазина
    {
        Data.isInShop = true;
        _shopMenu.enabled = true;
        _mainMenu.enabled = false;
        _camera.transform.position = new Vector3(0f, 40f, 0f);
        _camera.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(90, 0, 0)), 5f);

        for (int i = 0; i < _walls.Length; i++)
        {
            _walls[i].SetActive(false);
        }

        for (int i = 0; i < _foodForSale.Length; i++)
        {
            _foodForSale[i].SetActive(true);
        }

        for (int i = 0; i < _chairs.Length; i++)
        {
            _chairs[i].gameObject.SetActive(true);
            if (_chairs[i].isTableBought)
            {
                _chairMarks[i].enabled = true;
            }

            if (_chairs[i].isBought)
            {
                _chairMarks[i].sprite = _gameManager.BoughtSprite;
            }
        }

        for (int i = 0; i < _tables.Length; i++)
        {
            _tables[i].gameObject.SetActive(true);
            _tablesMarks[i].enabled = true;
            if (_tables[i].isBought)
            {
                _tablesMarks[i].sprite = _gameManager.BoughtSprite;
            }
        }
    }

    public void LoadSettings() // загрузка меню с настройками
    {
        _mainMenu.enabled = false;
        _settingsMenu.enabled = true;
    }

    public void LoadMenuFromSettings() // загрузка главного меню из меню настроек
    {
        _settingsMenu.enabled = false;
        _mainMenu.enabled = true;
    }

    public void ShowPrice(FoodForSale _choosedFood, Food _food)  // меню с покупкой еды
    {
        Data.Food = _food;
        Data.CaseWithFood = _choosedFood;
        _showFoodPrice.enabled = true;
        Data.isBuying = true;
        _moneyFoodButton.text = Data.moneyFoodCost.ToString();
        _diamondFoodButton.text = Data.diamondFoodCost.ToString();
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetActive(false);
        }
    }

    public void ShowChairPrice() // меню с покупкой стула
    {
        Data.isBuying = true;
        _showChairPrice.enabled = true;
        _moneyChairButton.text = Data.moneyChairCost.ToString();
        _diamondChairButton.text = Data.diamondChairCost.ToString();
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetActive(false);
        }
    }

    public void HidePrice() // при нажатии на темный фон прячем меню выбора оплаты и возвращаемся в меню магазина
    {
        Data.Food = null;
        Data.CaseWithFood = null;
        _showFoodPrice.enabled = false;
        _showChairPrice.enabled = false;
        Data.isBuying = false;
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetActive(true);
        }
    }

    public void InternetLost()
    {
        _mainMenu.enabled = false;
        _netLostWindow.enabled = true;
    }

    public void LoadWithInternet()
    {
        _mainMenu.enabled = true;
        _netLostWindow.enabled = false;
        _shopButton.enabled = true;
        _gameButton.enabled = true;
    }

    public void LoadVersionWindow()
    {
        _wrongVersionWindow.enabled = true;
    }

    public void LoadGameWithoutInternet()
    {
        //вырубаем все ненужное

        _mainMenu.enabled = true;
        _netLostWindow.enabled = false;

        _shopButton.enabled = false;
        _gameButton.enabled = false;

        var tempColor1 = _gameImage.color;
        tempColor1.a = 0.5f;
        _gameImage.color = tempColor1;

        var tempColor2 = _shopImage.color;
        tempColor2.a = 0.5f;
        _shopImage.color = tempColor2;
    }

    public void LoadTopList()
    {
        _mainMenu.enabled = false;
        _netList.SetActive(true);
        _gameManager.RefreshLists();
    }

    public void LoadNetScoreBoard()
    {
        _netList.SetActive(true);
        _localList.SetActive(false);
    }

    public void LoadLocalScoreBoard()
    {
        _netList.SetActive(false);
        _localList.SetActive(true);
    }

    public void LoadMenuFromBoard()
    {
        _mainMenu.enabled = true;
        _netList.SetActive(false);
        _localList.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
