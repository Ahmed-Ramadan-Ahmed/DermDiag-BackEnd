using DermDiag.DTO;
using DermDiag.Models;

namespace DermDiag.Repository
{
    public class Authentication
    {
        private readonly DermDiagContext context1;
        public Authentication(DermDiagContext context)
        {
            context1 = context;
        }

        public bool Login(LoginDTO login)
        {
            var c1 =
            context1.Patients.FirstOrDefault((p) => p.Email == login.Email && p.Password == login.Password);
            if (c1 != null) { return true; } else { return false; }


        }
    }
}
