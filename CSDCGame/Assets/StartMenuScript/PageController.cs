using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    public GameObject pagePanel;

    public void TogglePage()
    {
        bool isActive = pagePanel.activeSelf;
        pagePanel.SetActive(!isActive);
    }

    public void ClosePage()
    {
        bool isActive = pagePanel.activeSelf;
        pagePanel.SetActive(false);
    }
    private void Start()
    {
        pagePanel.SetActive(false);
    }
}
