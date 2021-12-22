namespace Semantics.Models
{
    public sealed class Unit
    {
        private static Unit _instance;
        private static readonly object Padlock = new();

        private Unit() { }
        
        public static Unit Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ??= new Unit();
                }
            }
        }
    }
}