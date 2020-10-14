#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StateMachine))]
public class StateMachineInspector : Editor
{

	private StateMachine stateMachine;
	private bool showKeyStates = true;
	private bool showPlayerState = true;

	void OnEnable()
	{
		stateMachine = (StateMachine)target;
	}

	public override void OnInspectorGUI()
	{
		EditorUtility.SetDirty(target);

		EditorGUILayout.BeginVertical("box");
		EditorGUILayout.BeginHorizontal();
		GUILayout.Space(10);
		EditorGUILayout.BeginVertical();
		showKeyStates = EditorGUILayout.Foldout(showKeyStates, "Key States", true);
		if (showKeyStates)
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(15);
			EditorGUILayout.BeginVertical();
			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.Toggle("Forward Key", stateMachine.keyStates.moveForward);
			EditorGUILayout.Toggle("Backward Key", stateMachine.keyStates.moveBackward);
			EditorGUILayout.Toggle("Right Rotate Key", stateMachine.keyStates.rotateRight);
			EditorGUILayout.Toggle("Left Rotate Key", stateMachine.keyStates.rotateLeft);
			EditorGUILayout.Toggle("Shoot Key", stateMachine.keyStates.shoot);
			EditorGUILayout.Toggle("Portal Key", stateMachine.keyStates.portal);
			EditorGUI.EndDisabledGroup();
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();


		GUILayout.Space(5);

		EditorGUILayout.BeginVertical("box");
		EditorGUILayout.BeginHorizontal();
		GUILayout.Space(10);
		EditorGUILayout.BeginVertical();
		showPlayerState = EditorGUILayout.Foldout(showPlayerState, "Player State", true);
		if (showPlayerState)
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(15);
			EditorGUILayout.BeginVertical();
			EditorGUI.BeginDisabledGroup(true);
			GUILayout.Label("Time Multiplier : " + stateMachine.gameState.timeMultiplier);
			GUILayout.Label("Player Position ::        X : " + stateMachine.gameState.playerPosition.x + "        Y : " + stateMachine.gameState.playerPosition.y);
			GUILayout.Label("GunType : " + stateMachine.gameState.gunType);
			GUILayout.Label("Remaining Bullets : " + stateMachine.gameState.magazine);
			EditorGUILayout.Toggle("Portal Mode", stateMachine.gameState.portalMode);
			EditorGUI.EndDisabledGroup();
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}

		//base.OnInspectorGUI();
	}
}
#endif