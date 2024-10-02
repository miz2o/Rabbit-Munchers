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
        button.onClick.AddListener(() => OnButtonClick(button));
        Debug.Log("Added listener to button: " + button.name);
    }



    void OnButtonClick(Button clickedButton)
    {

        sellmode = !sellmode;
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (sellmode && Input.GetMouseButtonDown(0)) // Detect left-click (mouse button 0)
        {
            DetectTowerClick();

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
                    if (wavehandler.GetComponent<WaveHandler>() != null)
                    {
                        wavehandler.GetComponent<WaveHandler>().currency += sellamt;
                      
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
