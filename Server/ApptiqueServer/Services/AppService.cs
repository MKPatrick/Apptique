using ApptiqueServer.Config;
using ApptiqueServer.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Xml.Linq;

namespace ApptiqueServer.Services
{
    public class AppService : IAppService
    {

        private static List<AppModel> _apps = new List<AppModel>();
        private string DbName = "MBAppTique";
        public string CollectionName = "Apps";
        private readonly IWebHostEnvironment env;
        private readonly IOptions<SecretModel> _options;
        private readonly MongoClient _mongoClient;


        public AppService(IWebHostEnvironment env, IOptions<SecretModel> options)
        {
            this.env = env;
            this._options = options;
            _mongoClient = new MongoClient(_options.Value.ConnectionString);
  
        }

        public async Task RemoveAPKPhysically(string apkFile)
        {
            File.Delete($"{env.WebRootPath}\\{apkFile}");
        }


        public async Task<string> CreateAPKPhysically(IBrowserFile apkFile)
        {
            string fileName = Guid.NewGuid().ToString();
            Stream stream = apkFile.OpenReadStream(1024 * 500000);
            if (!Directory.Exists($"{env.WebRootPath}\\Apps"))
            {
                Directory.CreateDirectory($"{env.WebRootPath}\\Apps");
            }
            var path = $"{env.WebRootPath}\\Apps\\{fileName}.apk";
            FileStream fs = File.Create(path);
            await stream.CopyToAsync(fs);
            stream.Close();
            fs.Close();
            return $"Apps/{fileName}.apk";
        }

        public async Task UpdateApp(AppModel appModel)
        {
            var filter_id = Builders<AppModel>.Filter.Eq("ID", ObjectId.Parse(appModel.ID));
            IMongoCollection<AppModel> collection = FetchCollection();
            collection.ReplaceOne(filter_id, appModel);
        }

        public async Task CreateNewApp(AppModel appModel)
        {
            IMongoCollection<AppModel> collection = FetchCollection();
            await collection.InsertOneAsync(appModel);

        }

        public async Task<AppModel> GetAppByID(string id)
        {
            var filter_id = Builders<AppModel>.Filter.Eq("ID", ObjectId.Parse(id));
            IMongoCollection<AppModel> collection = FetchCollection();
            return collection.Find(filter_id).FirstOrDefault();
        }

        public async Task<List<AppModel>> GetAllApps()
        {
            IMongoCollection<AppModel> collection = FetchCollection();
            return collection.Find(new BsonDocument()).ToList();
        }



        public async Task DeleteAppById(string id)
        {
          
            var filter_id = Builders<AppModel>.Filter.Eq("ID", ObjectId.Parse(id));
            IMongoCollection<AppModel> collection = FetchCollection();
            await collection.DeleteOneAsync(filter_id);
        }


        private IMongoCollection<AppModel> FetchCollection()
        {
            var database = _mongoClient.GetDatabase(DbName);
            var collection = database.GetCollection<AppModel>(CollectionName);
            return collection;
        }


    }
}
