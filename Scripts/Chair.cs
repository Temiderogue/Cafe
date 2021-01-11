using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chair : MonoBehaviour
{
    [SerializeField] private Table _table;
    public bool isTableBought;
    public bool isBought;
    public Image Stance;
    public int Cost;
    public Transform Point;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private SceneLoader _sceneLoader;

    // покупка стула
    private void OnMouseDown()
    {
        if (Data.Diamonds > Cost && isBought == false && Data.isInShop && !Data.isBuying && isTableBought)
        {
            Data.Chair = GetComponent<Chair>();
            Data.Image = Stance;
            _sceneLoader.ShowChairPrice();
        }
    }
}
