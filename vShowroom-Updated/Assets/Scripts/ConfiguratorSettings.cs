using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfiguratorSettings : MonoBehaviour
{
    //public GameObject CanvasConfig;
    //public GameObject sliderCanvas;
    private bool ActivateConfig;
    public Navigation PlayerNav;
    public RotateObjects rotation;
    public OriginalPositionSlerp reset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenDetailedMode()
    {
        //Open the Configuration UI
        //CanvasConfig.SetActive(true);
        //Disable player movement 
        PlayerNav.enabled = false;
        //Enable the bike movement
        rotation.enabled = true;
    }

    public void CloseDetailedMode()
    {
        //Close the Configuration UI
        //CanvasConfig.SetActive(false);
        //Enable player movement 
        PlayerNav.enabled = true;
        //Disable the bike movement
        rotation.enabled = false;
        //Rotate the bike to the original position
        reset.original = true;


    }
    public void ChangeDetailedMode()
    {
        //Close the Configuration UI
        //CanvasConfig.SetActive(false);

        //Disable the bike movement
        rotation.enabled = false;

        //Rotate the bike to the original position
        reset.original = true;




    }

}
