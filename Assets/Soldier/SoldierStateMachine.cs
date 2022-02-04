using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStateMachine
{
    private ISoldierState _currentState;

    private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
    ///transitions possible from current state
    private List<Transition> _currentTransitions = new List<Transition>();
    ///transitions no matter which current state is
    private List<Transition> _anyTransitions = new List<Transition>();

    private static List<Transition> EmptyTransitions = new List<Transition>(0);

    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
            SetState(transition.To);

        _currentState?.Tick();
    }

    private void SetState(ISoldierState state)
    {
        if (state == _currentState) return;

        _currentState?.OnExit();
        _currentState = state;

        ///in case we cannot change current state
        _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
        if (_currentTransitions == null)
            _currentTransitions = EmptyTransitions;     ///to use EmptyTransitions is faster than creating new list

        _currentState.OnEnter();
    }

    public void AddTransition(ISoldierState from, ISoldierState to, Func<bool> predicate)
    {
        ///trying to get list of transitions for from state
        if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            _transitions[from.GetType()] = transitions;
        }

        ///add new transition to list which now lies in dictionary under from.GetType() key
        transitions.Add(new Transition(to, predicate));
    }

    public void AddAnyTransition(ISoldierState state, Func<bool> predicate) => 
        _anyTransitions.Add(new Transition(state, predicate));

    private class Transition
    {
        public Func<bool> Condition { get; }
        public ISoldierState To { get; }

        public Transition(ISoldierState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }

    private Transition GetTransition()
    {
        foreach (var transition in _anyTransitions)
            if (transition.Condition())
                return transition;

        foreach (var transition in _currentTransitions)
            if (transition.Condition())
                return transition;

        return null;
    }
}
