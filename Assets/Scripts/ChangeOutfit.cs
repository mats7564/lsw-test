using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOutfit : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject outfitCamera;
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject outfitUI;
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject tutorialShop;
    [SerializeField] GameObject tutorialLocker;

    [Header("ButtonsForBuy")]
    [SerializeField] GameObject shirtButtons;
    [SerializeField] GameObject pantsButtons;
    [SerializeField] GameObject shoeButtons;


    [SerializeField] Vector3[] ErrorSave;

    public event Action OnOutfitChanging;
    public event Action OutOutfitChanging;
    public event Action OnStore;

    bool openOutfit;
    bool openShop;
    bool enterLocker;
    bool enterShop;

    [Header("LockInfo")]
    [SerializeField] GameObject lockShirt;
    [SerializeField] GameObject lockLeg;
    [SerializeField] GameObject lockShoe;

    [Header("Animator")]
    [SerializeField] Animator headAnim;
    [SerializeField] Animator torsoAnim;
    [SerializeField] Animator legAnim;
    [SerializeField] Animator shoeAnim;

    [Header("ClothesNumbers")]
    // 0-White Shirt 1-Red Jacket 2- Green Sweater 3-Blue Shirt 4-School Jacket
    public static Dictionary<int, bool> shirt = new Dictionary<int, bool>();
    int shirtNumber = 0;
    public static int referenceShirtNumber;
    // 0-Blue Leg 1-Green Leg 2- Yellow Leg 
    public static Dictionary<int, bool> leg = new Dictionary<int, bool>();
    int legNumber = 0;
    public static int referenceLegNumber;
    // 0-Normal Boot 1-Red Tennis 2- Blue Tennis 3-Green Boot
    public static Dictionary<int, bool> shoe = new Dictionary<int, bool>();
    int shoeNumber = 0;
    public static int referenceShoeNumber;

    void Start()
    {
        shirt.Add(0, true);
        shirt.Add(1, false);
        shirt.Add(2, false);
        shirt.Add(3, false);
        shirt.Add(4, true);
        leg.Add(0, true);
        leg.Add(2, false);
        leg.Add(1, false);
        shoe.Add(0, true);
        shoe.Add(1, true);
        shoe.Add(2, true);
        shoe.Add(3, true);
    }

    void Update()
    {
        ReferenceUpdate();
        SwapCamera();
        SwapUIForOutfit();
        SwapUIForShop();
        BeginLocker();
        BeginShop();
        EndLocker();
        EndShop();
    }

    void ReferenceUpdate()
    {
        referenceShirtNumber = shirtNumber;
        referenceLegNumber = legNumber;
        referenceShoeNumber = shoeNumber;
    }

    #region Action

    void BeginLocker()
    {
        if (!enterLocker) { return; }

        tutorialLocker.SetActive(true);

        if (Input.GetKeyDown(KeyCode.X))
        {
            openOutfit = true;
            OnOutfitChanging?.Invoke();
        }
    }
    void BeginShop()
    {
        if (!enterShop) { return; }

        tutorialShop.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            openShop = true;
            OnStore?.Invoke();
        }
    }
    void EndLocker()
    {
        if (enterLocker) { return; }

        tutorialLocker.SetActive(false);

        
    }

    void EndShop()
    {
        if (enterShop) { return; }

        tutorialShop.SetActive(false); 
    }

    #endregion


    #region Swap
    void SwapCamera()
    {
        if(!openOutfit) { return; }
        mainCamera.SetActive(false);
        outfitCamera.SetActive(true);
    }

    void SwapUIForOutfit()
    {
        if (!openOutfit) { return; }
        gameplayUI.SetActive(false);
        outfitUI.SetActive(true);
    }
    void SwapUIForShop()
    {
        if (!openShop) { return; }
        gameplayUI.SetActive(false);
        shopUI.SetActive(true);
    }

    #endregion

    #region Button
    public void NextTorso()
    {
        shirtNumber++;
        if (shirtNumber == 5) { shirtNumber = 0; }

        bool isUnlock = shirt[shirtNumber];

        if (!isUnlock)
        {
            lockShirt.SetActive(true);
        }
        else if (isUnlock)
        {
            lockShirt.SetActive(false);
        }
      
        ChangingAnimatorNumber(torsoAnim, "TorsoNumber", shirtNumber);
    }
    public void NextLeg()
    {
        legNumber++;
        if (legNumber == 3) { legNumber = 0; }
        ChangingAnimatorNumber(legAnim, "LegNumber", legNumber);
    }
    public void NextShoe()
    {
        shoeNumber++;
        if (shoeNumber == 4) { shoeNumber = 0; }
        ChangingAnimatorNumber(shoeAnim, "ShoeNumber", shoeNumber);
    }

    public void PreviusTorso()
    {
        shirtNumber--;
        if(shirtNumber == -1) { shirtNumber = 4; }

        bool isUnlock = shirt[shirtNumber];

        if (!isUnlock)
        {
            lockShirt.SetActive(true);
        }
        else if (isUnlock)
        {
            lockShirt.SetActive(false);
        }

        ChangingAnimatorNumber(torsoAnim, "TorsoNumber", shirtNumber);
    }
    public void PreviusLeg()
    {
        legNumber--;
        if (legNumber ==- 1) { legNumber = 2; }
        ChangingAnimatorNumber(legAnim, "LegNumber", legNumber);
    }
    public void PreviusShoe()
    {
        shoeNumber--;
        if (shoeNumber == -1) { shoeNumber = 3; }
        ChangingAnimatorNumber(shoeAnim, "ShoeNumber", shoeNumber);
    }

    public void ExitLocker()
    {
        if (lockShirt.activeInHierarchy) { return; }

        openOutfit = false;
        OutOutfitChanging?.Invoke();
        mainCamera.SetActive(true);
        outfitCamera.SetActive(false);
        gameplayUI.SetActive(true);
        outfitUI.SetActive(false);
    }
    public void ExitShop()
    {
        if (lockShirt.activeInHierarchy) { return; }

        openShop = false;
        OutOutfitChanging?.Invoke();
        gameplayUI.SetActive(true);
        shopUI.SetActive(false);
    }

    public void ShopShirtButton()
    {
        shirtButtons.SetActive(true);
        pantsButtons.SetActive(false);
        shoeButtons.SetActive(false);
    }
    public void ShopPantsButton()
    {
        shirtButtons.SetActive(false);
        pantsButtons.SetActive(true);
        shoeButtons.SetActive(false);
    }
    public void ShopShoeButton()
    {
        shirtButtons.SetActive(false);
        pantsButtons.SetActive(false);
        shoeButtons.SetActive(true);
    }
  
    #endregion

    #region Changing Animator

    void ChangingAnimatorNumber(Animator animObj,string parameterName, int partNumber)
    {
        animObj.SetInteger(parameterName, partNumber);
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Locker"))
        {
            enterLocker = true;
        }
        if (collision.CompareTag("Shop"))
        {
            enterShop = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Locker"))
        {
            enterLocker = false;
        }
        if (collision.CompareTag("Shop"))
        {
            enterShop = false;     
        }
    }
}
