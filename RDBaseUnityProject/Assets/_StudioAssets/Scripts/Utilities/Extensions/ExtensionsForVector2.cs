/*
 * ExtensionsForVector2.cs
 * 
 * Description:
 * - Adds extensions to the basic Vector2 class
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.Extensions
{
    public static class ExtensionsForVector2
    {
        //--- Fake Extensions (Not really extensions since static extensions are impossible) ---//
        public static Vector2 Vec2_X0(float x)
        {
            return new Vector2(x, 0.0f);
        }

        public static Vector2 Vec2_0Y(float y)
        {
            return new Vector2(0.0f, y);
        }

        //--- Actual Extensions ---//
        public static float RandValBetweenXAndY(this Vector2 range)
        {
            return Random.Range(range.x, range.y);
        }

        public static float LerpBetweenXAndY(this Vector2 range, float lerpT)
        {
            return Mathf.Lerp(range.x, range.y, lerpT);
        }
		
		public static bool IsValueInRange(this Vector2 range, float value)
        {
            return value >= range.x && value <= range.y;
        }

        public static Vector3 Vec3_XY0(this Vector2 vec)
        {
            return new Vector3(vec.x, vec.y, 0.0f);
        }

        public static Vector3 Vec3_X0Y(this Vector2 vec)
        {
            return new Vector3(vec.x, 0.0f, vec.y);
        }

        public static Vector3 Vec3_0XY(this Vector2 vec)
        {
            return new Vector3(0.0f, vec.x, vec.y);
        }

        public static Vector3 Vec3_YX0(this Vector2 vec)
        {
            return new Vector3(vec.y, vec.x, 0.0f);
        }

        public static Vector3 Vec3_Y0X(this Vector2 vec)
        {
            return new Vector3(vec.y, 0.0f, vec.x);
        }

        public static Vector3 Vec3_0YX(this Vector2 vec)
        {
            return new Vector3(0.0f, vec.y, vec.x);
        }
    }
}