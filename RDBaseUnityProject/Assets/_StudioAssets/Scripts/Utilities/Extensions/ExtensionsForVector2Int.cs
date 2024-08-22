/*
 * ExtensionsForVectorInt2.cs
 * 
 * Description:
 * - Adds extensions to the basic Vector2Int class
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.Extensions
{
    public static class ExtensionsForVector2Int
    {
        //--- Fake Extensions (Not really extensions since static extensions are impossible) ---//
        public static Vector2Int Vec2_X0(int x)
        {
            return new Vector2Int(x, 0);
        }

        public static Vector2Int Vec2_0Y(int y)
        {
            return new Vector2Int(0, y);
        }

        //--- Actual Extensions ---//
        public static int RandValBetweenXAndY(this Vector2Int range)
        {
            return Random.Range(range.x, range.y + 1);
        }

        public static float LerpBetweenXAndY(this Vector2Int range, int lerpT)
        {
            return Mathf.Lerp(range.x, range.y, lerpT);
        }

        public static Vector3Int Vec3_XY0(this Vector2Int vec)
        {
            return new Vector3Int(vec.x, vec.y, 0);
        }

        public static Vector3Int Vec3_X0Y(this Vector2Int vec)
        {
            return new Vector3Int(vec.x, 0, vec.y);
        }

        public static Vector3Int Vec3_0XY(this Vector2Int vec)
        {
            return new Vector3Int(0, vec.x, vec.y);
        }

        public static Vector3Int Vec3_YX0(this Vector2Int vec)
        {
            return new Vector3Int(vec.y, vec.x, 0);
        }

        public static Vector3Int Vec3_Y0X(this Vector2Int vec)
        {
            return new Vector3Int(vec.y, 0, vec.x);
        }

        public static Vector3Int Vec3_0YX(this Vector2Int vec)
        {
            return new Vector3Int(0, vec.y, vec.x);
        }
    }
}