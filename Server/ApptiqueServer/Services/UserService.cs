using ApptiqueServer.Config;
using ApptiqueServer.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApptiqueServer.Services
{
    public class UserService : IUserService
    {

        private static List<AppModel> _apps = new List<AppModel>();
        private string DbName = "MBAppTique";
        public string CollectionName = "Credentials";
        private readonly IWebHostEnvironment env;
        private readonly IOptions<SecretModel> _options;
        private readonly MongoClient _mongoClient;


        public UserService(IWebHostEnvironment env, IOptions<SecretModel> options)
        {
            this.env = env;
            this._options = options;
            _mongoClient = new MongoClient(_options.Value.ConnectionString);

        }

        private IMongoCollection<UserModel> FetchCollection()
        {
            var database = _mongoClient.GetDatabase(DbName);
            var collection = database.GetCollection<UserModel>(CollectionName);
            return collection;
        }


        public async Task CreateNewUser(UserModel userModel)
        {
            IMongoCollection<UserModel> collection = FetchCollection();
            await collection.InsertOneAsync(userModel);
        }

        public async Task DeleteUserById(string id)
        {
            var filter_id = Builders<UserModel>.Filter.Eq("_id", id);
            IMongoCollection<UserModel> collection = FetchCollection();
            await collection.DeleteOneAsync(filter_id);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            IMongoCollection<UserModel> collection = FetchCollection();
            return collection.Find(new BsonDocument()).ToList();
        }

        public List<UserModel> CreateAdminOnNewDatabase()
        {
            IMongoCollection<UserModel> collection = FetchCollection();
            return collection.Find(new BsonDocument()).ToList();
        }

        public async Task<UserModel> GetUserById(string id)
        {
            var filter_id = Builders<UserModel>.Filter.Eq("_id", ObjectId.Parse(id));
            IMongoCollection<UserModel> collection = FetchCollection();
            return collection.Find(filter_id).FirstOrDefault();
        }

        public UserModel GetUsersByUsername(string username)
        {

            var filter_id = Builders<UserModel>.Filter.Eq("Username", username);
            IMongoCollection<UserModel> collection = FetchCollection();
            return collection.Find(filter_id).FirstOrDefault();

        }
    }
}
