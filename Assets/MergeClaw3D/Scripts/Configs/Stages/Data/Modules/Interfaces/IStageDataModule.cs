namespace MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Interfaces
{
    public interface IStageDataModule
    {
        #if UNITY_EDITOR
        public void ManualValidate();
        #endif
    }
}
