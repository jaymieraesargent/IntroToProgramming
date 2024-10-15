using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenAndResolution : MonoBehaviour
{
    public void FullscreenToggle(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    #region Resolution
    public Resolution[] resolutionsOnComputer;
    public Dropdown resolutionDropdown;

    private void Start()
    {
        //get all the resolutions for this computer from our screen info
        resolutionsOnComputer = Screen.resolutions;
        //reset and empty the dropdown
        resolutionDropdown.ClearOptions();
        //get ready to store new dropdown options
        List<string> options = new List<string>();
        //get ready to set the current resolution when found
        int currentResolutionIndex = 0;
        //loop through all options the computer has
        for (int i = 0; i < resolutionsOnComputer.Length; i++)
        {
            //hold formatted option
            string option = $"{resolutionsOnComputer[i].width} x {resolutionsOnComputer[i].height}";
            //---> check if option is already in list if not then
            //add that option to list
            options.Add(option);
            //if the option match our current res 
            if (resolutionsOnComputer[i].width == Screen.currentResolution.width && resolutionsOnComputer[i].height == Screen.currentResolution.height)
            {
                //then set this as our starting resolution
                currentResolutionIndex = i;
            }
        }
        //add options to dropdown
        resolutionDropdown.AddOptions(options);
        //display current resolution on dropdown
        resolutionDropdown.value = currentResolutionIndex;
        //refresh dropdown to make sure display is correct
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int selectedIndex)
    {
        Resolution resolution = resolutionsOnComputer[selectedIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    #endregion
}
