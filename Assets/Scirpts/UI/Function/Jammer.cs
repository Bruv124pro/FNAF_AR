using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jammer : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void OnJammerButtonCliecked()
    {
        panel.SetActive(false);
    }
}
