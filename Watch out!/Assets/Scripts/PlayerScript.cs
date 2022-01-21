using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject start;
    [SerializeField] private AudioClip bing, bong;
    [SerializeField] private List<GameObject> soundMarkers = new List<GameObject>();
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
        if(EventSystem.current.currentSelectedGameObject != null)
            transform.position = EventSystem.current.currentSelectedGameObject.transform.position;

        if (GameManager.loseState)
            EventSystem.current.SetSelectedGameObject(null);
    }

    private void BongBell()
    {
        if(Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            if (ClockManager.TimeIsTwelwe)
            {
                ClockManager.ClockHasBeenRung = true;
            }
            RingBell();
        }
    }

    private void FixCog()
    {
        if (Keyboard.current[Key.Space].wasPressedThisFrame)
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

    public void RingBell()
    {
        float delay = 0.4f;
        List<AudioClip> rings = new List<AudioClip> { bing, bong };
        StartCoroutine(SoundManager.PlayNextSound(rings, delay));
        soundMarkers[0].GetComponent<SpriteRenderer>().color = GameManager.OnColor;
        soundMarkers[1].GetComponent<SpriteRenderer>().color = GameManager.OnColor;
        StartCoroutine(Ring(delay));
    }

    private IEnumerator Ring(float delay)
    {
        yield return new WaitForSeconds(delay);
        soundMarkers[2].GetComponent<SpriteRenderer>().color = GameManager.OnColor;
        StartCoroutine(Silence());
    }

    private IEnumerator Silence()
    {
        yield return new WaitForSeconds(0.6f);
        soundMarkers[0].GetComponent<SpriteRenderer>().color = GameManager.OffColor;
        soundMarkers[1].GetComponent<SpriteRenderer>().color = GameManager.OffColor;
        soundMarkers[2].GetComponent<SpriteRenderer>().color = GameManager.OffColor;
    }
}
