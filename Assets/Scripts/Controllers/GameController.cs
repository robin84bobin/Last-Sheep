using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlatformController _platformController;
        [SerializeField] private Transform _spawnPointsContainer;
        [SerializeField] private Transform _plane;
        [SerializeField] private GameObject _field;
        [SerializeField] private GameConfig _config;
        
        private GameModel _gameModel;

        private void Start()
        {
            _gameModel = new GameModel(_config);
            
            _platformController.Init(_gameModel.platform, _field);
            _gameModel.Update();

            CreateSheeps();
        }

        private Queue<Vector3> _spawnPoints;
        private void CreateSheeps()
        {
            _spawnPoints = new Queue<Vector3>();
            int count = _spawnPointsContainer.childCount;
            for(int i = 0; i < count; i++)
            {
                Transform child = _spawnPointsContainer.GetChild(i);
                _spawnPoints.Enqueue(child.position);
            }
            
            var prefabHero = Resources.Load<GameObject>("Prefabs/Sven");
            var hero = Instantiate(prefabHero);
            hero.GetComponent<PlayerSheepController>().plane = _plane;
            SpawnSheep(hero);
            
            foreach (var sheepModel in _gameModel.sheeps)
            {
                var prefabSheep = Resources.Load<GameObject>("Prefabs/SheepBot");
                var sheep = Instantiate(prefabSheep);
                sheep.GetComponent<BotSheepController>().Init(sheepModel);
                SpawnSheep(sheep);
            }
        }

        private void SpawnSheep(GameObject sheep)
        {
            if (_spawnPoints.Count <= 0)
            {
                Debug.LogError("Not enough spawn points in scene!");
                return;
            }
            sheep.transform.position = _spawnPoints.Dequeue();
            sheep.transform.localScale = Vector3.one;
            sheep.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0,360), 0f);
            sheep.SetActive(true);
        }

        private void Update()
        {
            _gameModel.Update();
        }
    }
}