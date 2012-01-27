namespace Cloney.Core.SubRoutines
{
    public abstract class SubRoutineBase
    {
        public bool Finished { get; private set; }


        public void Finish()
        {
            Finished = true;
        }
    }
}
