using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{
    public GameObject[] CanvasToClose;
    public GameObject[] CanvasToOpen;
    public void CanvasOpen()
    {
        OpenCanvas(CanvasToClose, CanvasToOpen);
    }
    public void LoadScene(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OpenCanvas(GameObject[] CanvasToClose, GameObject[] CanvasToOpen)
    {
        CanvasToClose[1].gameObject.SetActive(false);
        CanvasToClose[0].gameObject.SetActive(false);

        CanvasToOpen[0].gameObject.SetActive(true);
    }
}
