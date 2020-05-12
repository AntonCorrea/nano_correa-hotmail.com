using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Vector3 pointerPosition;
 
    private NavMeshAgent navAgent;
    public LineRenderer line;

    public GameObject cameraPivot;
    public float cameraMaxDist = 24f, cameraMinDist = 7f,cameraSpeed=0.2f;
    public GraphicRaycaster m_UIGraphicRaycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public Inventory inventory; 
    public GameObject uiInventoryOptions;
    public GameObject uiInventoryOptionTarget;
    public GameObject uiUseButton;
    public GameObject uiContextButton;
    public GameObject uiInvButton;
    public GameObject uiDescPanel;
    public GameObject uiInventoryPanel;
    public GameObject uiInternalPanelInventory;
    public GameObject uiTextMsg;
    public GameObject uiCombinePanel;
    public bool uiPressedOption = false;
    public bool uiPressed = false;

    public Joystick fixedJoystick;

    public List<GameObject>  contextItems = new List<GameObject>();

    
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
        navAgent = GetComponent<NavMeshAgent>();
        line.useWorldSpace=true;
        //destination = transform.position;

        //Fetch the Raycaster from the GameObject (the Canvas)
        //m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        //velocity = navAgent.velocity;

        if (Input.touchCount > 0)
        {      
            //if(Input.GetTouch(0).phase == TouchPhase.Began )
            //{
                pointerPosition = Input.GetTouch(0).position;
            /*}
            else
            {
                pointerPosition = Vector3.zero;
            }*/
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                pointerPosition = Vector3.zero;
            }
        }
        else
        {
            pointerPosition = Vector3.zero;
            if (Input.GetMouseButton(0))
            {
                pointerPosition = Input.mousePosition;
            }
            else
            {
                pointerPosition = Vector3.zero;
            }
        }

        if (pointerPosition != Vector3.zero)
        {

            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            //m_PointerEventData.position = Input.mousePosition;
            m_PointerEventData.position = pointerPosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_UIGraphicRaycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            if (results.Count > 0)
            {
                if (uiPressed == false)
                {

                    foreach (RaycastResult result in results)
                    {

                        //Debug.Log(result.gameObject.name);
                        uiPressed = true;
                        
                        if (result.gameObject.name == "slot" && !uiPressedOption)
                        {
                            if (uiCombinePanel.activeSelf)
                            {
                                //print(result.gameObject.transform.GetChild(2).GetComponent<Text>().text);
                                //print(uiCombinePanel.transform.parent.GetChild(2).GetComponent<Text>().text);
                                if (result.gameObject.transform.GetChild(0).GetComponent<Text>().text != uiCombinePanel.transform.parent.GetChild(0).GetComponent<Text>().text)
                                {
                                    if (result.gameObject.transform.GetChild(2).GetComponent<Text>().text == uiCombinePanel.transform.parent.GetChild(2).GetComponent<Text>().text)
                                    {
                                        PrintMsg("Has obtenido " + result.gameObject.transform.GetChild(2).GetComponent<Text>().text.Remove(0, 5) + ".");
                                        uiCombinePanel.SetActive(false);
                                        inventory.Add(result.gameObject.transform.GetChild(2).GetComponent<Text>().text);
                                        inventory.Remove(result.gameObject.transform.GetChild(0).GetComponent<Text>().text);
                                        inventory.Remove(uiCombinePanel.transform.parent.GetChild(0).GetComponent<Text>().text);
                                    }
                                    else
                                    {
                                        uiCombinePanel.SetActive(false);
                                        PrintMsg("No se puede combinar " + result.gameObject.transform.GetChild(0).GetComponent<Text>().text.Remove(0, 5) + " con " + uiCombinePanel.transform.parent.GetChild(0).GetComponent<Text>().text + " !");
                                    }

                                }
                            }
                            else
                            {
                                uiInventoryOptions.SetActive(true);
                                uiInventoryOptions.transform.SetParent(result.gameObject.transform);
                                uiInventoryOptions.transform.localPosition = Vector2.zero;
                                uiInventoryOptions.transform.SetParent(uiInventoryPanel.transform.parent);
                                uiInventoryOptionTarget = result.gameObject;
                            }


                        }
                        if (result.gameObject.name == "Combine")
                        {
                            PrintMsg("Combinar " + uiInventoryOptionTarget.transform.GetChild(0).GetComponent<Text>().text.Remove(0, 5) + " con...");
                            uiPressedOption = true;
                            uiInventoryOptions.SetActive(false);
                            uiCombinePanel.transform.SetParent(uiInventoryOptionTarget.transform);
                            uiCombinePanel.transform.localPosition = Vector2.zero;
                            //uiCombinePanel.transform.SetParent(uiInventoryPanel.transform);
                            uiCombinePanel.SetActive(true);
                        }
                        if (result.gameObject.name == "Drop")
                        {

                            uiPressedOption = true;
                            uiInventoryOptions.SetActive(false);
                            int k = 0;
                            bool foundInAllItems = false;
                            while (k < inventory.allItems.Count && foundInAllItems == false)
                            {
                                if (inventory.allItems[k].gameObject.name == uiInventoryOptionTarget.transform.GetChild(0).GetComponent<Text>().text)
                                {
                                    PrintMsg("Tiraste " + uiInventoryOptionTarget.transform.GetChild(0).GetComponent<Text>().text.Remove(0, 5) + " al suelo.");
                                    GameObject instance = Instantiate(inventory.allItems[k], transform.position + Vector3.up * 2f, Quaternion.identity, null);
                                    instance.name = inventory.allItems[k].gameObject.name;
                                    foundInAllItems = true;
                                    inventory.Remove(inventory.allItems[k].gameObject.name);
                                    if (inventory.allItems[k].gameObject.name == uiUseButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text)
                                    {
                                        uiUseButton.transform.GetChild(0).gameObject.SetActive(false);
                                        uiUseButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "";

                                    }

                                }
                                k++;
                            }

                        }
                        if (result.gameObject.name == "Equip")
                        {
                            //uiInventoryOptionTarget.transform.parent.GetComponent<Image>().color = Color.white;

                            uiPressedOption = true;
                            uiInventoryOptions.SetActive(false);
                            uiUseButton.transform.GetChild(0).gameObject.SetActive(true);
                            uiUseButton.transform.GetChild(0).GetComponent<Image>().sprite = uiInventoryOptionTarget.GetComponent<Image>().sprite;
                            PrintMsg("Has equipado " + uiInventoryOptionTarget.transform.GetChild(0).GetComponent<Text>().text.Remove(0, 5) + ".");
                            uiUseButton.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = uiInventoryOptionTarget.transform.GetChild(0).GetComponent<Text>().text;
                            //uiInventoryOptionTarget.transform.parent.GetComponent<Image>().color = Color.green;
                        }
                        if (result.gameObject.name == "Examine")
                        {
                            uiPressedOption = true;
                            uiInventoryOptions.SetActive(false);
                            uiInternalPanelInventory.SetActive(false);
                            uiDescPanel.SetActive(true);
                            uiDescPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = uiInventoryOptionTarget.transform.GetChild(0).GetComponent<Text>().text.Remove(0, 5);
                            uiDescPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = uiInventoryOptionTarget.transform.GetChild(1).GetComponent<Text>().text;
                        }
                        if (result.gameObject.name == "LeftTurn")
                        {

                            cameraPivot.transform.Rotate(0, 50f * Time.deltaTime, 0);
                            uiPressed = false;
                        }
                        if (result.gameObject.name == "RightTurn")
                        {

                            cameraPivot.transform.Rotate(0, -50f * Time.deltaTime, 0);
                            uiPressed = false;
                        }
                        if (result.gameObject.name == "inventoryButton")
                        {
                            //ui.buttons[2].SetActive(!ui.buttons[2].activeSelf);
                            uiDescPanel.SetActive(false);
                            uiInventoryPanel.SetActive(!uiInventoryPanel.activeSelf);
                            uiInternalPanelInventory.SetActive(true);
                            uiInventoryOptions.SetActive(false);
                        }
                        if (result.gameObject.name == "useButton")
                        {

                        }
                        if (result.gameObject.name == "contextButton")
                        {
                            contextItems[0].transform.GetComponent<Pickeable>().caller = gameObject;
                            contextItems[0].transform.GetComponent<Pickeable>().readyToPick = true;
                        }
                        if (result.gameObject.name == "InventaryPanelOut" && results.Count == 1f)
                        {
                            uiInventoryPanel.SetActive(false);

                            uiPressed = false;
                        }
                        if (result.gameObject.name == "DescriptionPanel")
                        {
                            uiDescPanel.SetActive(false);
                            uiInventoryPanel.SetActive(true);
                            uiInternalPanelInventory.SetActive(true);
                        }
                        if (result.gameObject.name == "zoomIn")
                        {
                            if (Camera.main.transform.localPosition.y > 7f)
                            {
                                Camera.main.transform.localPosition += new Vector3(0, -1f, 1f);
                                uiPressed = false;
                            }

                        }
                        if (result.gameObject.name == "zoomOut")
                        {
                            if (Camera.main.transform.localPosition.y < 24f)
                            {
                                Camera.main.transform.localPosition += new Vector3(0, 1f, -1f);
                                uiPressed = false;
                            }
                        }

                    }


                }
            }
            else
            //Else you touched the Terrain
            {

                //ui.buttons[2].SetActive(false);
                //if (uiInventoryPanel.activeSelf==true)
                //{
                uiInventoryPanel.SetActive(false);
                uiInventoryOptions.SetActive(false);
                uiDescPanel.SetActive(false);
                uiCombinePanel.SetActive(false);
                //}
                //else
                //{
                if(fixedJoystick.Horizontal == 0f || fixedJoystick.Vertical == 0f)
                {
                    //print(fixedJoystick.Horizontal+":::"+ fixedJoystick.Vertical);
                    //Ray ray = Camera.main.ScreenPointToRay(pointerPosition);
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(pointerPosition), out RaycastHit hit))
                    {
                        navAgent.SetDestination(hit.point);
                        line.positionCount = navAgent.path.corners.Length;
                        line.SetPositions(navAgent.path.corners);
                        /*if (hit.transform.name.StartsWith("Item"))
                        {

                            navAgent.SetDestination(hit.transform.position);
                            hit.transform.GetComponent<Pickeable>().caller = gameObject;
                            hit.transform.GetComponent<Pickeable>().readyToPick = true;

                        }*/
                        //pointerSprite.transform.position = navAgent.destination;
                    }
                }
                
                //}



            }

        }
        else
        {
            uiPressed = false;
            uiPressedOption = false;
        }

        /*if (Vector3.Distance(pointerSprite.transform.position, transform.position) < 1f)
        {
            pointerSprite.gameObject.SetActive(false);
        }
        else
        {
            pointerSprite.gameObject.SetActive(true);
        }*/

        if (fixedJoystick.Horizontal != 0f || fixedJoystick.Vertical != 0f)
        {
            //uiPressed = false;
            //float value;
            if (Camera.main.transform.localPosition.y > cameraMinDist && Camera.main.transform.localPosition.y < cameraMaxDist)
            {
                //value = fixedJoystick.Vertical * cameraSpeed * Time.deltaTime;
                Camera.main.transform.localPosition += new Vector3(0, -fixedJoystick.Vertical * cameraSpeed * Time.deltaTime, fixedJoystick.Vertical * cameraSpeed * Time.deltaTime);
            }
            if (Camera.main.transform.localPosition.y < cameraMinDist)
            {
                if (fixedJoystick.Vertical < 0)
                {
                    //value = fixedJoystick.Vertical * cameraSpeed * Time.deltaTime;
                    Camera.main.transform.localPosition += new Vector3(0, -fixedJoystick.Vertical * cameraSpeed * Time.deltaTime, fixedJoystick.Vertical * cameraSpeed * Time.deltaTime);
                }
            }
            if (Camera.main.transform.localPosition.y > cameraMaxDist)
            {
                if (fixedJoystick.Vertical > 0)
                {
                    //value = fixedJoystick.Vertical * cameraSpeed * Time.deltaTime;
                    Camera.main.transform.localPosition += new Vector3(0, -fixedJoystick.Vertical * cameraSpeed * Time.deltaTime, fixedJoystick.Vertical * cameraSpeed * Time.deltaTime);
                }
            }
            //Camera.main.transform.localPosition += new Vector3(0, - value, value);
            cameraPivot.transform.Rotate(0, -fixedJoystick.Horizontal * 50f * Time.deltaTime, 0);
        }

        contextItems.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].transform.name.StartsWith("Item"))
            {
                print(hitColliders[i].transform.name);
                if (!contextItems.Contains(hitColliders[i].gameObject))
                {
                    contextItems.Add(hitColliders[i].gameObject);
                }
                
            }
            
            i++;
        }



    }

    public void PrintMsg(string stringMsg)
    {
        uiTextMsg.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = stringMsg + "\n" + uiTextMsg.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    }

}

