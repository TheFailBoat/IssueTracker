namespace IssueTracker.WPF.Core
{
   public class ClearHistory
    {
       private static readonly ClearHistory instance = new ClearHistory();

       private ClearHistory() {}

       public static ClearHistory Instance { get { return instance; } }
    }
}
