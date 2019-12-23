using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameConfig _config;
        [SerializeField] private GameObject _field;

        private GameModel _gameModel;
        [SerializeField] private Transform _plane;
        [SerializeField] private PlatformController _platformController;
        private Queue<Vector3> _spawnPoints;
        [SerializeField] private Transform _spawnPointsContainer;

        private void Start()
        {
            _gameModel = new GameModel(_config);
            _gameModel.GameOver += GameOver;

            _platformController.Init(_gameModel.platform, _field);
            _platformController.OnAppear += OnPlatformAppear;
            _platformController.OnMoveUpComplete += OnPlatformUp;
            _platformController.OnMoveDownComplete += OnPlatformDown;
            _gameModel.Start();
            ;

            CreateSheeps();
        }

        private void OnPlatformDown()
        {
            _gameModel.SetSheepState(SheepState.Walk);
        }

        private void OnPlatformUp()
        {
            _gameModel.Kill();
        }

        private void OnPlatformAppear()
        {
            _gameModel.SetSheepTarget(_platformController.transform.position);
        }

        private void CreateSheeps()
        {
            _spawnPoints = new Queue<Vector3>();
            var count = _spawnPointsContainer.childCount;
            for (var i = 0; i < count; i++)
            {
                var child = _spawnPointsContainer.GetChild(i);
                _spawnPoints.Enqueue(child.position);
            }

            var prefabHero = Resources.Load<GameObject>("Prefabs/Sven");
            var hero = Instantiate(prefabHero);
            var playerSheepController = hero.GetComponent<PlayerSheepController>();
            playerSheepController.Init(_gameModel.playerSheepModel);
            playerSheepController.plane = _plane;
            SpawnSheep(hero);

            foreach (var sheepModel in _gameModel.botSheeps)
            {
                var prefabSheep = Resources.Load<GameObject>("Prefabs/SheepBot");
                var sheep = Instantiate(prefabSheep);
                var sheepController = sheep.GetComponent<BotSheepController>();
                sheepController.Init(sheepModel);
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
            sheep.transform.rotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
            sheep.SetActive(true);
        }

        private void Update()
        {
            _gameModel.Update();
        }

        private void GameOver(GameResult gameResult)
        {
            Release();
            var sceneName = gameResult.Equals(GameResult.Win) ? "GameOverWin" : "GameOver";
            SceneManager.LoadScene(sceneName);
        }

        private void Release()
        {
            _gameModel.Release();
        }
    }
}