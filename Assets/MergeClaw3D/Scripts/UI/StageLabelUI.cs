using System;
using MergeClaw3D.Scripts.Stages;
using TMPro;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.UI
{
    public class StageLabelUI : MonoBehaviour
    {
        [Inject] private StagesManager _stagesManager;

        private const string StagePrefix = "Stage ";

        [SerializeField] private TMP_Text _stageText;

        private void Start()
        {
            _stageText.text = StagePrefix + (_stagesManager.CurrentStageGlobalIndex+1);
        }
    }
}
