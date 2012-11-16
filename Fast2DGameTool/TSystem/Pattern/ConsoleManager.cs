using System;
using System.Collections.Generic;

namespace Tool.TSystem.Pattern
{
    public class ConsoleManager
    {
        public struct CommandInfo
        {
            public string command;
            public string help;
            public int argCount;

            public CommandInfo(string _cmd, int _count, string _help)
            {
                command = _cmd;
                argCount = _count;
                help = _help;
            }
        }

        private readonly List<IConsole> m_consoles;
        private readonly List<string> m_outputs;
        private readonly Dictionary<string, CommandInfo> m_commands;

        public ConsoleManager()
        {
            m_consoles = new List<IConsole>();
            m_outputs = new List<string>();
            m_commands = new Dictionary<string, CommandInfo>();
        }

        public void Add(IConsole console)
        {
            m_consoles.Add(console);
            console.Register(this);
        }

        public void Remove(IConsole console)
        {
            console.UnRegister(this);
            m_consoles.Remove(console);
        }

        public Dictionary<string, CommandInfo> Commands
        {
            get { return m_commands; }
        }

        public List<string> Outputs
        {
            get { return m_outputs; }
        }

        public void Command(string line)
        {
            m_outputs.Clear();

            if ( ShowCommandHelp(line) ) return;

            string[] cmd = line.Split(new char[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            if ( cmd.Length == 0 ) return;

            CommandInfo info;
            if (FindCommand(cmd[0], out info))
            {
                if (info.argCount != cmd.Length - 1)
                {
                    m_outputs.Add(info.help);
                    return;
                }
            }
            else
            {
                localCommand(cmd[0]);
                return;
            }


            foreach (IConsole console in m_consoles)
            {
                if (console.ConsoleExecute(this,cmd))
                {
                    break;
                }
            }
        }

        private bool ShowCommandHelp(string line)
        {
            if (line == "help" || line == "?")
            {
                foreach (CommandInfo info in m_commands.Values)
                {
                    m_outputs.Add(info.help);
                }
                return true;
            }
            return false;
        }

        private void localCommand(string cmd)
        {
            if (cmd == "help")
            {
                foreach (CommandInfo info in m_commands.Values)
                {
                    m_outputs.Add( "\t" + info.help );    
                }
            }
            else
            {
                m_outputs.Add("존재하지 않는 Command 입니다.");
            }
        }

        public void RegisterCommand(string cmd, int i, string help)
        {
            m_commands.Add(cmd, new CommandInfo(cmd, i, help));
        }

        public void UnRegister(string cmd)
        {
            m_commands.Remove(cmd);
        }

        public bool FindCommand(string cmd, out CommandInfo info)
        {
            if (m_commands.TryGetValue(cmd, out info))
            {
                return true;
            }
            return false;
        }

        public bool SameCommand(string str, out string outCmd)
        {
            foreach (string command in m_commands.Keys)
            {
                if (command.IndexOf(str) >= 0)
                {
                    outCmd = command;
                    return true;
                }
            }
            outCmd = string.Empty;
            return false;
        }

        public void Clear()
        {
            foreach (IConsole console in m_consoles)
            {
                console.UnRegister( this );
            }
            m_consoles.Clear();
        }
    }
}
