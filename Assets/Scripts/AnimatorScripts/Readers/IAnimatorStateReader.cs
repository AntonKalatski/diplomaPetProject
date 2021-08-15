using AnimatorScripts.States;

namespace AnimatorScripts.Readers
{
    public interface IAnimatorStateReader
    {
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
        AnimatorState StateFor(int stateHash);
    }
}