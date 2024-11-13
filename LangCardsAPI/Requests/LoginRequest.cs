using System.ComponentModel.DataAnnotations;

namespace LangCardsAPI.Requests;

public record ApplicationUserRegisterRequest(
    [Required] [EmailAddress] string Email,
    [Required] string Password,
    string LastName = "",
    string FirstName = "");