using GameState;
using Services;
using Services.GameCamera;

namespace Bootstrap
{
    public class Game
    {
        public static IInputService InputService { get; set; }
        public static ICameraService CameraService { get; set; }
        public GameStateMachine GameStateMachine { get; }

        public Game()
        {
            GameStateMachine = new GameStateMachine();
        }
    }
}