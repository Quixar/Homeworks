namespace ASP_P26.Models.User;

public class UserSignUpPageModel
{
    public UserSignUpFormModel? FormModel { get; set; }
    public Dictionary<string, string>? FormErrors { get; set; }
    
}