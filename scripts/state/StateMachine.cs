using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class StateMachine : Node
{
	[Signal] public delegate void TransitionedEventHandler();

	[Export] private State initialState;

	private State currentState;
	private Dictionary<string, State> states = new();

	public override void _Ready()
	{
		foreach (State child in GetChildren().Cast<State>())
		{
			states[child.Name.ToString().ToLower()] = child;
			child.Transitioned += OnStateTransition;
		}

		if (initialState != null)
		{
			initialState.Enter();
			currentState = initialState;
		}
	}

	public override void _Process(double delta)
	{
		currentState?.Update(delta);
	}

	private void OnStateTransition(State sourceState, string newStateName)
	{
		if (sourceState != currentState)
		{
			return;
		}

		State newState = states[newStateName.ToString().ToLower()];
		if (newState == null)
		{
			return;
		}

		currentState?.Exit();
		newState.Enter();
		currentState = newState;
	}
}
