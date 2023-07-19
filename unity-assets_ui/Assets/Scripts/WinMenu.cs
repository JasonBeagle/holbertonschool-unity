using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void MainMenu()
    {
        // // Hide the cursor when returning to the main menu
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
        
        // Load the main menu scene. 
        // Replace "MainMenu" with the actual name of your main menu scene
        SceneManager.LoadScene("MainMenu");
    }

    public void Next()
    {
        // Hide the cursor when moving to the next level
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
        
        // Get the index of the current scene in the build settings
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Check if there's another scene after the current one
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            // If so, load it
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            // Otherwise, go back to the main menu
            MainMenu();
        }
    }
}
