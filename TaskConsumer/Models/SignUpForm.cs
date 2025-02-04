using System.ComponentModel.DataAnnotations;

namespace TaskConsumer.Models;

public class SignUpForm
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}