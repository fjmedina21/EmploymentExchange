using BC = BCrypt.Net.BCrypt;

namespace DispensarioMedico.Helpers
{
    public class PasswordHashing
    {
        public string HashPassword(string password, int salt = 13)
        {
            return BC.EnhancedHashPassword(inputKey: password, workFactor: salt);
        }

        public bool ComparePassword(string password, string hash)
        {
            return BC.EnhancedVerify(text: password, hash: hash);
        }
    }
}
