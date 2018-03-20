using UnityEngine;
using System.Collections;

public class mouseCursor : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public float hotspotOffset = 0f;

    void Update()
    {
        Vector2 hotSpot = new Vector2(hotspotOffset, hotspotOffset);
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}