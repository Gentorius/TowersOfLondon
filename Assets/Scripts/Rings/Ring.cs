using UnityEngine;

namespace Rings
{
    public class Ring : MonoBehaviour
    {
        public void SetRingPrefab(GameObject ringPrefab)
        {
            var ring = Instantiate(ringPrefab, transform.position, Quaternion.identity);
            ring.transform.SetParent(transform);
        }
    }
}
