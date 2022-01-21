using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> hands = new List<GameObject>();
    [SerializeField] private float tickSpeed = 1f;
    [SerializeField] private GameObject clockTrigger;
    public static bool TimeIsTwelwe = false;
    private int currentTime;

    private void Start()
    {
        for(int i = 0; i < hands.Count; i++)
        {
            if(i != 0)
            {
                hands[i].GetComponent<SpriteRenderer>().color = GameManager.OffColor;
            }
        }
        currentTime = 0;
        RotateTrigger();
        StartCoroutine(Tick());
    }

    private void RotateTrigger()
    {
        clockTrigger.transform.rotation = hands[currentTime].transform.rotation;
    }

    private IEnumerator Tick()
    {
        yield return new WaitForSeconds(tickSpeed);
        hands[currentTime].GetComponent<SpriteRenderer>().color = GameManager.OffColor;

        if(currentTime < 11)
            currentTime++;
        else
            currentTime = 0;

        hands[currentTime].GetComponent<SpriteRenderer>().color = GameManager.OnColor;
        RotateTrigger();
        StartCoroutine(Tick());
    }
}
