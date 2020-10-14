using UnityEngine;
using System.Collections;

public enum GunTypes
{
	Colt,
	Sniper,
	MachineGun,
	Grenade
}

public class GameState
{
	public float timeMultiplier = 0f;
	public Vector2 playerPosition = new Vector2(0, 0);
	public bool portalMode = false;
	public int magazine = 0;
	public bool portalLanding = false;
	public GunTypes gunType = GunTypes.Sniper;
}

public class KeyStates
{
	public bool moveForward;
	public bool moveBackward;
	public bool rotateRight;
	public bool rotateLeft;
	public bool portal;
	public bool shoot;

}

[CreateAssetMenu(fileName = "StateMachine")]
public class StateMachine : ScriptableObject
{
	public KeyStates keyStates = new KeyStates();
	public GameState gameState = new GameState();
}
