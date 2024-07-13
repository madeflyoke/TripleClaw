using Cysharp.Threading.Tasks;
using DG.Tweening;
using MergeClaw3D.Scripts.Configs.Items;
using MergeClaw3D.Scripts.Configs.Stages;
using MergeClaw3D.Scripts.Events;
using MergeClaw3D.Scripts.Factories;
using MergeClaw3D.Scripts.Items;
using MergeClaw3D.Scripts.Items.Data;
using MergeClaw3D.Scripts.Items.Enums;
using MergeClaw3D.Scripts.Items.Spawner;
using System;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MergeClaw3D.Scripts.Spawner
{
    public class ItemsSpawner
    {
        private const float _SPHERE_CAST_RADIUS = 0.3f;
        private const int _NUMBER_QUARTER_SQUARES = 1;
        private const int _NUMBER_SQUARES_IN_SQUARE = 4;
        private const int _SMALL_SQUARE_COUNT = _NUMBER_SQUARES_IN_SQUARE * _NUMBER_QUARTER_SQUARES;

        private readonly float _SmallSquareSide;

        private ItemsFactory _factory;
        private ItemsConfig _itemsConfig;
        private ItemsContainer _itemsContainer;
        private ItemsSpawnerConfig _spawnerConfig;
        private ItemsSpawnPoint _spawnPoint;
        private CancellationTokenSource _cts;

        public ItemsSpawner(ItemsConfig itemsConfig, ItemsSpawnerConfig spawnerConfig, ItemsFactory itemsFactory, 
            ItemsContainer itemsContainer, ItemsSpawnPoint spawnPoint)
        {
            _SmallSquareSide = spawnPoint.SquareSide / (_NUMBER_SQUARES_IN_SQUARE / 2f);
            
            _itemsConfig = itemsConfig;
            _spawnerConfig = spawnerConfig;

            _factory = itemsFactory;
            _spawnPoint = spawnPoint;
            _itemsContainer = itemsContainer;
        }

        public async void Spawn(StageData stageData) //TODO Make all items to spawn immediately and return list to store somewhere (i.e. if list count==0 its win)
        {
            _cts = new();

            var squareLeftPoints = GetSquaresLeftBottomPoints();
            var reusedSpawnData = new GameObjectSpawnData
            {
                Parent = _spawnPoint.transform,
                Scale = Vector3.zero,
            };

            var totalItemsCount = stageData.TotalItemsCount;

            var itemSizesRatiosIndexes = SpreadItemsSizeRatios(stageData, totalItemsCount);
            var targetItemConfigs = _itemsConfig.GetRandomItemsData(stageData.ItemsVariantsCount);

            for (int i = 0, centerIndex = 0; i < totalItemsCount; i++, centerIndex++)
            {
                if (centerIndex == _SMALL_SQUARE_COUNT)
                {
                    centerIndex = 0;
                }

                var x = Random.Range(0f, _SmallSquareSide);
                var z = Random.Range(0f, _SmallSquareSide);

                var randomPosition = squareLeftPoints[centerIndex] + new Vector3(x, _spawnPoint.transform.position.y, z);
                randomPosition = CorrectSpawnByCloseItems(randomPosition);

                reusedSpawnData.Position = randomPosition;
                reusedSpawnData.Rotation = Random.rotation;

                var itemSize = ItemSize.MEDIUM;

                for (int j = 0; j < itemSizesRatiosIndexes.Count; j++)
                {
                    if (i < itemSizesRatiosIndexes[j].Key)
                    {
                        itemSize = itemSizesRatiosIndexes[j].Value;
                        break;
                    }
                }

                var itemSpecifications = new ItemSpecificationData(itemSize);
                var itemConfig = targetItemConfigs[Random.Range(0, targetItemConfigs.Count)];
                var item = _factory.Create(reusedSpawnData, itemConfig, itemSpecifications);
                item.Show(_spawnerConfig.ItemScaleDuration);

                _itemsContainer.AddItem(item);

                await AwaitDelayBeforeSpawn();
            }

            foreach (var item in _itemsContainer.Items)
            {
                if (item.ShowTween.IsActive() == false)
                {
                    continue;
                }

                await item.ShowTween.AsyncWaitForCompletion();
            }

            MessageBroker.Default.Publish(ItemsSpawned.Create());
        }

        private List<KeyValuePair<int, ItemSize>> SpreadItemsSizeRatios(StageData stageData, int totalItemsCount)
        {
            var itemSizesRatiosIndexes = new List<KeyValuePair<int, ItemSize>>();
            var previousSizeIndex = 0;

            for (int i = 0; i < stageData.ItemSizeRatios.Count; i++)
            {
                var ratio = stageData.ItemSizeRatios[i];
                var sizeIndex = Mathf.RoundToInt(ratio.RatioPartPercent / 100f * totalItemsCount);
                itemSizesRatiosIndexes.Add(new KeyValuePair<int, ItemSize>(sizeIndex + previousSizeIndex, ratio.ItemSize));
                previousSizeIndex = sizeIndex;
            }

            return itemSizesRatiosIndexes;
        }

        private Vector3 CorrectSpawnByCloseItems(Vector3 generatedPosition)
        {
            var colliders = Physics.OverlapSphere(generatedPosition, _SPHERE_CAST_RADIUS);

            if (colliders == null || colliders.Length == 0)
            {
                return generatedPosition;
            }

            generatedPosition = Vector3.zero;
            var maxHeight = _spawnPoint.transform.position.y;

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
            var squareSideHalf = _spawnPoint.SquareSide / 2f;
            var startPoint = _spawnPoint.transform.position - new Vector3(squareSideHalf, 0f, squareSideHalf);

            for (int x = 0; x < smallSquareCountHalf; x++)
            {
                for (int z = 0; z < smallSquareCountHalf; z++)
                {
                    var smallSquareLeftBottomPoint = startPoint + new Vector3(_SmallSquareSide * x, 0f, _SmallSquareSide * z);

                    squarsPoints.Add(smallSquareLeftBottomPoint);
                }
            }

            return squarsPoints;
        }

        private async UniTask AwaitDelayBeforeSpawn()
        {
            if (_itemsContainer.ItemCount % _spawnerConfig.PackCount != 0)
            {
                return;
            }

            var canceled = await UniTask.Delay(TimeSpan.FromSeconds(_spawnerConfig.DelayBeforeEachPackSpawn), cancellationToken: _cts.Token)
                .SuppressCancellationThrow();
        }
    }
}
