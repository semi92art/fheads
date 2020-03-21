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
	List commandHistory = new List();
	Dictionary<string, CommandRegistration> commands = new Dictionary<string, CommandRegistration>();

	public string[] log { get; private set; } //Copy of scrollback as an array for easier use by ConsoleView
	
	const string repeatCmdName = "!!"; //Name of the repeat command, constant since it needs to skip these if they are in the command history
	
	public ConsoleController() {
		//When adding commands, you must add a call below to registerCommand() with its name, implementation method, and help text.
		registerCommand("help", help, "Print this help.");
		registerCommand("reload", reload, "Reload game.");
		registerCommand("resetprefs", resetPrefs, "Reset & saves PlayerPrefs.");
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

	/// 
	/// A test command to demonstrate argument checking/parsing.
	/// Will repeat the given word a specified number of times.
	///
	///
	
	void reload(string[] args) {
		Application.LoadLevel(Application.loadedLevel);
	}
	
	void resetPrefs(string[] args) {
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
	}

	void help(string[] args)
	{
		string msg = "no help for dyatel";
		Debug.Log(msg);
		appendLogLine(msg);
	}

	#endregion
}
