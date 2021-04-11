using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Utils
{
    public static class TilemapExtensions
    {
        public static List<Vector2Int> GetTiles(this Tilemap tilemap)
        {
            return tilemap.GetTiles(Vector2Int.zero);
        }
        public static List<Vector2Int> GetTiles(this Tilemap tilemap, Vector2Int offset)
        {
            var bounds = tilemap.cellBounds;
            var allTiles = tilemap.GetTilesBlock(bounds);
            var foundTiles = new List<Vector2Int>();
            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    var tile = allTiles[x + y * bounds.size.x];
                    if (tile == null)
                    {
                        continue;
                    }
                    foundTiles.Add(new Vector2Int(x + offset.x, y + offset.y));
                }
            }

            return foundTiles;
        }
        public static List<Vector2Int> GetPath(this Tilemap tilemap, Vector2Int offset)
        {
            var tiles = tilemap.GetTiles(offset);
            return GetPath(tiles);
        }

        public static List<Vector2Int> GetPath(this List<Vector2Int> tiles)
        {
            var tail = new Vector2Int(-100000, -100000);
            foreach (var tile in tiles)
            {
                if (NeighborCount(tile, tiles) == 1)
                {
                    tail = tile;
                    break;
                }
            }

            if (tail == new Vector2Int(-100000, -100000))
            {
                throw new ArgumentException("No tail found");
            }

            var visited = new HashSet<Vector2Int> {tail};
            var current = tail;
            var path = new List<Vector2Int>() {tail};
            var guard = 10000;
            while (visited.Count < tiles.Count)
            {
                guard--;
                foreach (var other in tiles.Where(
                    other => other != current && !visited.Contains(other)))
                {
                    if (!current.IsNeighbor(other))
                    {
                        continue;
                    }

                    // visited.add(other)
                    visited.Add(other);
                    path.Add(other);
                    current = other;
                    break;
                }

                if (guard == 0)
                {
                    Debug.LogError("Full path not found");
                    break;
                }
            }

            return path;
        }

        private static bool IsNeighbor(this Vector2Int t1, Vector2Int t2)
        {
            return (Math.Abs(t1.x - t2.x) == 1 && Math.Abs(t1.y - t2.y) == 0) ||
                   (Math.Abs(t1.x - t2.x) == 0 && Math.Abs(t1.y - t2.y) == 1);
        }

        private static int NeighborCount(this Vector2Int item, List<Vector2Int> others)
        {
            return others
                .Count(other => item.IsNeighbor(other));
        }

        public static void DrawPathGizmos(this Tilemap baseMap, List<Vector3Int> path)
        {
            var colorStep = 1f / path.Count;
            var currentColor = new Color(0, 0, 1, 0.5f);
            for (var pointIndex = 0; pointIndex < path.Count; pointIndex++)
            {
                var point = path[pointIndex];
                var worldPos = baseMap.GetCellCenterWorld(point);
                currentColor.r += colorStep;
                currentColor.b -= colorStep;
                Gizmos.color = currentColor;
                Gizmos.DrawCube(worldPos, Vector3.one);
            }
        }
        
        // private static Texture 

    }
}