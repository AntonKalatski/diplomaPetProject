using AnimatorBehaviors;

namespace Zombies
{
    public interface IAnimatorStateReader
    {
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
        AnimatorState StateFor(int stateHash);
    }
}