/*
 * RadialPlacementHelper.cs
 * 
 * Description:
 * - Utility script to place objects around an arc automatically
 * - Will place in a 2D plane at the same y-value as the central object
 * - Used in editor mode only. Will compile out in builds
 * 
 * Author(s): 
 * - Daniel MacCormick, 2023
*/

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MacCormick.TechnicalTest.Utilities
{
	public class RadialPlacementHelper : MonoBehaviour
	{
#if UNITY_EDITOR
        //--- Properties ---//

        //--- Public Variables ---//

        //--- Protected Variables ---//

        //--- Private Variables ---//
        [Header("Placement Information")]
        [SerializeField] private Transform m_CenterAnchor = default;
        [SerializeField] private Transform m_ParentAnchor;
        [SerializeField] private float m_StartingDegFromForward = 0.0f;
        [SerializeField] private float m_EndingDegFromForward = 180.0f;
        [SerializeField] private float m_SpawnRadiusX = 1.0f;
        [SerializeField] private float m_SpawnRadiusZ = 1.0f;

        [Header("Object Information")]
        [SerializeField] private Transform[] m_ObjectsToArrange = default;

        //--- Unity Methods ---//

        //--- Public Methods ---//
        [ContextMenu("ArrangeObjects()")]
        public void ArrangeObjects()
        {
            // Need this value to act as the denominator in the lerp, since the first object placed counts as well
            float objectCountMinusOne = (float)m_ObjectsToArrange.Length - 1;

            for (int i = 0; i < m_ObjectsToArrange.Length; ++i)
            {
                float angle = Mathf.Lerp(m_StartingDegFromForward, m_EndingDegFromForward, i / objectCountMinusOne);

                Vector3 spawnPosition = CalculateRadialSpawnPoint(m_CenterAnchor, m_SpawnRadiusX, m_SpawnRadiusZ, angle);

                m_ObjectsToArrange[i].position = spawnPosition;
                m_ObjectsToArrange[i].parent = m_ParentAnchor;
            }

            EditorUtility.SetDirty(this.gameObject);
        }

        //--- Protected Methods ---//

        //--- Private Methods ---//
        private Vector3 CalculateRadialSpawnPoint(Transform centerAnchor, float radiusX, float radiusZ, float angleFromLeft)
        {
            Vector3 spawnPoint;
            spawnPoint.x = radiusX * Mathf.Sin(angleFromLeft * Mathf.Deg2Rad);
            spawnPoint.y = 0.0f;
            spawnPoint.z = radiusZ * Mathf.Cos(angleFromLeft * Mathf.Deg2Rad);

            // Transform the spawn point so it is local around the anchor and can therefore make use of its rotation
            spawnPoint = centerAnchor.TransformPoint(spawnPoint);

            return spawnPoint;
        }
#endif
    }
}
