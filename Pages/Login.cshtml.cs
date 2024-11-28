using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace SqlInjection.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        public string? Message { get; set; }

        public IActionResult OnPost()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Vulnerable SQL query (for demonstration purposes)
                    string query = $"SELECT COUNT(*) FROM Users WHERE Username = '{Username}' AND Password = '{Password}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        // Redirect to Admin page on successful login
                        return RedirectToPage("/Admin");
                    }
                    else
                    {
                        // Display error message on login failure
                        Message = "Invalid username or password.";
                    }
                }
                catch (Exception ex)
                {
                    Message = "Error: " + ex.Message;
                }
            }

            // Stay on the same page if login fails
            return Page();
        }
    }
}
