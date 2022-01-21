using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ClockManager.TimeIsTwelwe = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ClockManager.TimeIsTwelwe = false;
    }
}
