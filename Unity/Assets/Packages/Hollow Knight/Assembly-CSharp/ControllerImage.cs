using System;
using GlobalEnums;
using UnityEngine;

[Serializable]
public class ControllerImage
{
	[SerializeField]
	public string name;

	[SerializeField]
	public GamepadType gamepadType;

	[SerializeField]
	public Sprite sprite;

	[SerializeField]
	public ControllerButtonPositions buttonPositions;

	public float displayScale = 1f;

	public float offsetY;

	public bool canRemap = true;
}
