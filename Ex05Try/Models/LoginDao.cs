using System;
using System.IO;
using System.Text;
using System.Text.Json;

#nullable disable
namespace MyApp.Namespace
{
    public class LoginDao
    {
        private readonly string source;

        public LoginDao(string contentRootPath) {
            this.source = Path.Combine(contentRootPath, "Models", "Datas", "login.json");
        }

        public LoginData find(string loginId) {
            LoginData result = null;
            using (var reader = new StreamReader(this.source, Encoding.UTF8)) {
                string jsonString = reader.ReadToEnd();
                result = JsonSerializer.Deserialize<List<LoginData>>(jsonString).Find(x => x.LoginId == loginId);
            }
            return result;
        }
    }
}
