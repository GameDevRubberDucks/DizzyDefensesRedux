/*
 * ExtensionsForVector3.cs
 * 
 * Description:
 * - Adds extensions to the basic Vector3 class
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.Extensions
{
    public static class ExtensionsForVector3
    {
        //--- Fake Extensions (Not really extensions since static extensions are impossible) ---//
        public static Vector3 Vec3_X00(float x)
        {
            return new Vector3(x, 0.0f, 0.0f);
        }

        public static Vector3 Vec3_0Y0(float y)
        {
            return new Vector3(0.0f, y, 0.0f);
        }

        public static Vector3 Vec3_00Z(float z)
        {
            return new Vector3(0.0f, 0.0f, z);
        }

        //--- Actual Extensions ---//
        public static Vector2 XY(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }

        public static Vector2 YX(this Vector3 vec)
        {
            return new Vector2(vec.y, vec.x);
        }
        
        public static Vector2 XZ(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.z);
        }

        public static Vector2 ZX(this Vector3 vec)
        {
            return new Vector2(vec.z, vec.x);
        }

        public static Vector2 YZ(this Vector3 vec)
        {
            return new Vector2(vec.y, vec.z);
        }

        public static Vector2 ZY(this Vector3 vec)
        {
            return new Vector2(vec.z, vec.y);
        }

        public static Vector3 TransferOtherX(this Vector3 vec, Vector3 otherVec)
        {
            return new Vector3(otherVec.x, vec.y, vec.z);
        }

        public static Vector3 TransferOtherY(this Vector3 vec, Vector3 otherVec)
        {
            return new Vector3(vec.x, otherVec.y, vec.z);
        }

        public static Vector3 TransferOtherZ(this Vector3 vec, Vector3 otherVec)
        {
            return new Vector3(vec.x, vec.y, otherVec.z);
        }

        public static Vector3 TransferOtherXY(this Vector3 vec, Vector3 otherVec)
        {
            return new Vector3(otherVec.x, otherVec.y, vec.z);
        }

        public static Vector3 TransferOtherXZ(this Vector3 vec, Vector3 otherVec)
        {
            return new Vector3(otherVec.x, vec.y, otherVec.z);
        }

        public static Vector3 TransferOtherYZ(this Vector3 vec, Vector3 otherVec)
        {
            return new Vector3(vec.x, otherVec.y, otherVec.z);
        }

        public static Vector3 NewX(this Vector3 vec, float newX)
        {
            return new Vector3(newX, vec.y, vec.z);
        }

        public static Vector3 NewY(this Vector3 vec, float newY)
        {
            return new Vector3(vec.x, newY, vec.z);
        }

        public static Vector3 NewZ(this Vector3 vec, float newZ)
        {
            return new Vector3(vec.x, vec.y, newZ);
        }

        public static Vector3 NewXY(this Vector3 vec, float newX, float newY)
        {
            return new Vector3(newX, newY, vec.z);
        }

        public static Vector3 NewXY(this Vector3 vec, Vector2 newXY)
        {
            return NewXY(vec, newXY.x, newXY.y);
        }

        public static Vector3 NewXZ(this Vector3 vec, float newX, float newZ)
        {
            return new Vector3(newX, vec.y, newZ);
        }

        public static Vector3 NewXZ(this Vector3 vec, Vector2 newXZ)
        {
            return NewXY(vec, newXZ.x, newXZ.y);
        }

        public static Vector3 NewYZ(this Vector3 vec, float newY, float newZ)
        {
            return new Vector3(vec.x, newY, newZ);
        }

        public static Vector3 NewYZ(this Vector3 vec, Vector2 newYZ)
        {
            return NewXY(vec, newYZ.x, newYZ.y);
        }
    }
}