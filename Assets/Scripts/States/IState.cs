public interface IState
{
    public void Enter();
    public void Process(float dt);

    public void FixProcess(float dt);
    public void Exit();
}
