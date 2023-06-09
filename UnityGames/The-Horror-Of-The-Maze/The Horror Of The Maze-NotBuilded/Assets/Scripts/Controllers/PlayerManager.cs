using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour{

    #region Singleton

    public static PlayerManager instance;
    public static int inventory;
    public static int maxInventory = 9;
    static Text InventoryText;

    void Awake ()
    {
        instance = this;
        inventory = 0;
        InventoryText = GameObject.Find("InventoryCounter").GetComponent<Text>();
    }

    #endregion


    void Update()
    {
        //på ESC skifter den til meain menu scene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("close");
            Application.Quit();
        }
    }

    //tilføjer en note til inventory og updater inventoryText
    public static void AddToInventory()
    {
        inventory++;
        InventoryText.text = "Notes: " + inventory + "/" + maxInventory;
        print(inventory);
    }

    public static int GetInvetory()
    {
        return inventory;
    }



    public GameObject player;
}
