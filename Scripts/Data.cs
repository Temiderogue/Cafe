using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Data 
{
    public static int Lvl = 1;
    public static int Score = 100;
    public static int Money = 1000;
    public static int Diamonds = 100;

    public static int EarnedMoney;

    public static int SpawnDuration = 5;

    public static int WaitingTime =8;

    public static int PeopleInCafeAmount  = 0;


    public static int PeopleInCafe;
    public static int PeopleOnTable;

    public static int TablesBought = 2;
    public static int TablePrice = 25;

    public static int moneyFoodCost = 4000;
    public static int diamondFoodCost = 20;

    public static int moneyChairCost = 1000;
    public static int diamondChairCost = 5;

    public static int BoughtChairAmount = 5;
    public static int BoughtFoodAmount = 5;

    public static float TimeLeft = 30;

    public static int WaiterSAmount = 1;

    public static bool isItemDragged = false;
    public static bool CanDragEmptyFood = true;
    public static bool LvlEnd;
    public static bool isInShop;
    public static bool isBuying;
    public static bool isInGameWithTime;
    public static bool isInGameWithoutTime;
    public static bool isFirstGame = true;
    public static bool isScoreBoardLoaded = false;

    public static List<Food> foodList = new List<Food>();
    public static List<Food> BouhgtFood = new List<Food>();
    public static List<emptyDish> emptyFoodList = new List<emptyDish>();

    public static FoodForSale CaseWithFood;
    public static Food Food;
    public static Chair Chair;
    public static Image Image;

    public static bool SoundStance;
    public static bool MusicStance;

    public static Vector3 WaiterPosition = new Vector3(3.23f,0f,4.43f);
    public static Vector3 WaiterPoolPosition = new Vector3(0f, 0f, 40f);

    public static Vector3 _spawnPosiition1 = new Vector3(0f, 1.5f, 22f);
    public static Vector3 _spawnPosiition2 = new Vector3(3f, 1.5f, 22f);

    
    public static Vector3 _doorPosition = new Vector3(0f, 0f, -3.4f);
    public static Vector3 _menuPosition = new Vector3(3f, 2f, 4.4f);

    public static List<Table> _tableList= new List<Table>();
    public static List<Chair> _chairList = new List<Chair>();

    public static string[] FoodTypes = {"Pie","Sushi","Coffee","Cocktail","Eggs","Fish","IceCream", "Mojito", "OrangeJuice", "Pizza", "Soup"};

    public static Dictionary<string, string> LocalScoreList = new Dictionary<string, string>();
    public static List<Action> OrderList = new List<Action>();


    public static void ChangeScore(int value)
    {
        Score += value;
        if (Score < 0)
        {
            Score = 0;
        }
        else if (Score >= 100)
        {
            LvlEnd = true;
            Score = 0;
            Lvl++;
        }
    }

    public static void ChangeDiamonds(int value)
    {
        Diamonds -= value;
        if (Diamonds < 0)
        {
            Diamonds = 0;
        }
    }

    public static void CostFoodUp() // повышение цен для еды
    {
        switch (BoughtFoodAmount)
        {
            case 6:
                Food.BoughtNumber = 6;
                diamondFoodCost = 30;
                moneyFoodCost = 6000;
                break;
            case 7:
                Food.BoughtNumber = 7;
                diamondFoodCost = 40;
                moneyFoodCost = 8000;
                break;
            case 8:
                Food.BoughtNumber = 8;
                diamondFoodCost = 50;
                moneyFoodCost = 10000;
                break;
            case 9:
                Food.BoughtNumber = 9;
                diamondFoodCost = 60;
                moneyFoodCost = 12000;
                break;
            case 10:
                Food.BoughtNumber = 10;
                diamondFoodCost = 70;
                moneyFoodCost = 14000;
                break;
            case 11:
                Food.BoughtNumber = 11;
                break;
        }
    }

    public static void CostChairUp() // повышение цен для стульев
    {
        switch (BoughtChairAmount)
        {
            case 6:
                diamondChairCost = 30;
                moneyChairCost = 6000;
                break;
            case 7:
                diamondChairCost = 40;
                moneyChairCost = 8000;
                break;
            case 8:
                diamondChairCost = 50;
                moneyChairCost = 10000;
                break;
            case 9:
                diamondChairCost = 60;
                moneyChairCost = 12000;
                break;
            case 10:
                diamondChairCost = 70;
                moneyChairCost = 14000;
                break;
            case 11:
                break;
        }
    }

    public static void CheckPrice()
    {
        switch (TablesBought)
        {
            case 3:
                TablePrice = 250;
                break;
            case 4:
                TablePrice = 2500;
                break;
            case 5:
                TablePrice = 25000;
                break;
        }
    } // повышение цен для столов

    public static void AddTime(int BoughtNumber)
    {
        switch (BoughtNumber)
        {
            case 6:
                TimeLeft += 6;
                break;
            case 7:
                TimeLeft += 7;
                break;
            case 8:
                TimeLeft += 8;
                break;
            case 9:
                TimeLeft += 9;
                break;
            case 10:
                TimeLeft += 10;
                break;
            case 11:
                TimeLeft += 11;
                break;
            default:
                TimeLeft += 5;
                break;
        }
    } // Добавление времени к таймеру при удачном заказе

    public static void AddToLocalScoreList(string score)
    {
        bool isHaveScore = false;
        foreach (string _score in LocalScoreList.Keys)
        {
            if (_score == score)
            {
                isHaveScore = true;
                break;
            }
        }

        if (!isHaveScore)
        {
            LocalScoreList.Add(score, System.DateTime.Now.ToString("MM/dd/yyyy"));
        }
    }
}
