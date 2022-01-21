using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Color onColor, offColor;
    public static Color OnColor { get; set; }
    public static Color OffColor { get; set; }

    private void Awake()
    {
        OnColor = onColor;
        OffColor = offColor;
    }
}
