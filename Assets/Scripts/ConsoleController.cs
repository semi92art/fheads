using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Boo.Lang;

public delegate void CommandHandler(string[] _Args);

public class ConsoleController {
	
	#region Event declarations
	
	public delegate void LogChangedHandler(string[] _Log);
	public event LogChangedHandler LogChanged;
	
	public delegate void VisibilityChangedHandler(bool _Visible);
	public event VisibilityChangedHandler VisibilityChanged;
	#endregion


	class CommandRegistration {
		public string Command { get; private set; }
		public CommandHandler Handler { get; private set; }
		public string Help { get; private set; }

		public CommandRegistration(string _Command, CommandHandler _Handler, string _Help)
		{
			this.Command = _Command;
			this.Handler = _Handler;
			this.Help = _Help;
		}
	}

	const int SCROLLBACK_SIZE = 100;

	Queue<string> scrollback = new Queue<string>(SCROLLBACK_SIZE);
	public List commandHistory = new List();
	Dictionary<string, CommandRegistration> commands = new Dictionary<string, CommandRegistration>();

	public string[] Log { get; private set; } 
	
	public ConsoleController() {
		//When adding commands, you must add a call below to registerCommand() with its name, implementation method, and help text.
		RegisterCommand("help", Help, "Print command list.");
		RegisterCommand("restart",Restart,"Restart game.");
		RegisterCommand("load", Load, "Reload specified level.");
		RegisterCommand("reload",Reload,"Reload current level.");
		RegisterCommand("clc",ClearConsole,"Clear console.");
	}
	
	void RegisterCommand(string _Command, CommandHandler _Handler, string _Help) {
		commands.Add(_Command, new CommandRegistration(_Command, _Handler, _Help));
	}
	
	public void AppendLogLine(string _Line) {
		Debug.Log(_Line);
		
		if (scrollback.Count >= ConsoleController.SCROLLBACK_SIZE) {
			scrollback.Dequeue();
		}
		scrollback.Enqueue(_Line);
		
		Log = scrollback.ToArray();
		if (LogChanged != null) {
			LogChanged(Log);
		}
	}
	
	public void RunCommandString(string _CommandString) {
		AppendLogLine("$ " + _CommandString);
		
		string[] commandSplit = ParseArguments(_CommandString);
		string[] args = new string[0];
		if (commandSplit.Length<=0)
		{
			return;
		}  else if (commandSplit.Length >= 2) {
			int numArgs = commandSplit.Length - 1;
			args = new string[numArgs];
			Array.Copy(commandSplit, 1, args, 0, numArgs);
		}
		RunCommand(commandSplit[0].ToLower(), args);
		commandHistory.Add(_CommandString);
	}
	
	public void RunCommand(string _Command, string[] _Args) {
		CommandRegistration reg = null;
		if (!commands.TryGetValue(_Command, out reg)) {
			AppendLogLine(string.Format("Unknown command '{0}', type 'help' for list.", _Command));
		}  else {
			if (reg.Handler == null) {
				AppendLogLine(string.Format("Unable to process command '{0}', handler was null.", _Command));
			}  else {
				reg.Handler(_Args);
			}
		}
	}
	
	static string[] ParseArguments(string _CommandString)
	{
		LinkedList<char> parmChars = new LinkedList<char>(_CommandString.ToCharArray());
		bool inQuote = false;
		var node = parmChars.First;
		while (node != null)
		{
			var next = node.Next;
			if (node.Value == '"') {
				inQuote = !inQuote;
				parmChars.Remove(node);
			}
			if (!inQuote && node.Value == ' ') {
				node.Value = ' ';
			}
			node = next;
		}
		char[] parmCharsArr = new char[parmChars.Count];
		parmChars.CopyTo(parmCharsArr, 0);
		return (new string(parmCharsArr)).Split(new char[] {' '} , StringSplitOptions.RemoveEmptyEntries);
	}

	#region Command handlers
	//Implement new commands in this region of the file.

	void Reload(string[] _Args) {
		if (_Args.Length == 0)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		else
		{
			foreach (string arg in _Args)
			{
				if (arg == "-h")
				{
					AppendLogLine("Reload current level.");
					break;
				}
			}
		}
	}

	void Help(string[] _Args)
	{
		if (_Args.Length == 0)
		{
			Debug.Log("Print: command list");
			AppendLogLine("Current command list:");
			foreach (KeyValuePair<string, CommandRegistration> kvp in commands)
			{
				AppendLogLine($"{kvp.Key}: {kvp.Value.Help}");
			}
		}
		else
		{
			foreach (string arg in _Args)
			{
				if (arg == "woodpecker")
				{
					AppendLogLine("Work is filled up, woodpeckers!");
					break;
				}
			}
		}
	}
	
	void Restart(string[] _Args)
	{
		if (_Args.Length == 0)
		{
			Application.LoadLevel("____Preload");
		}
		else
		{
			foreach (string arg in _Args)
			{
				if (arg == "-h")
				{
					AppendLogLine("Reload game.");
					break;
				}
			}
		}
	}

	void Load(string[] _Args)
	{
		if (_Args.Length == 0)
		{
			AppendLogLine("Specify level");
		}
		else
		{
			foreach (string arg in _Args)
			{
				if (arg == "-h")
				{
					AppendLogLine("Load level:");
					AppendLogLine("preload: restart game.");
					AppendLogLine("menu: load menu.");
					AppendLogLine("level: load field.");
					break;
				}
				else
				{
					switch (arg)
					{
						case "preload":
							Application.LoadLevel("____Preload");
							break;
						case "menu":
							Application.LoadLevel("____Menu");
							break;	
						case "level":
							Application.LoadLevel("____Level");
							break;
						default:
							AppendLogLine("No such level.");
							break;
					}
				}
			}
		}
	}

	void ClearConsole(string[] _Args)
	{
		Array.Clear(Log,0,Log.Length);
		scrollback.Clear();
		LogChanged(Log);
	}
	#endregion
}
