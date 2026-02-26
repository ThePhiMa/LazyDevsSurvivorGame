using System.Collections;
using UnityEngine.Pool;
using UnityEngine;

namespace BIMM.Core
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _baseSpawnInterval = 2f;
        [SerializeField] private float _minimumSpawnInterval = 0.3f;
        [SerializeField] private float _rampDuration = 300f;
        private ObjectPool<GameObject> _pool;
        public ObjectPool<GameObject> Pool => _pool;
        private Camera camera;

        public static EnemySpawner Instance;

       
        void Awake()
        {
            // Create a pool with the four core callbacks.
            _pool = new ObjectPool<GameObject>(
                createFunc: CreateItem,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroyItem,
                collectionCheck: true,   // helps catch double-release mistakes
                defaultCapacity: 10,
                maxSize: 50
            );

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                Debug.LogError("only one instance of this class should exist");
            }
        }

        // Creates a new pooled GameObject the first time (and whenever the pool needs more).
        private GameObject CreateItem()
        {
            GameObject gameObject = Instantiate( _enemyPrefab );
            return gameObject;
        }

        // Called when an item is taken from the pool.
        private void OnGet(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        // Called when an item is returned to the pool.
        private void OnRelease(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        // Called when the pool decides to destroy an item (e.g., above max size).
        private void OnDestroyItem(GameObject gameObject)
        {
            Destroy(gameObject);
        }

        private System.Collections.IEnumerator ReturnAfter(GameObject gameObject, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            // Give it back to the pool.
            _pool.Release(gameObject);
        }

        private void Start()
        {
            StartCoroutine(SpawnLoop());
            camera = FindObjectOfType<Camera>();
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

            if (camera == null)
            {
                return;
            }

            Vector2 spawnPosition = GetSpawnPosition(camera);
            GameObject gameObject = _pool.Get();
            gameObject.transform.position = spawnPosition;

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
