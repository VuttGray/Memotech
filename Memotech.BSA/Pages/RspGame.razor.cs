using Memotech.BSA.Helpers;

namespace Memotech.BSA.Pages
{
    public partial class RspGame : IDisposable
    {
        readonly List<GameHandler> games = new()
        {
            new GameHandler {Choose = RspOptions.Paper, LooseCondition = RspOptions.Scissor, WinCondition = RspOptions.Rock, Image="./images/paper.png"},
            new GameHandler {Choose = RspOptions.Rock, LooseCondition = RspOptions.Paper, WinCondition = RspOptions.Scissor, Image="./images/rock.png"},
            new GameHandler {Choose = RspOptions.Scissor, LooseCondition = RspOptions.Rock, WinCondition = RspOptions.Paper, Image="./images/scissors.png"},
        };

        readonly System.Timers.Timer timer;
        readonly Random rnd;
        
        GameHandler opponent;
        int imageIndex = 0;
        string gameResultMessage = "";
        string resultStyle = "";

        public RspGame()
        {
            opponent = games[0];
            rnd = new Random(DateTime.Now.Millisecond);

            timer = InitializeTimer();
            timer.Start();
        }

        System.Timers.Timer InitializeTimer()
        {
            var timer = new System.Timers.Timer() { Interval = 100 };
            timer.Elapsed += ElapsedTimer;
            return timer;
        }

        async void ElapsedTimer(object? sender, System.Timers.ElapsedEventArgs args)
        {
            imageIndex = rnd.Next(0, games.Count);
            opponent = games[imageIndex];
            await InvokeAsync(StateHasChanged);
        }

        void SelectedHandler(GameHandler game)
        {
            if (opponent == null)
            {
                throw new NullReferenceException(nameof(opponent));
            }

            timer.Stop();
            RspGameStates gameResult = game.GameResult(opponent);

            switch (gameResult)
            {
                case RspGameStates.Victory:
                    gameResultMessage = "You are WON";
                    resultStyle = "success";
                    break;
                case RspGameStates.Loss:
                    gameResultMessage = "You are LOST!";
                    resultStyle = "danger";
                    break;
                case RspGameStates.Draw:
                    gameResultMessage = "Draw!";
                    resultStyle = "primary";
                    break;
            };
        }

        void ResetGame()
        {
            timer.Start();
            gameResultMessage = "";
            resultStyle = "";
        }

        public void Dispose()
        {
            if (timer != null)
            {
                timer.Dispose();
            }
        }
    }
}
