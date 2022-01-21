using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> hands = new List<GameObject>();
    [SerializeField] private float tickSpeed = 1f;
    private float actualTickSpeed;
    [SerializeField] private GameObject clockTrigger;

    [Header("SOUNDS")]
    [SerializeField] private AudioClip tick;
    [SerializeField] private AudioClip miss;

    public static bool TimeIsTwelwe = false;
    public static bool ClockHasBeenRung = false;
    private int currentTime;
    private bool firstDoesntCount = true;

    private void Start()
    {
        ChangeTickTime();
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
        ChangeTickTime();
        yield return new WaitForSeconds(actualTickSpeed);
        hands[currentTime].GetComponent<SpriteRenderer>().color = GameManager.OffColor;

        if (!firstDoesntCount)
        {
            if (currentTime == 0 && !ClockHasBeenRung)
            {
                GameManager.GivePenalty();
                SoundManager.PlaySound(miss);
            }
            else { ClockHasBeenRung = false; }
        }
        else { firstDoesntCount = false; }

        if(currentTime < 11)
            currentTime++;
        else
            currentTime = 0;

        hands[currentTime].GetComponent<SpriteRenderer>().color = GameManager.OnColor;
        RotateTrigger();

        if (!SoundManager.source.isPlaying)
        {
            SoundManager.PlaySound(tick);
        }
        StartCoroutine(Tick());
    }

    private void ChangeTickTime()
    {
        if(BreakManager.numOfBrokenCogs > 0)
        {
            actualTickSpeed = tickSpeed - (0.25f * BreakManager.numOfBrokenCogs);
        }
        else { actualTickSpeed = tickSpeed; }
    }
}
