using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class CamoTilePuzzle : PuzzleController {

	[SerializeField] private List<Sprite> _tilesSprites;
    [SerializeField] private CamoTile _tilePrefab;
    [SerializeField] private BoxCollider2D _tileCollider;
    [SerializeField] private Transform _parent;
	[SerializeField] private Transform _startPos;

	[SerializeField] private int _width;
	[SerializeField] private int _height;
	[SerializeField] private int _randomNumber;
    [SerializeField] private float _tileSize;

	private CamoTile[] camoTiles;

	private void Awake()
	{
        camoTiles=new CamoTile[_width*_height];
    }

	private void Start()
	{
        CreateTiles();
        RandomPlace();
    }

	private void Reset()
	{
		_width = 6;
        _height = 4;
		_randomNumber = 1000;
    }
	
	public void CreateTiles()
	{
		for (int i = 0; i < _tilesSprites.Count; i++)
		{
			if (camoTiles.Length-1<i)
			{
				break;
			}	
            var newTile = Instantiate(_tilePrefab, _parent);
            newTile.index = i;
			newTile.SetSprite(_tilesSprites[i]);
			camoTiles[i]=newTile;
        }
	}

	public void RandomPlace()
	{
		camoTiles = Utility.ShuffleArray(camoTiles, Random.Range(0, _randomNumber));

		int longerSize= _height>_width? _height: _width;
		Vector3 offset = Vector3.zero;
		for (int i = 0;i < _height; i++)
		{
			offset.y = -i * _tileSize;
            for (int j = 0; j< _width; j++)
			{
                offset.x = j * _tileSize;
				camoTiles[j+ longerSize * i].transform.localPosition = _startPos.transform.localPosition + offset;
            }
		}
	}

	public void SwapTile()
	{

	}
}
