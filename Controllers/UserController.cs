using System.Text.Json;
using System.Text.RegularExpressions;
using ASP_P26.Data;
using ASP_P26.Data.Entities;
using ASP_P26.Models.User;
using ASP_P26.Services.Kdf;
using ASP_P26.Services.Random;
using Microsoft.AspNetCore.Mvc;

namespace ASP_P26.Controllers
{
    public class UserController(
        IRandomService randomService, 
        IKdfService kdfService,
        DataContext dataContext,
        ILogger<UserController> logger) : Controller
    {
        private readonly IRandomService _randomService = randomService;
        private readonly IKdfService _kdfService = kdfService;
        private readonly DataContext _dataContext = dataContext;
        private readonly ILogger<UserController> _logger = logger;
        
        public ViewResult SignUp()
        {
            UserSignUpPageModel pageModel = new();
            if (HttpContext.Session.Keys.Contains("UserSignUpFormModel"))
            {
                pageModel.FormModel = JsonSerializer.Deserialize<UserSignUpFormModel>(
                    HttpContext.Session.GetString("UserSignUpFormModel")!
                );
                pageModel.FormErrors = ProcessSignUpData(pageModel.FormModel!);
                ProcessSignUpData(JsonSerializer.Deserialize<UserSignUpFormModel>(
                    HttpContext.Session.GetString("UserSignUpFormModel")!
                    )! );
                HttpContext.Session.Remove("UserSignUpFormModel");
            }
            return View(pageModel);
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Register(UserSignUpFormModel model)
        {
            HttpContext.Session.SetString("UserSignUpFormModel", JsonSerializer.Serialize(model));
            return RedirectToAction(nameof(SignUp));
        }

        private Dictionary<string, string> ProcessSignUpData(UserSignUpFormModel model)
        {
            Dictionary<string, string> errors = [];
            
            #region Validation
            if (string.IsNullOrEmpty(model.UserName))
            {
                errors[nameof(model.UserName)] = "Имя не может быть пустым";
            }

            if (string.IsNullOrEmpty(model.UserEmail))
            {
                errors[nameof(model.UserEmail)] = "Email не может быть пустым";
            }
            
            if (string.IsNullOrEmpty(model.UserLogin))
            {
                errors[nameof(model.UserLogin)] = "Login не может быть пустым";
            }
            else
            {
                if (model.UserLogin.Contains(":"))
                {
                    errors[nameof(model.UserLogin)] = "Login не может иметь символ ':'";
                }
                else
                {
                    if (_dataContext.UserAccesses.Any(ua => ua.Login == model.UserLogin))
                    {
                        errors[nameof(model.UserRepeat)] = "Такой логин уже существует";
                    }
                }
            }

            if (string.IsNullOrEmpty(model.UserPassword))
            {
                errors[nameof(model.UserPassword)] = "Пароль не может быть пустым";
            }
            else
            {
                if (string.IsNullOrEmpty(model.UserRepeat))
                {
                    errors[nameof(model.UserRepeat)] = "Повторите пароль";
                }
                
                if (model.UserPassword != model.UserRepeat)
                {
                    errors[nameof(model.UserRepeat)] = "Повтор не совпадает";
                }
                
                if (model.UserPassword.Length < 6)
                {
                    errors[nameof(model.UserPassword)] = "Пароль должен содержать минимум 6 символов";
                }
            
                string password = model.UserPassword;
                bool hasDigit = Regex.IsMatch(password, @"\d");
                bool hasSpecial = Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>]");

                if (!hasDigit || !hasSpecial)
                {
                    errors[nameof(model.UserPassword)] = "Пароль должен содержать цифру и спец символ";
                }
            }

            // if (!model.Agree)
            // {
            //     errors[nameof(model.Agree)] = "Для создание аккаунта нужно принять соглашение";
            // }
            #endregion

            if (errors.Count() == 0)
            {
                Guid userId = Guid.NewGuid();
 
                UserData user = new() { 
                    Id = userId,
                    Name = model.UserName,
                    Email = model.UserEmail,
                    Birthdate = model.Birthdate,
                    RegisteredAt = DateTime.Now,
                };
                String salt = _randomService.Otp(12);
                UserAccess userAccess = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Login = model.UserLogin,
                    Salt = salt,
                    Dk = _kdfService.Dk(model.UserPassword, salt),
                    RoleId = "SelfRegistered"
                };
                // додаємо нові об'єкти до контексту
                _dataContext.Database.BeginTransaction();
                _dataContext.Users.Add(user);
                _dataContext.UserAccesses.Add(userAccess);
                // після додавання даних до контексту вони доступні у програмі, але
                // не передані до БД
                try
                {
                    _dataContext.SaveChanges();
                    _dataContext.Database.CommitTransaction();
                }
                catch (Exception ex)
                {
                    _logger.LogError("ProcessSignUpData: {ex}", ex.Message);
                    _dataContext.Database.RollbackTransaction();
                    errors["500"] = "Проблема с сохранением данных. Попробуйте позже";
                }
            }
            
            return errors;
        }
    }
}
