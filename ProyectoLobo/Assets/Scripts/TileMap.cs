﻿using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

    public Vector2 mapSize = new Vector2(20, 10);
    public Texture2D texture2D; // para almacenar la textura que vamos a usar para sacar las tiles
    public Vector2 tileSize = new Vector2();
    public Vector2 tilePadding = new Vector2();
    public Object[] spriteReferences;
    public Vector2 gridSize = new Vector2();
    public int pixelsToUnits = 100;
    public int tileID = 0;
    public GameObject tiles;

    public Sprite currentTileBrush {
        get { return spriteReferences[tileID] as Sprite; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmosSelected() {
        var pos = transform.position;

        if (texture2D != null) {

            //Esto es para dibujar la malla
            Gizmos.color = Color.gray;
            var row = 0;
            var maxColumns = mapSize.x;
            var total = mapSize.x * mapSize.y;
            var tile = new Vector3(tileSize.x / pixelsToUnits, tileSize.y / pixelsToUnits);
            var offset = new Vector2(tile.x / 2 , tile.y / 2);

            for (var i = 0; i < total; i++){

                var column = i % maxColumns;

                var newX = (column * tile.x) + offset.x + pos.x;
                var newY = -(row * tile.y) - offset.y + pos.y;

                Gizmos.DrawWireCube(new Vector2(newX, newY), tile);

                if (column == maxColumns - 1) {
                    row++;
                }
            }

            //Esto es para dibujar el borde
            Gizmos.color = Color.white;
            var centerX = pos.x + (gridSize.x / 2);
            var centerY = pos.y - (gridSize.y / 2);

            Gizmos.DrawWireCube(new Vector2(centerX, centerY), gridSize);
        }

    }

}
