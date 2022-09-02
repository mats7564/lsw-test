using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] int clotheNumber;
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject sellButton;
    [SerializeField] bool shirt;
    [SerializeField] bool pants;
    [SerializeField] bool shoe;

    private void Start()
    {
       
    }
    private void Update()
    {
        CheckClothesStatus();
    }

    void CheckClothesStatus()
    {        
        if (ChangeOutfit.shirt[clotheNumber] == false) 
        { 
            buyButton.SetActive(true);
            sellButton.SetActive(false);
        }
        else
        {
            buyButton.SetActive(false);
            sellButton.SetActive(true);
        }
        AlreadyHaveClothes();
    }

    public void Buy()
    {
        ChangeOutfit.shirt[clotheNumber] = true;
    }
    public void Sell()
    {
        ChangeOutfit.shirt[clotheNumber] = false;
    }

    void AlreadyHaveClothes()
    {
        if (shirt)
        {
            DisableBottons(ChangeOutfit.referenceShirtNumber);
        }
        else if (pants)
        {
            DisableBottons(ChangeOutfit.referenceLegNumber);
        }
        else if (shoe)
        {
            DisableBottons(ChangeOutfit.referenceShoeNumber);
        }
    }
    void DisableBottons(int number)
    {
        if (number == clotheNumber)
        {
            buyButton.SetActive(false);
            sellButton.SetActive(false);

            return;
        }
    }
}
