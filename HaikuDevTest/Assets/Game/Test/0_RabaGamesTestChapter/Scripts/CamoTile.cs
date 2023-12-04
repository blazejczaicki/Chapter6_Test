using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamoTile : MonoBehaviour {

	public event Action<CamoTile> SelectTile;

	[SerializeField] private SpriteRenderer _highlight;
	private SpriteRenderer _spriteRenderer;

	public bool IsSelected { get; set; }
	public int index { get; set; }

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		IsSelected = false;
    }

    private void OnMouseDown()
    {
		if (SelectTile != null)
        {
            SelectTile.Invoke(this);
        }
		IsSelected = !IsSelected;

        if (IsSelected)
		{
			OnSelect();
        }
		else
		{
			OnDeselect();
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
