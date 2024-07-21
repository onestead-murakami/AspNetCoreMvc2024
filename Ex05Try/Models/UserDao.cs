using System;
using System.IO;
using System.Text;
using System.Text.Json;

#nullable disable
namespace MyApp.Namespace
{
    public class UserDao
    {
        private readonly string source;

        public UserDao(string contentRootPath) {
            this.source = Path.Combine(contentRootPath, "Models", "Datas", "user.json");
        }

        public List<UserData> findAll() {
            List<UserData> result = null;
            using (var reader = new FileStream(this.source, FileMode.Open, FileAccess.Read, FileShare.None)) {
                result = JsonSerializer.Deserialize<List<UserData>>(reader);
            };
            return result;
        }

        public UserData find(string userId) {
            UserData result = findAll().Find(x => x.UserId == userId);
            return result;
        }

        public void insert(UserData userData) {
            using (var reader = new FileStream(this.source, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var writer = new FileStream(this.source, FileMode.Open, FileAccess.Write, FileShare.ReadWrite)) {
                var list = JsonSerializer.Deserialize<List<UserData>>(reader);
                userData.No = (list.Max(x => x.No) + 1);
                list.Add(userData);
                writer.SetLength(0);
                JsonSerializer.Serialize(writer, list);
            };
        }

        public void update(UserData userData) {
            using (var reader = new FileStream(this.source, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var writer = new FileStream(this.source, FileMode.Open, FileAccess.Write, FileShare.ReadWrite)) {
                var list = JsonSerializer.Deserialize<List<UserData>>(reader);
                int index = list.FindIndex(x => x.UserId == userData.UserId);
                list[index].UserName = userData.UserName;
                list[index].MailAddress = userData.MailAddress;
                writer.SetLength(0);
                JsonSerializer.Serialize(writer, list);
            };
        }

        public void delete(UserData userData) {
            using (var reader = new FileStream(this.source, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var writer = new FileStream(this.source, FileMode.Open, FileAccess.Write, FileShare.ReadWrite)) {
                var list = JsonSerializer.Deserialize<List<UserData>>(reader);
                int index = list.FindIndex(x => x.UserId == userData.UserId);
                list.RemoveAt(index);
                writer.SetLength(0);
                JsonSerializer.Serialize(writer, list);
            };
        }

    }
}
