
namespace Library.Utils
{
    class Validations
    {
        private static Validations Instance;
        private Validations {}
    public static Validations GetInstance()
    {
        if (Instance == null)
            Instance = new Validations();
        return Instance;
    }
    public bool IsPasswordValid(string password)
    {
        // Password must be at least 6 characters long, can have special chars except ',' and ';', no spaces.
        // Password must contain both characters and numbers.
        if (string.IsNullOrEmpty(password))
            return false;
        var regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[a-zA-Z])(?=.*\d)[^\s,;]{6,}$");
        return regex.IsMatch(password);
    }
}
}