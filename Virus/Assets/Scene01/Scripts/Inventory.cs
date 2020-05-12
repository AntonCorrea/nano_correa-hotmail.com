using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private PlayerController controller;
    public GameObject inventoryPanels;
    public List<string> inventory = new List<string>();
    
    //private List<GameObject> panels = new List<GameObject>();
    public List<GameObject> allItems = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        /*for(int i = 0; i < inventoryPanels.transform.childCount; i++)
        {
            panels.Add(inventoryPanels.transform.GetChild(i).gameObject);
        }    */
         
    }

    // Update is called once per frame
    void Update()
    {
        //inventory = controller.inventory;


        
    }
    void UpdateInventoryUI()
    {
        for(int i=0;i<inventoryPanels.transform.childCount;i++)
        {
            inventoryPanels.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "";
            inventoryPanels.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
        }

        for (int i=0;i<inventory.Count;i++)
        {        
            int j = 0;
            bool foundInPanel = false;
            while (j < inventoryPanels.transform.childCount && foundInPanel == false)
            {
                if (inventoryPanels.transform.GetChild(j).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text == "")
                {
                    foundInPanel = true;
                    int k = 0;
                    bool foundInAllItems = false;
                    while (k < allItems.Count && foundInAllItems == false)
                    {
                        if (allItems[k].gameObject.name == inventory[i])
                        {
                            foundInAllItems = true;
                            inventoryPanels.transform.GetChild(j).transform.GetChild(0).gameObject.SetActive(true);
                            inventoryPanels.transform.GetChild(j).transform.GetChild(0).GetComponent<Image>().sprite = allItems[k].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                            inventoryPanels.transform.GetChild(j).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = allItems[k].name;
                            inventoryPanels.transform.GetChild(j).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = allItems[k].GetComponent<Pickeable>().description;
                            inventoryPanels.transform.GetChild(j).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = allItems[k].GetComponent<Pickeable>().recipeFor;
                        }
                        k++;
                    }
                }
                j++;
            }
        }
    }

    public void Add(string item)
    {
        inventory.Add(item);
        UpdateInventoryUI();
    }

    public void Remove(string item) 
    {
        inventory.Remove(item);
        UpdateInventoryUI();
    }
}
