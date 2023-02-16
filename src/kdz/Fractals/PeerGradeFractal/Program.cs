namespace PeerGradeFractal
{
    internal sealed class Program
    {
        private static void Run()
        {
            var anecdote = new ScanConsole();
            while (true)
            {
                anecdote.Run();
                if (!anecdote.RunAgain())
                {
                    break;
                }
            }
        }
        
        private static void Main()
        {
            Run();
        }
    }
}