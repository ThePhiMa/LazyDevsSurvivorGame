using System.Collections;
using UnityEngine;

namespace BIMM.Core
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _baseSpawnInterval = 2f;
        [SerializeField] private float _minimumSpawnInterval = 0.3f;
        [SerializeField] private float _rampDuration = 300f;

        Camera cam;

        private void Start()
        {
            cam = Camera.main;
            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(GetSpawnInterval());
            }
        }

        private void SpawnEnemy()
        {
            if (cam == null)
            {
                return;
            }

            Vector2 spawnPosition = GetSpawnPosition(cam);
            Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        }

        private Vector2 GetSpawnPosition(Camera cam)
        {
            float height = cam.orthographicSize + 1f;
            float width = height * cam.aspect;
            Vector3 origin = cam.transform.position;

            int edge = Random.Range(0, 4);

            switch (edge)
            {
                case 0: return new Vector2(origin.x + Random.Range(-width, width), origin.y + height);
                case 1: return new Vector2(origin.x + Random.Range(-width, width), origin.y - height);
                case 2: return new Vector2(origin.x + width, origin.y + Random.Range(-height, height));
                default: return new Vector2(origin.x - width, origin.y + Random.Range(-height, height));
            }
        }

        private float GetSpawnInterval()
        {
            float t = Mathf.Clamp01(Time.timeSinceLevelLoad / _rampDuration);
            return Mathf.Lerp(_baseSpawnInterval, _minimumSpawnInterval, t);
        }
    }
}
