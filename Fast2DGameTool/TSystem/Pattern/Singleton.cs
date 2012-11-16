namespace Tool.TSystem.Pattern
{
	public class Singleton<T> where T : new ()
    {
        private static T instance; 
	    public static T Instance
	    {
	        get { return instance; }
	    }     
        static Singleton()
        {
            instance = new T();
        }     
        protected Singleton() { }
	}
}