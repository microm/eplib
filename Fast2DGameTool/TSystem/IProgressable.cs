namespace Tool.TSystem
{
    public interface IProgressable
    {
        void ProgressStart(int maximum);
        void ProgressEnd();
        void ProgressStep();
        void Progress(int step);
    }
}
