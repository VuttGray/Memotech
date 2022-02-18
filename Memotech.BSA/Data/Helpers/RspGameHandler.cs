namespace Memotech.BSA.Data.Helpers
{
    public class RspGameHandler
    {
        public RspOptions Choose { get; set; }
        public RspOptions WinCondition { get; set; }
        public RspOptions LooseCondition { get; set; }
        public string? Image { get; set; }

        public RspGameStates GameResult(RspGameHandler opponent)
        {
            if (Choose == opponent.Choose) return RspGameStates.Draw;
            if (LooseCondition == opponent.Choose) return RspGameStates.Loss;
            return RspGameStates.Victory;
        }
    }
}
