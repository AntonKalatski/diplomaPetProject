﻿using System;
using System.Collections.Generic;
using Bootstrap;
using Factories.Interfaces;
using GameSM.Interfaces;
using GameSM.States;
using Services.Configs;
using Services.Configs.Zombie;
using Services.GameProgress;
using Services.GameServiceLocator;
using Services.SaveLoad;
using UI.Loading;

namespace GameSM
{
    public class GameStateMachine
    {
        private readonly ServiceLocator serviceLocator;
        private Dictionary<Type, IExitableState> states;
        private IExitableState activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, ServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
            states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, serviceLocator),
                [typeof(LoadProgressState)] = new LoadProgressState(this,
                    serviceLocator.LocateService<IGameProgressService>(),
                    serviceLocator.LocateService<ISaveLoadService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain,
                    serviceLocator.LocateService<IGameUIFactory>(), serviceLocator.LocateService<IGamePrefabFactory>(),
                    serviceLocator.LocateService<IGameProgressService>(), serviceLocator.LocateService<IConfigsService>()),
                [typeof(GameLoopState)] = new GameLoopState(this, sceneLoader, curtain, serviceLocator),
            };
        }

        public void Enter<TState>() where TState : class, IState => ChangeState<TState>().Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> =>
            ChangeState<TState>().Enter(payload);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            activeState?.Exit();

            TState state = GetState<TState>();
            activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => states[typeof(TState)] as TState;
    }
}