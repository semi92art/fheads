using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Boo.Lang;

public delegate void CommandHandler(string[] args);

public class ConsoleController {
	
	#region Event declarations
	
	public delegate void LogChangedHandler(string[] log);
	public event LogChangedHandler logChanged;
	
	public delegate void VisibilityChangedHandler(bool visible);
	public event VisibilityChangedHandler visibilityChanged;
	#endregion


	class CommandRegistration {
		public string command { get; private set; }
		public CommandHandler handler { get; private set; }
		public string help { get; private set; }
		
		public CommandRegistration(string command, CommandHandler handler, string help) {
			this.command = command;
			this.handler = handler;
			this.help = help;
		}
	}

	const int scrollbackSize = 100;

	Queue<string> scrollback = new Queue<string>(scrollbackSize);
	public List commandHistory = new List();
	Dictionary<string, CommandRegistration> commands = new Dictionary<string, CommandRegistration>();

	public string[] log { get; private set; } 
	
	public ConsoleController() {
		//When adding commands, you must add a call below to registerCommand() with its name, implementation method, and help text.
		registerCommand("help", help, "Print command list.");
		registerCommand("restart",restart,"Restart game.");
		registerCommand("load", load, "Reload specified level.");
		registerCommand("reload",reload,"Reload current level.");
		registerCommand("clc",clearConsole,"Clear console.");
	}
	
	void registerCommand(string command, CommandHandler handler, string help) {
		commands.Add(command, new CommandRegistration(command, handler, help));
	}
	
	public void appendLogLine(string line) {
		Debug.Log(line);
		
		if (scrollback.Count >= ConsoleController.scrollbackSize) {
			scrollback.Dequeue();
		}
		scrollback.Enqueue(line);
		
		log = scrollback.ToArray();
		if (logChanged != null) {
			logChanged(log);
		}
	}
	
	public void runCommandString(string commandString) {
		appendLogLine("$ " + commandString);
		
		string[] commandSplit = parseArguments(commandString);
		string[] args = new string[0];
		if (commandSplit.Length<=0)
		{
			return;
		}  else if (commandSplit.Length >= 2) {
			int numArgs = commandSplit.Length - 1;
			args = new string[numArgs];
			Array.Copy(commandSplit, 1, args, 0, numArgs);
		}
		runCommand(commandSplit[0].ToLower(), args);
		commandHistory.Add(commandString);
	}
	
	public void runCommand(string command, string[] args) {
		CommandRegistration reg = null;
		if (!commands.TryGetValue(command, out reg)) {
			appendLogLine(string.Format("Unknown command '{0}', type 'help' for list.", command));
		}  else {
			if (reg.handler == null) {
				appendLogLine(string.Format("Unable to process command '{0}', handler was null.", command));
			}  else {
				reg.handler(args);
			}
		}
	}
	
	static string[] parseArguments(string commandString)
	{
		LinkedList<char> parmChars = new LinkedList<char>(commandString.ToCharArray());
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

	void reload(string[] args) {
		if (args.Length == 0)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		else
		{
			foreach (string arg in args)
			{
				if (arg == "-h")
				{
					appendLogLine("Reload current level.");
					break;
				}
			}
		}
	}

	void help(string[] args)
	{
		if (args.Length == 0)
		{
			Debug.Log("Print: command list");
			appendLogLine("Current command list:");
			foreach (KeyValuePair<string, CommandRegistration> kvp in commands)
			{
				appendLogLine($"{kvp.Key}: {kvp.Value.help}");
			}
		}
		else
		{
			foreach (string arg in args)
			{
				if (arg == "woodpecker")
				{
					appendLogLine("Work is filled up, woodpeckers!");
					break;
				}
			}
		}
	}
	
	void restart(string[] args)
	{
		if (args.Length == 0)
		{
			Application.LoadLevel("____Preload");
		}
		else
		{
			foreach (string arg in args)
			{
				if (arg == "-h")
				{
					appendLogLine("Reload game.");
					break;
				}
			}
		}
	}

	void load(string[] args)
	{
		if (args.Length == 0)
		{
			appendLogLine("Specify level");
		}
		else
		{
			foreach (string arg in args)
			{
				if (arg == "-h")
				{
					appendLogLine("Load level:");
					appendLogLine("preload: restart game.");
					appendLogLine("menu: load menu.");
					appendLogLine("level: load field.");
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
							appendLogLine("No such level.");
							break;
					}
				}
			}
		}
	}

	void clearConsole(string[] args)
	{
		Array.Clear(log,0,log.Length);
		scrollback.Clear();
		logChanged(log);
	}
	#endregion
}
