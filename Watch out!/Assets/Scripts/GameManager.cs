using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        OnColor = onColor;
        OffColor = offColor;
        missSprites = tries;
        lossMessage = loss;
    }

    public static void GivePenalty()
    {
        if (misses < 3)
        {
            missSprites[misses].GetComponent<Image>().color = OnColor;
        }
        else { lossMessage.SetActive(true); }
        misses++;
    }
}
