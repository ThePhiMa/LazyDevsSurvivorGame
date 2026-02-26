using System;
using UnityEngine;

namespace BIMM.Core
{
    public class PooledEnemy : MonoBehaviour
    {
        private Action<PooledEnemy> _release;

        // Called by the spawner when the object is taken from the pool
        public void Init(Action<PooledEnemy> releaseCallback)
        {
            _release = releaseCallback;
        }

        // Call this whenever the enemy should "despawn"
        public void Despawn()
        {
            if (_release != null)
                _release(this);
            else
                gameObject.SetActive(false); // fallback
        }
    }
}
