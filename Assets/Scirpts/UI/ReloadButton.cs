using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadButton : MonoBehaviour
{
    public void ReloadButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
