#define VERSION "1.0.0"
/*
Version history:
2021-06-10  1.0.0 - Make generic, remove references to l4d. Bump to v1.
2021-02-23  0.0.2 - Minor metadata and comment tweaks.
2020-11-15  0.0.1 - Initial release.
*/

/* PrintToChat colors:
x01 = Default (White)
x02 = Team? (Blue/Red)
x03 = Light Green
x04 = Green (yellow/orange in L4D)
*/

#pragma semicolon 1

#include <sourcemod>
#include <sdktools>

#pragma newdecls required


public Plugin myinfo = 
{
	name = "[SM] Print Info",
	author = "The Danner",
	description = "Prints misc. player info. Meant to be consumed and parsed by other components that can execute a command via rcon.",
	version = VERSION,
	url = "https://github.com/thedanner/PurpleFrancis"
}


public void OnPluginStart()
{
	RegConsoleCmd(
		"sm_printinfo",
		Command_PrintInfo,
		"Prints player information.");
	
	CreateConVar(
		"sm_printinfo_version",
		VERSION,
		"Print Info plugin version",
		FCVAR_SPONLY|FCVAR_NOTIFY|FCVAR_REPLICATED|FCVAR_DONTRECORD);
}

public Action Command_PrintInfo(int client, int args)
{
	char teamName[16];
	
	ReplyToCommand(client, "[PI] BEGIN");
	// Skip client 0 since it's rcon and will never be on a team.
	for (int i = 1; i <= MaxClients; i++)
	{
		if (IsClientInGame(i))
		{
			int team = GetClientTeam(i);
			GetTeamName(team, teamName, sizeof(teamName));
			
			if (team > 0)
			{
				// Output line is "[PI] %L<%i><%s>" in SourceMod string formatting.
				// https://wiki.alliedmods.net/Format_Class_Functions_(SourceMod_Scripting)
				// %L expands to 1<2><3><> where 1 is the player's name, 2 is the player's userid,
				// and 3 is the player's Steam ID. If the client index is 0, the string will be: Console<0><Console><Console>
				// The next <> is team #. SPECTATOR = 1, SURVIVORS = 2, INFECTED = 3
				// The final <> is team name (Survivors, Infected, Spectator maybe?)
				// So, that means, if elements are indexed by <> with the name being 0th:
				// [0]: player's name
				// [1]: player's userid
				// [2]: player's steamid
				// [3]: ???
				// [4]: player's team index
				// [5]: player's team name
				
				ReplyToCommand(
					client,
					"[PI] %L<%i><%s>",
					i,
					team,
					teamName
				);
			}
		}
	}
	
	ReplyToCommand(client, "[PI] END");
	
	return Plugin_Handled;
}
