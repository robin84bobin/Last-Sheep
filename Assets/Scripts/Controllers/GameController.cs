using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
        private List<BaseSheepController> _sheeps;

        private void Start()
        {
            _gameModel = new GameModel(_config);
            _gameModel.GameOver += GameOver;
           
            _platformController.Init(_gameModel.platform, _field);
            _platformController.OnAppear += OnPlatformAppear;
            _gameModel.Update();
            
            CreateSheeps();
        }

        private void OnPlatformAppear()
        {
            _gameModel.SetSheepsTarget(_platformController.transform.position);
        }

        private void GameOver()
        {
            SceneManager.LoadScene("GameOver");
        }

        private Queue<Vector3> _spawnPoints;
        private void CreateSheeps()
        {
            _spawnPoints = new Queue<Vector3>();
            int count = _spawnPointsContainer.childCount;
            for(int i = 0; i < count; i++){
                Transform child = _spawnPointsContainer.GetChild(i);
                _spawnPoints.Enqueue(child.position);
            }
            
            var prefabHero = Resources.Load<GameObject>("Prefabs/Sven");
            var hero = Instantiate(prefabHero);
            var playerSheepController = hero.GetComponent<PlayerSheepController>();
            playerSheepController.Init(_gameModel.playerSheepModel);
            playerSheepController.plane = _plane;
            SpawnSheep(hero);
            
            _sheeps = new List<BaseSheepController>(_gameModel.botSheeps.Count);
            foreach (var sheepModel in _gameModel.botSheeps)
            {
                var prefabSheep = Resources.Load<GameObject>("Prefabs/SheepBot");
                var sheep = Instantiate(prefabSheep);
                var sheepController = sheep.GetComponent<BotSheepController>();
                    sheepController.Init(sheepModel);
                _sheeps.Add(sheepController);
                SpawnSheep(sheep);
            }
        }

        private void SpawnSheep(GameObject sheep)
        {
            if (_spawnPoints.Count <= 0){
                Debug.LogError("Not enough spawn points in scene!");
                return;
            }
            sheep.transform.position = _spawnPoints.Dequeue();
            sheep.transform.localScale = Vector3.one;
            sheep.transform.rotation = Quaternion.Euler(0f, Random.Range(0,360), 0f);
            sheep.SetActive(true);
        }

        private void Update()
        {
            _gameModel.Update();
        }
    }
}