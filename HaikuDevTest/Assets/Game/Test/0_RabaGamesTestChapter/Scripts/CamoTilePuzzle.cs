using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;

public class CamoTilePuzzle : PuzzleController {

	[SerializeField] private List<Sprite> _tilesSprites;
    [SerializeField] private CamoTile _tilePrefab;
    [SerializeField] private Transform _parent;
	[SerializeField] private Transform _startPos;

	[SerializeField] private int _width;
	[SerializeField] private int _height;
	[SerializeField] private int _randomNumber;
    [SerializeField] private float _tileSize;

	private CamoTile[] _camoTiles;

	private CamoTile _selectedTile;

	private void Reset()
	{
		_width = 6;
        _height = 4;
		_randomNumber = 1000;
    }
	
	private void Awake()
	{
        _camoTiles=new CamoTile[_width*_height];
        CreateTiles();
        RandomPlace();
    }

	public void CreateTiles()
	{
		for (int i = 0; i < _tilesSprites.Count; i++)
		{
			if (_camoTiles.Length-1<i)
			{
				break;
			}	
            var newTile = Instantiate(_tilePrefab, _parent);
            newTile.TrueIndex = i;
            newTile.CurrentIndex = i;
			newTile.SetSprite(_tilesSprites[i]);
			_camoTiles[i]=newTile;
			newTile.ClickTile += OnClickTile;
        }
	}

	public void RandomPlace()
	{
		_camoTiles = Utility.ShuffleArray(_camoTiles, UnityEngine.Random.Range(0, _randomNumber));

		for (int i = 0; i < _camoTiles.Length; i++)
		{
			_camoTiles[i].CurrentIndex = i;
		}

		PlaceTiles(_camoTiles);
    }

	public void PlaceTiles(CamoTile[] _camoTiles)
	{
        int longerSize = _height > _width ? _height : _width;
        Vector3 offset = Vector3.zero;
        for (int i = 0; i < _height; i++)
        {
            offset.y = -i * _tileSize;
            for (int j = 0; j < _width; j++)
            {
                offset.x = j * _tileSize;
                _camoTiles[j + longerSize * i].transform.localPosition = _startPos.transform.localPosition + offset;
            }
        }
    }

	public void OnClickTile(CamoTile clickedTile)
	{
		if (_selectedTile == null)
		{
			_selectedTile = clickedTile;
            clickedTile.OnSelect();
        }
		else if (clickedTile == _selectedTile)
		{
			clickedTile.OnDeselect();
			_selectedTile = null;
        }
		else
		{
			SwapTile(clickedTile);
			if (CheckPuzzleCorectness())
			{
				Win();
			}
        }
	}

	public void SwapTile(CamoTile clickedTile)
	{
		var tmpPos= clickedTile.transform.localPosition;
		clickedTile.transform.localPosition = _selectedTile.transform.localPosition;
        _selectedTile.transform.localPosition = tmpPos;

		_camoTiles[clickedTile.CurrentIndex] = _selectedTile;
		_camoTiles[_selectedTile.CurrentIndex] = clickedTile;

		var tmpInd = _selectedTile.CurrentIndex;
        _selectedTile.CurrentIndex = clickedTile.CurrentIndex;
        clickedTile.CurrentIndex = tmpInd;


        clickedTile.OnDeselect();
        _selectedTile.OnDeselect();
        _selectedTile = null;
    }

	public bool CheckPuzzleCorectness()
	{
		for (int i = 0; i < _camoTiles.Length; i++)
		{
			Debug.Log(_camoTiles[i].CurrentIndex + "   " + _camoTiles[i].TrueIndex);
			if (_camoTiles[i].CurrentIndex != _camoTiles[i].TrueIndex)
			{
				return false;
			}
		}
		return true;
	}

    public override void Skip()
    {
        base.Skip();
		DisablePuzzle();
		SetToCorrect();
    }

    public override void ResetPuzzle()
    {
		if ( _selectedTile != null )
		{
            _selectedTile.OnDeselect();
            _selectedTile = null;
        }
		EnablePuzzle();
        RandomPlace();
    }

    protected override void Win()
    {
        base.Win();
		DisablePuzzle();
    }

    public void SetToCorrect()
	{
        _camoTiles = _camoTiles.OrderBy(x => x.TrueIndex).ToArray();

        for (int i = 0; i < _camoTiles.Length; i++)
        {
            _camoTiles[i].CurrentIndex = i;
		}

        PlaceTiles(_camoTiles);
    }

	private void EnablePuzzle()
	{
        for (int i = 0; i < _camoTiles.Length; i++)
		{
			_camoTiles[i].IsActive = true;
		}        
    }
	private void DisablePuzzle()
	{
        if (_selectedTile != null)
        {
            _selectedTile.OnDeselect();
            _selectedTile = null;
        }
        for (int i = 0; i < _camoTiles.Length; i++)
		{
			_camoTiles[i].IsActive = false;
		}        
    }
}
