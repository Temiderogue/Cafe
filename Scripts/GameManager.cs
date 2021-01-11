using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Sprite BoughtSprite;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _lvltext;
    [SerializeField] private Text _tableCost;
    [SerializeField] private Text _message;
    [SerializeField] private Text _money;
    [SerializeField] private Text _diamonds;

    [SerializeField] private Text _sound;
    [SerializeField] private Text _music;

    [SerializeField] private Image _timerBar;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Canvas _afterGameMenu;
    [SerializeField] private Canvas _gameMenu;
    [SerializeField] private Text _earnedMoney;
    [SerializeField] private GameObject _netContent;
    [SerializeField] private GameObject _localContent;

    [SerializeField] private GameObject _panelPrefab;

    [SerializeField] private GameObject[] _medal;
    [SerializeField] private GameObject _localPrefab;
    //[SerializeField] private Net _net;
    private IEnumerator _waitForEndLvl;
    private Vector3 _listPos = new Vector3(0f, 0f, 0f);
    private int i = 0;
    private int _rest = 0;


    private void Start()
    {
        Cursor.visible = true;
        _afterGameMenu.enabled = false;
        _message.enabled = false;
        _diamonds.text = Data.Diamonds.ToString();
        _money.text = Data.Money.ToString();
    }
    
    // Эти функции висят на кнопках покупки за определенную валюту
    public void BuyFoodWithMoney() 
    {
        _rest = Data.Money - Data.moneyFoodCost;
        if (_rest >= 0)
        {
            Data.BoughtFoodAmount++;
            Data.Money -= Data.moneyFoodCost;
            _money.text = Data.Money.ToString();
            Data.CostFoodUp();
            Data.Food.isBought = true;
            _message.enabled = false;
            _sceneLoader.HidePrice();
        }
        else
        {
            _message.enabled = true;
        }
        _rest = 0;
    }

    public void BuyFoodWithDiamonds()
    {
        _rest = Data.Diamonds - Data.diamondFoodCost;
        if (_rest >= 0)
        {
            Data.BoughtFoodAmount++;
            Data.Diamonds -= Data.diamondFoodCost;
            _diamonds.text = Data.Diamonds.ToString();
            Data.CostFoodUp();
            Data.Food.isBought = true;
            _message.enabled = false;
            _sceneLoader.HidePrice();
        }
        else
        {
            _message.enabled = true;
        }
        _rest = 0;
    }

    public void BuyChairWithMoney()
    {
        _rest = Data.Money - Data.moneyChairCost;
        if (_rest >= 0)
        {
            Data.BoughtChairAmount++;
            Data.Money -= Data.moneyChairCost;
            _money.text = Data.Money.ToString();
            Data.CostChairUp();
            Data.Chair.isBought = true;
            Data.Image.sprite = BoughtSprite;
            _message.enabled = false;
            _sceneLoader.HidePrice();
        }
        else
        {
            _message.enabled = true;
        }
        _rest = 0;
    }

    public void BuyChairWithDiamonds()
    {
        _rest = Data.Diamonds - Data.diamondChairCost;
        if (_rest >= 0)
        {
            Data.BoughtFoodAmount++;
            Data.Diamonds -= Data.diamondChairCost;
            _diamonds.text = Data.Diamonds.ToString();
            Data.CostChairUp();
            Data.Chair.isBought = true;
            Data.Image.sprite = BoughtSprite;
            _message.enabled = false;
            _sceneLoader.HidePrice();
        }
        else
        {
            _message.enabled = true;
        }
        _rest = 0;
    }

    public void RefreshMoney() // обновление данных, которые отображаются в магазине
    {
        _diamonds.text = Data.Diamonds.ToString();
        _money.text = Data.Money.ToString();
        _tableCost.text = "Table cost: " + Data.TablePrice.ToString();
    }

    // переключатели музыки
    public void SoundSwitch()
    {
        if (Data.SoundStance)
        {
            // Звук выключается
            Data.SoundStance = false;
            _sound.text = "Sound : off";
        }
        else
        {
            // Звук включается
            Data.SoundStance = true;
            _sound.text = "Sound : on";
        }
    }

    public void MusicSwitch()
    {
        if (Data.MusicStance)
        {
            // Звук выключается
            Data.MusicStance = false;
            _music.text = "Music : off";
        }
        else
        {
            // Звук включается
            Data.MusicStance = true;
            _music.text = "Music : on";
        }
    }

    public void RefreshLists()
    {
        if (Data.isScoreBoardLoaded)
        {
            foreach (Transform item in _localContent.transform)
            {
                Destroy(item.gameObject);
            }
        }

        int i = 0;
        foreach (KeyValuePair<string, string> each in Data.LocalScoreList.OrderBy(each => each.Key))
        {
            Debug.Log("222222222");
            //  ЛОКАЛЬНЫЙ СПИСОК
            GameObject _localPanel = Instantiate(_localPrefab, _listPos, Quaternion.identity);
            //_listPos.y -= 10;
            _localPanel.GetComponent<LocalScore>().SetData(each.Value, Int32.Parse(each.Key));


            switch (i)
            {
                case 0:
                    GameObject _gold = Instantiate(_medal[i], new Vector3(-334, 0, 0), Quaternion.identity);
                    _gold.transform.SetParent(_localPanel.transform, false);
                    break;
                case 1:
                    GameObject _silver = Instantiate(_medal[i], new Vector3(-334, 0, 0), Quaternion.identity);
                    _silver.transform.SetParent(_localPanel.transform, false);
                    break;
                case 2:
                    GameObject _bronze = Instantiate(_medal[i], new Vector3(-334, 0, 0), Quaternion.identity);
                    _bronze.transform.SetParent(_localPanel.transform, false);
                    break;
            }
            i++;
            _localPanel.transform.SetParent(_localContent.transform, false);

        }

        if (Data.isScoreBoardLoaded)
        {
            foreach (Transform item in _netContent.transform)
            {
                Destroy(item.gameObject);
            }
        }
        // НЕ ЛОКАЛЬЫНЙ
        //_net.GetOverallScore();
        
        Data.isScoreBoardLoaded = true;

    }
}
