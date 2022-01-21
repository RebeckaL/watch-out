using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject start;
    private bool byBell, byCog;
    private GameObject currentCog;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(start);
    }

    private void Update()
    {
        Move();
        if (byBell)
        {
            BongBell();
        }
        else if (byCog)
        {
            FixCog();
        }
    }

    private void Move()
    {
        transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
    }

    private void BongBell()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (ClockManager.TimeIsTwelwe)
            {
                Debug.Log("BONG BONG");
                ClockManager.ClockHasBeenRung = true;
            }
        }
    }

    private void FixCog()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentCog.GetComponent<CogScript>().FixCog();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "bell")
        {
            byBell = true;
        }
        else if (collision.gameObject.CompareTag("cog"))
        {
            byCog = true;
            currentCog = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "bell")
        {
            byBell = false;
        }
        else if (collision.gameObject.CompareTag("cog"))
        {
            byCog = false;
            currentCog = null;
        }
    }
}
