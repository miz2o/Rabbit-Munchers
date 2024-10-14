using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellScript : MonoBehaviour
{
    public Button button;
    public bool sellmode;
    private int sellamt;
    public GameObject wavehandler;

    // Start is called before the first frame update
    void Start()
    {
        // Check if button is null before adding the listener
        if (button != null)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
            Debug.Log("Added listener to button: " + button.name);
        }
        else
        {
            Debug.LogError("Button reference not set in the Inspector!");
        }
    }

    void OnButtonClick(Button clickedButton)
    {
        sellmode = !sellmode;
        print("sell clicked");
    }

    // Update is called once per frame
    void Update()
    {
        if (sellmode && Input.GetMouseButtonDown(0)) // Detect left-click (mouse button 0)
        {
            DetectTowerClick();
            print("Clicked tower");
        }
    }

    void DetectTowerClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cast a ray from the camera through the mouse position
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Tower")) // Check if the object hit has the "Tower" tag
            {
                Debug.Log("Clicked on a Tower!");
                // Perform whatever actions you need for clicking on a Tower object
                Destroy(hit.collider.gameObject);
                sellmode = false;

                if (hit.collider.GetComponent<TowerAttacking>() != null)
                {
                    sellamt = hit.collider.GetComponent<TowerAttacking>().towercost / 2;

                    // Ensure wavehandler and WaveHandler are not null before accessing them
                    if (wavehandler != null && wavehandler.GetComponent<WaveHandler>() != null)
                    {
                        wavehandler.GetComponent<WaveHandler>().currency += sellamt;
                    }
                    else
                    {
                        Debug.LogError("WaveHandler component or wavehandler GameObject is missing!");
                    }

                    if (hit.collider.GetComponent<TowerAttacking>().toThrow != null)
                    {
                        Destroy(hit.collider.GetComponent<TowerAttacking>().toThrow);
                    }
                }
            }
        }
    }
}