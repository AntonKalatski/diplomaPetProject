namespace GameSM.Interfaces
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}