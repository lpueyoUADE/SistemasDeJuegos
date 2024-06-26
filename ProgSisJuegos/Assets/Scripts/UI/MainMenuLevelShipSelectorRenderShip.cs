using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuLevelShipSelectorRenderShip : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject rotatedObject;
    public float rotatingSpeed = 25;
    bool rotate = false;

    private void FixedUpdate()
    {
        if (!rotate) return;

        rotatedObject.transform.Rotate(Vector3.up, rotatingSpeed * Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rotate = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rotate = false;
    }
}
