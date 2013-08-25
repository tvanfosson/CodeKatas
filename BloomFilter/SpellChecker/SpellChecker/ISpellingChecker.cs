namespace SpellChecker
{
    public interface ISpellingChecker
    {
        void Add(string word);
        bool Check(string word);
    }
}