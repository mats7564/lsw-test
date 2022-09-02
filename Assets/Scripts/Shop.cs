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
    [SerializeField] int price = 10;

    private void Start()
    {
       
    }
    private void Update()
    {
        CheckClothesStatus();
    }

    void CheckClothesStatus()
    {        
        if (shirt)
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
            DisableBottons(ChangeOutfit.referenceShirtNumber);
        }
        else if (pants)
        {           
            if (ChangeOutfit.leg[clotheNumber] == false)
            {
                buyButton.SetActive(true);
                sellButton.SetActive(false);
            }
            else
            {
                buyButton.SetActive(false);
                sellButton.SetActive(true);
            }
            DisableBottons(ChangeOutfit.referenceLegNumber);
        }
        else if (shoe)
        {
            if (ChangeOutfit.shoe[clotheNumber] == false)
            {
                buyButton.SetActive(true);
                sellButton.SetActive(false);
            }
            else
            {
                buyButton.SetActive(false);
                sellButton.SetActive(true);
            }
            DisableBottons(ChangeOutfit.referenceShoeNumber);
        }
    }

    public void Buy()
    {
        if(ChangeOutfit.money < price) { return; }

        if (ChangeOutfit.money >= price) 
        {
            ChangeOutfit.money = price - ChangeOutfit.money;
        }

        if (shirt)
        {
            ChangeOutfit.shirt[clotheNumber] = true;
        }
        else if(pants)
        {
            ChangeOutfit.leg[clotheNumber] = true;
        }
        else if (shoe)
        {
            ChangeOutfit.shoe[clotheNumber] = true;
        }
        
    }
    public void Sell()
    {
        ChangeOutfit.money = price + ChangeOutfit.money;

        if (shirt)
        {
            ChangeOutfit.shirt[clotheNumber] = false;
        }
        else if (pants)
        {
            ChangeOutfit.leg[clotheNumber] = false;
        }
        else if (shoe)
        {
            ChangeOutfit.shoe[clotheNumber] = false;
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
