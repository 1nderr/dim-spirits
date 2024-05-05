using Godot;
using System;

public abstract partial class InteractableObject : StaticBody2D
{
  protected bool interactable = true;
  public abstract void Interact(Node interactor);
}
