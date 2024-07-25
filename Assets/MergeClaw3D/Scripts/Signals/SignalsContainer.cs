using MergeClaw3D.Scripts.Configs.Stages.Data;

namespace MergeClaw3D.Scripts.Signals
{
    public struct ResetSignal
    {
        
    }
    
    // public struct StartGameCallSignal //push play button in hub or smth
    // {
    //     
    // }
    //
    // public struct GameStartedSignal //actual start gameplay by some manager
    // {
    //   
    // }

    public struct StageStartedSignal
    {
        public readonly StageData StageData;
        
        public StageStartedSignal(StageData stageData)
        {
            StageData = stageData;
        }
    }
    
    public struct StageFailedSignal
    {
        
    }
    
    public struct StageCompletedSignal
    {
       
    }

    public struct NextStageCallSignal
    {
        
    }
}
