using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, OffsetColor, wallColor;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;

    public void InitGround(bool isOffset) {
        if (isOffset)
            renderer.color = OffsetColor;
        else
            renderer.color = baseColor;
    }

    public void InitWall() {
        renderer.color = wallColor;
    }

    void OnMouseEnter() {
        highlight.SetActive(true);
    }

    void OnMouseExit() {
        highlight.SetActive(false);
    }
}
