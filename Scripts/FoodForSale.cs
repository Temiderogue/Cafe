using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodForSale : MonoBehaviour
{
    [SerializeField] private Food _food;
    [SerializeField] private int Cost;
    [SerializeField] private Image _image;
    [SerializeField] private SceneLoader _sceneLoader;

    // Скрипт для еды, которая находится в магазине

    private void OnMouseDown()
    {
        _sceneLoader.ShowPrice(gameObject.GetComponent<FoodForSale>(), _food);
    }
}
