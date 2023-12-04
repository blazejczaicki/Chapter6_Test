using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamoTile : MonoBehaviour {

	public event Action<CamoTile> ClickTile;

	[SerializeField] private SpriteRenderer _highlight;
	private SpriteRenderer _spriteRenderer;

	public bool IsActive { get; set; }
	public int TrueIndex { get; set; }
	public int CurrentIndex { get; set; }

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
        IsActive = true;
    }

    private void OnMouseDown()
    {
		if (IsActive == false)
		{
			return;
		}

		if (ClickTile != null)
        {
            ClickTile.Invoke(this);
        }
    }

	public void SetSprite(Sprite sprite)
	{
        _spriteRenderer.sprite = sprite;
    }

    public void OnSelect()
	{
		_highlight.color = new Color(_highlight.color.r, _highlight.color.g, _highlight.color.b, 1);
	}

	public void OnDeselect()
	{
        _highlight.color = new Color(_highlight.color.r, _highlight.color.g, _highlight.color.b, 0);
    }
}
