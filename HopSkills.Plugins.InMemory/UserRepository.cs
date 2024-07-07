using HopSkills.CoreBusiness;

namespace HopSkills.Plugins.InMemory
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users;

        public UserRepository()
        {
            _users = GenerateRandomUsers(30);
        }

        public async Task<List<User>> GetUsers()
        {
            return _users;
        }

        public Task AddUserAsync(User user)
        {
            var maxId = _users.Max(u => u.UserId);
            user.UserId = maxId + 1;

            _users.Add(user);

            return Task.CompletedTask;
        }

        public Task DeleteUserAsync(User user)
        {
            _users.Remove(user);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<User>> GetUserByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) return await Task.FromResult(_users);

            return _users.Where(u => u.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase)
            || u.LastName.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public User? GetUserById(int userId)
        {
            return _users.FirstOrDefault(u => u.UserId == userId);
        }

        public Task UpdateUser(User user)
        {
            var use = _users.FirstOrDefault(u => u.UserId == user.UserId);
            if (use is not null)
            {
                use.FirstName = user.FirstName;
                use.LastName = user.LastName;
                use.role = user.role;
            }
            return Task.CompletedTask;
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomPhoneNumber()
        {
            return $"0{random.Next(100000000, 999999999)}";
        }

        public static User CreateRandomUser(int userId)
        {
            return new User
            {
                UserId = userId,
                FirstName = RandomString(8),
                LastName = RandomString(10),
                Address = $"{random.Next(1, 1000)} {RandomString(8)} Street",
                Email = $"{RandomString(10)}@gmail.com",
                Phone = RandomPhoneNumber(),
                company = new Company
                {
                    Address = $"{random.Next(1, 1000)} {RandomString(8)} Avenue",
                    Phone = RandomPhoneNumber(),
                    Name = RandomString(12),
                    Email = $"{RandomString(10)}@company.com",
                    Country = "CA",
                    Image = "image_url",
                    AttachedLicence = new Subscription
                    {
                        Description = "Licence",
                        Title = "Standard",
                        NumberOfUsers = random.Next(1, 100),
                        Image = "image_url"
                    }
                },
                role = new Role
                {
                    Name = "Admin",
                    UseCases = new List<UseCase>
                {
                    new UseCase
                    {
                        Name = "User",
                        rights = new Rights
                        {
                            Access = true,
                            Activate = true,
                            AddMember = true,
                            Create = true,
                            HardDelete = true,
                            Restore = true,
                            Show = true,
                            SoftDelete = true,
                            Update = true
                        }
                    }
                }
                },
                CreationDate = DateTime.Now,
                LastUpdated = DateTime.Now
            };
        }

        public static List<User> GenerateRandomUsers(int count)
        {
            var users = new List<User>();
            for (int i = 1; i <= count; i++)
            {
                users.Add(CreateRandomUser(i));
            }
            return users;
        }
    }
}
