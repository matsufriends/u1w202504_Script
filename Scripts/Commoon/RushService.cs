using unityroom.Api;

namespace u1w202504
{
    public sealed class RushService
    {
        public int RushCount { get; private set; }
        
        public void ResetRushCount()
        {
            RushCount = 0;
        }
        
        public void AddRushCount()
        {
            RushCount++;
            UnityroomApiClient.Instance.SendScore(2, RushCount, ScoreboardWriteMode.HighScoreDesc);
        }
    }
}