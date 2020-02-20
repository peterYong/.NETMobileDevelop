using SkillPool.Core.Helper;
using SkillPool.Core.Validations;
using SkillPool.Core.ViewModels.Base;
using SkillPool.Model.IM;
using SkillPool.Model.User;
using SkillPool.Server.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkillPool.Core.ViewModels
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        #region Property

        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;
        private bool _isMock;
        private bool _isValid;
        private bool _isLogin;
        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public bool IsMock
        {
            get
            {
                return _isMock;
            }
            set
            {
                _isMock = value;
                RaisePropertyChanged(() => IsMock);
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public bool IsLogin
        {
            get
            {
                return _isLogin;
            }
            set
            {
                _isLogin = value;
                RaisePropertyChanged(() => IsLogin);
            }
        }

        #endregion

        private readonly ISettingsService _settingsService;
        public LoginViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            _userName.Value = "cms18@wotrus.com";

            InvalidateMock();
            AddValidations();
            //IsBusy = true;  //显示活动指示器 
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is LogoutParameter logoutParameter)
            {
                if (logoutParameter.Logout)
                {
                    Logout();
                }
            }
            return base.InitializeAsync(navigationData);
        }
        /// <summary>
        /// 登出
        /// </summary>
        private void Logout()
        {
            if (_settingsService.UseMocks)
            {
                _settingsService.AuthAccessToken = string.Empty;
            }
        }

        /// <summary>
        /// 验证是否用模拟数据
        /// </summary>
        public void InvalidateMock()
        {
            IsMock = _settingsService.UseMocks;
        }
        /// <summary>
        /// 增加验证规则
        /// </summary>
        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
            //_password.Validations.Add(new PassWordRule<string> { ValidationMessage = "密码最少8位且必须同时包含数字和字母" });
        }


        #region Command
        /// <summary>
        /// Settings标签的行为
        /// </summary>
        public ICommand SettingsCommand => new Command(async () => await SettingsAsync());
        private async Task SettingsAsync()
        {
            await NavigationService.NavigateToAsync<SettingsViewModel>().ConfigureAwait(false);
        }


        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());

        private bool ValidateUserName()
        {
            return _userName.Validate();
        }

        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        public ICommand MockSignInCommand => new Command(async () => await MockSignInAsync());

        /// <summary>
        /// 模拟登陆
        /// </summary>
        /// <returns></returns>
        private async Task MockSignInAsync()
        {
            IsBusy = true;
            IsValid = true;
            bool isVilid = Validate();
            //登录是否成功
            bool isAuthenticated = false;
            if (isVilid)
            {
                try
                {
                    await Task.Delay(10);
                    //isAuthenticated = true;
                    isAuthenticated = Login(UserName.Value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[SignIn] Error signing in: {ex}");
                }
            }
            else
            {
                IsValid = false;
            }
            if (isAuthenticated)
            {
                _settingsService.AuthAccessToken = GlobalSetting.Instance.AuthToken;

                await NavigationService.NavigateToAsync<MainViewModel>();
                //await NavigationService.RemoveBackStackAsync();
            }

            IsBusy = false;
        }

        /// <summary>
        /// 真实的登录
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool Login(string email)
        {
            bool result = false;
            string url = "http://120.79.67.39/api/user" + "?email=" + email;
            //string url = "http://10.0.2.2:5000/api/user" + "?email=" + email;
            string response = WebApiHelper.InvokeApi(url);
            var temp = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(response);
            if (temp.Code == 200)
            {
                result = true;
                Newtonsoft.Json.Linq.JObject jObject = temp.Data;
                GlobalSetting.Instance.IM_USER = jObject.ToObject<IM_USER>();
            }
            return result;
        }

        private bool Validate()
        {
            bool isValidUser = ValidateUserName();
            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword;
        }


        #endregion

    }
}
