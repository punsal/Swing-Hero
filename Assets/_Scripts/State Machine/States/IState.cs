/// <summary>
/// An interface for states, to be used as a template.
/// </summary>
public interface IState {
    void OnEnter();
    void OnExecute();
    void OnExit();
}