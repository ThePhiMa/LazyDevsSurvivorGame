using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace BIMM.Core
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private PooledEnemy _enemyPrefab;
        [SerializeField] private float _baseSpawnInterval = 2f;
        [SerializeField] private float _minimumSpawnInterval = 0.3f;
        [SerializeField] private float _rampDuration = 300f;
        [SerializeField] private Camera cam;

        [Header("Pool")]
        [SerializeField] private int _defaultCapacity = 30;
        [SerializeField] private int _maxSize = 200;

        private ObjectPool<PooledEnemy> _pool;

        private void Awake()
        {
            // If not assigned, grab main camera
            if (cam == null) cam = Camera.main;

            _pool = new ObjectPool<PooledEnemy>(
                createFunc: CreateEnemy,
                actionOnGet: OnGetEnemy,
                actionOnRelease: OnReleaseEnemy,
                actionOnDestroy: OnDestroyEnemy,
                collectionCheck: false,
                defaultCapacity: _defaultCapacity,
                maxSize: _maxSize
            );
        }

        private void Start()
        {
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
            if (cam == null) return;

            Vector2 spawnPosition = GetSpawnPosition(cam);

            var enemy = _pool.Get();
            enemy.transform.position = spawnPosition;
            enemy.transform.rotation = Quaternion.identity;
        }

        // Pool callbacks
        private PooledEnemy CreateEnemy()
        {
            var enemy = Instantiate(_enemyPrefab);
            enemy.Init(ReleaseEnemy); // give it the release callback
            return enemy;
        }

        private void OnGetEnemy(PooledEnemy enemy)
        {
            enemy.gameObject.SetActive(true);
        }

        private void OnReleaseEnemy(PooledEnemy enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        private void OnDestroyEnemy(PooledEnemy enemy)
        {
            Destroy(enemy.gameObject);
        }

        private void ReleaseEnemy(PooledEnemy enemy)
        {
            _pool.Release(enemy);
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