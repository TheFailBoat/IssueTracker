namespace IssueTracker.API.Utilities
{
    public class CharUtils
    {
        public static char MapToAlphaNum(int i)
        {
            const string range = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return range[i%range.Length];
        }
    }
}