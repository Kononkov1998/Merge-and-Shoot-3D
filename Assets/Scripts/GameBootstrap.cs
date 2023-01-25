using Core.Services.StateMachine;
using Core.Services.StateMachine.States;
using UnityEngine;
using Zenject;

public class GameBootstrap : MonoBehaviour, IInitializable, ICoroutineRunner
{
    private IGameStateMachine _gameStateMachine;

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine) =>
        _gameStateMachine = gameStateMachine;

    public void Initialize() =>
        _gameStateMachine.Enter<BootstrapState>();
}