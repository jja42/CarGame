using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public GameObject StartText;
    public Scroll_Manager manager;

    private void OnStart()
    {
        manager.Begin();
        Car_Cont.start = true;
        StartText.SetActive(false);
    }
}
