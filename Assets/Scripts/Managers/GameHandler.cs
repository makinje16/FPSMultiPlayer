using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

	private const string PLAYER_ID_PREFIX = "Player ";
	
	private static Dictionary<string, PlayerManager> players = new Dictionary<string, PlayerManager>();

	public static void registerPlayer(string _netID, PlayerManager player)
	{
		string playerID = PLAYER_ID_PREFIX + _netID;
		players.Add(playerID, player);
		player.transform.name = playerID;
	}

	public static void unregisterPlayer(string _ID)
	{
		players.Remove(_ID);
	}

	public static PlayerManager getPlayer(string _playerID)
	{
		return players[_playerID];
	}

	private void OnGUI()
	{
		GUILayout.BeginArea(new Rect(200, 200, 200, 500));
		GUILayout.BeginVertical();

		foreach (string _playerID in players.Keys)
		{
			GUILayout.Label(_playerID + "    -    " + players[_playerID].transform.name);
		}
		
		GUILayout.EndVertical();
		GUILayout.EndArea();

	}
}
