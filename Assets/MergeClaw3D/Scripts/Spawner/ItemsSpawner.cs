using Cysharp.Threading.Tasks;
using MergeClaw3D.Scripts.Configs.Items;
using MergeClaw3D.Scripts.Factories;
using MergeClaw3D.Scripts.Items.Enums;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MergeClaw3D.Scripts.Spawner
{
    public class ItemsSpawner : SerializedMonoBehaviour
    {
        [SerializeField] private ItemsFactory _factory;
        [SerializeField] private ItemsConfig _itemsConfig;

        [SerializeField] private Transform spawnContainer;
        [SerializeField] private Transform spawnPoint;

        [Title("Spawn Settings")]
        [PropertySpace]
        [SerializeField] private float squareSide;
        [PropertySpace]
        [SerializeField] private int packCount;
        [SerializeField] private float delayBeforeEachPackSpawn;
        [SerializeField] private float objectScaleDuration;

        [Title("Test")]
        [SerializeField] private bool enableTestSpawnOnStart;
        [SerializeField] private int spawnCount = 0;

        private const float _SPHERE_CAST_RADIUS = 0.3f;
        private const int _NUMBER_QUARTER_SQUARES = 1;
        private const int _NUMBER_SQUARES_IN_SQUARE = 4;
        private const int _SMALL_SQUARE_COUNT = _NUMBER_SQUARES_IN_SQUARE * _NUMBER_QUARTER_SQUARES;

        private CancellationTokenSource _cts;

        private float SmallSquareSide => squareSide / (_NUMBER_SQUARES_IN_SQUARE / 2f);

        private void Start()
        {
            if (enableTestSpawnOnStart)
            {
                Spawn();
            }
        }

        private void OnDisable()
        {
            _cts?.Cancel();
        }

        private async void Spawn()
        {
            _cts = new();

            var squareLeftPoints = GetSquaresLeftBottomPoints();
            var reusedSpawnData = new GameObjectSpawnData
            {
                Parent = spawnContainer,
                Scale = Vector3.zero,
            };

            for (int i = 0, centerIndex = 0; i < spawnCount; i++, centerIndex++)
            {
                if (centerIndex == _SMALL_SQUARE_COUNT)
                {
                    centerIndex = 0;
                }

                var x = Random.Range(0f, SmallSquareSide);
                var z = Random.Range(0f, SmallSquareSide);

                var randomPosition = squareLeftPoints[centerIndex] + new Vector3(x, spawnPoint.position.y, z);
                randomPosition = CorrectSpawnByCloseItems(randomPosition);

                reusedSpawnData.Position = randomPosition;
                reusedSpawnData.Rotation = Random.rotation;

                _factory.Create(reusedSpawnData, _itemsConfig.GetRandomItemData(), new(ItemSize.LARGE))
                    .Show(objectScaleDuration);

                if ((i + 1) % packCount == 0)
                {
                    var canceled = await UniTask.Delay(TimeSpan.FromSeconds(delayBeforeEachPackSpawn), cancellationToken: _cts.Token)
                        .SuppressCancellationThrow();

                    if (canceled)
                    {
                        return;
                    }
                }
            }
        }

        private Vector3 CorrectSpawnByCloseItems(Vector3 generatedPosition)
        {
            var colliders =  Physics.OverlapSphere(generatedPosition, _SPHERE_CAST_RADIUS);

            if (colliders == null || colliders.Length == 0)
            {
                return generatedPosition;
            }

            generatedPosition = Vector3.zero;
            var maxHeight = spawnPoint.position.y;

            foreach (var collide in colliders)
            {
                generatedPosition += collide.transform.position;
                maxHeight = maxHeight > collide.transform.position.y ? maxHeight : collide.transform.position.y;
            }

            generatedPosition /= colliders.Length;
            generatedPosition.y = maxHeight;

            return generatedPosition;
        }

        private List<Vector3> GetSquaresLeftBottomPoints()
        {
            var squarsPoints = new List<Vector3>();

            var smallSquareCountHalf = _SMALL_SQUARE_COUNT / 2;

            var squareSideHalf = squareSide / 2f;
            var startPoint = new Vector3(spawnPoint.position.x - squareSideHalf, spawnPoint.position.y, spawnPoint.position.z - squareSideHalf);

            for (int x = 0; x < smallSquareCountHalf; x++)
            {
                for (int z = 0; z < smallSquareCountHalf; z++)
                {
                    var smallSquareLeftBottomPoint = new Vector3(startPoint.x + SmallSquareSide * x, startPoint.y, startPoint.z + SmallSquareSide * z);

                    squarsPoints.Add(smallSquareLeftBottomPoint);
                }
            }

            return squarsPoints;
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            var squareLeftBottomPoints = GetSquaresLeftBottomPoints();
            var smallSquareSideHalf = SmallSquareSide / 2f;

            var colors = new Color[]{
                Color.green,
                Color.red,
                Color.yellow,
                Color.blue,
                Color.gray,
                Color.black,
            };

            var colorIndex = 0;

            foreach (var leftBottomPoint in squareLeftBottomPoints)
            {
                var center = new Vector3(leftBottomPoint.x + smallSquareSideHalf, spawnPoint.position.y, leftBottomPoint.z + smallSquareSideHalf);

                Gizmos.DrawCube(center, new(SmallSquareSide, 0f, SmallSquareSide));
                Gizmos.color = colors[colorIndex];

                colorIndex = colorIndex == colors.Length - 1 ? 0 : colorIndex + 1;
            }
        }

#endif
    }
}
