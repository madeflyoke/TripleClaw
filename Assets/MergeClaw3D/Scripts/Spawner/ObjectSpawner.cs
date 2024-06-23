using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MergeClaw3D
{
    public class ObjectSpawner : MonoBehaviour
    {
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
        [SerializeField] private Transform objectPrefab;

        private const int NUMBER_QUARTER_SQUARES = 1;
        private const int NUMBER_SQUARES_IN_SQUARE = 4;
        private const int SMALL_SQUARE_COUNT = NUMBER_SQUARES_IN_SQUARE * NUMBER_QUARTER_SQUARES;

        private float SmallSquareSide => squareSide / (NUMBER_SQUARES_IN_SQUARE / 2f);

        private void Start()
        {
            if (enableTestSpawnOnStart)
            {
                Spawn();
            }
        }

        private async void Spawn()
        {
            var squareCenters = GetSquaresLeftBottomPoints();

            for (int i = 0, centerIndex = 0; i < spawnCount; i++, centerIndex++)
            {
                if (centerIndex == SMALL_SQUARE_COUNT)
                {
                    centerIndex = 0;
                }

                var x = Random.Range(0f, SmallSquareSide);
                var z = Random.Range(0f, SmallSquareSide);

                var instance = Instantiate(objectPrefab, spawnContainer);
                instance.localScale = Vector3.zero;
                instance.position = squareCenters[centerIndex] + new Vector3(x, 0f, z);
                instance.DOScale(Vector3.one, objectScaleDuration);

                if ((i + 1) % packCount == 0)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(delayBeforeEachPackSpawn));
                }
            }
        }

        private List<Vector3> GetSquaresLeftBottomPoints()
        {
            var squarsPoints = new List<Vector3>();

            var smallSquareCountHalf = SMALL_SQUARE_COUNT / 2;

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
                var center = new Vector3(leftBottomPoint.x + smallSquareSideHalf, leftBottomPoint.y, leftBottomPoint.z + smallSquareSideHalf);

                Gizmos.DrawCube(center, new(SmallSquareSide, 0f, SmallSquareSide));
                Gizmos.color = colors[colorIndex];

                colorIndex = colorIndex == colors.Length - 1 ? 0 : colorIndex + 1;
            }
        }
    }
}
