/*
 * ExtensionsForMathf.cs
 * 
 * Description:
 * - Adds extensions to the basic Mathf class
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

using System.Collections.Generic;

namespace RubberDucks.Utilities.Extensions
{
    public static class ExtensionsForMathf
    {
        //--- Fake Extensions (Not really extensions since static extensions are impossible) ---//
        public static float Average(List<float> floats)
        {
            float sum = 0.0f;
            floats.ForEach(f => sum += f);

            return sum / (float)floats.Count;
        }

        public static Vector2 Average(List<Vector2> vec2s)
        {
            List<float> xCoords = new List<float>();
            List<float> yCoords = new List<float>();

            vec2s.ForEach(vec => { xCoords.Add(vec.x); yCoords.Add(vec.y); });

            return new Vector2(Average(xCoords), Average(yCoords));
        }

        public static Vector3 Average(List<Vector3> vec3s)
        {
            List<float> xCoords = new List<float>();
            List<float> yCoords = new List<float>();
            List<float> zCoords = new List<float>();

            vec3s.ForEach(vec => { xCoords.Add(vec.x); yCoords.Add(vec.y); zCoords.Add(vec.z); });

            return new Vector3(Average(xCoords), Average(yCoords), Average(zCoords));
        }
    }
}