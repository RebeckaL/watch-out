using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject start;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(start);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
    }
}
