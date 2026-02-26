using BIMM.Gameplay.Enemy;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace BIMM.Core {
    public class EnemySpawner : MonoBehaviour {
        private GameObject _enemyWraper;
        private Camera _cam;
        private ObjectPool<GameObject> _pool;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _baseSpawnInterval = 2f;
        [SerializeField] private float _minimumSpawnInterval = 0.3f;
        [SerializeField] private float _rampDuration = 300f;

        public ObjectPool<GameObject> Pool => _pool; 
        public static EnemySpawner Instance;

            

        private void Awake() {
            //Singleton
            if (Instance == null)
                Instance = this;
            else {
                Debug.LogWarning("There can only be one enemy spawner");
                Destroy(this);
            }
        }

        private void Start() {
            // Pool Creation
            _pool = new ObjectPool<GameObject>(
            createFunc: CreateEnemy,
            actionOnGet: GetEnemy,
            actionOnRelease: ReleaseEnemy,
            actionOnDestroy: DestroyEnemy,
            collectionCheck: true,   // helps catch double-release mistakes
            defaultCapacity: 10,
            maxSize: 50
            );

            //Find objects
            _enemyWraper = GameObject.Find("Enemies");
            _cam = Camera.main;

            //Start spawning enemies
            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop() {
            while (true) {
                SpawnEnemy();
                yield return new WaitForSeconds(GetSpawnInterval());
            }
        }

        private void SpawnEnemy() {
            if (_cam == null) return;

            GameObject enemy = _pool.Get();
        }

        private GameObject CreateEnemy() {
            return Instantiate(_enemyPrefab, GetSpawnPosition(_cam), Quaternion.identity, _enemyWraper.transform);
        }

        private void GetEnemy(GameObject enemy) {
            enemy.gameObject.SetActive(true);
            enemy.transform.position = GetSpawnPosition(_cam);
        }

        private void ReleaseEnemy(GameObject enemy) {
            enemy.gameObject.SetActive(false);
        }

        private void DestroyEnemy(GameObject enemy) {
            Destroy(enemy);
        }

        private Vector2 GetSpawnPosition(Camera cam) {
            float height = cam.orthographicSize + 1f;
            float width = height * cam.aspect;
            Vector3 origin = cam.transform.position;

            int edge = Random.Range(0, 4);

            switch (edge) {
                case 0: return new Vector2(origin.x + Random.Range(-width, width), origin.y + height);
                case 1: return new Vector2(origin.x + Random.Range(-width, width), origin.y - height);
                case 2: return new Vector2(origin.x + width, origin.y + Random.Range(-height, height));
                default: return new Vector2(origin.x - width, origin.y + Random.Range(-height, height));
            }
        }

        private float GetSpawnInterval() {
            float t = Mathf.Clamp01(Time.timeSinceLevelLoad / _rampDuration);
            return Mathf.Lerp(_baseSpawnInterval, _minimumSpawnInterval, t);
        }
    }
}
