using UnityEngine;

namespace Pools
{
    /// <summary>
    /// All object in the ObjectsPool need to have this Class.
    /// Don't forget to "setActive(false)" when the object die.
    /// </summary>
    public class ObjectInPool : MonoBehaviour
    {
        /// <summary>
        /// When the object is disable and return in the pool for next use.
        /// </summary>
        private void OnDisable()
        {
            CancelInvoke();
        }
    }
}