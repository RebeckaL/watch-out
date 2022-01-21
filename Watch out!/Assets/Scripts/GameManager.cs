using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Color onColor, offColor;
    public static Color OnColor { get; set; }
    public static Color OffColor { get; set; }

    [SerializeField] private List<GameObject> tries = new List<GameObject>();
    private static List<GameObject> missSprites = new List<GameObject>();
    private static int misses = 0;
    [SerializeField] private GameObject loss;
    private static GameObject lossMessage;
    public static bool loseState = false;
    private void Start()
    {
        OnColor = onColor;
        OffColor = offColor;
        missSprites = tries;
        foreach(GameObject miss in missSprites)
        {
            miss.GetComponent<Image>().color = OffColor;
        }
        lossMessage = loss;
    }

    private void Update()
    {
        if (loseState)
        {
            Time.timeScale = 0;
            lossMessage.SetActive(true);
            if (Keyboard.current[Key.R].wasPressedThisFrame)
            {
                Time.timeScale = 1f;
                lossMessage.SetActive(false);
                loseState = false;
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }


    public static void GivePenalty()
    {
        if (misses < 3)
        {
            missSprites[misses].GetComponent<Image>().color = OnColor;
        }
        else
        {
            lossMessage.SetActive(true);
            loseState = true;
        }
        misses++;
    }
}
