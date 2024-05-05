using Godot;
using System;

public partial class InteractionAreaComponent : Area2D
{
  public void Interact(Node2D interactor)
  {
    ((InteractableObject)GetParent()).Interact(interactor);
  }
}
