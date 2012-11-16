namespace Tool.TSystem.Pattern
{
    public interface IConsole
    {
        void Register(ConsoleManager consoleManager);
        void UnRegister(ConsoleManager consoleManager);
        bool ConsoleExecute(ConsoleManager manager ,string[] cmd);
    }
}
