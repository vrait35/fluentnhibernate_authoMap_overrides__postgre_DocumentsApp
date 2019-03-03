using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextLibrary.MongoDb
{
    public class MongoDbContext
    {
        IMongoDatabase database;
        IGridFSBucket gridFS;

        public MongoDbContext()
        {
            string connectionString = "mongodb://localhost/filesdb";

            var connection = new MongoUrlBuilder(connectionString);

            MongoClient client = new MongoClient(connectionString);

            database = client.GetDatabase(connection.DatabaseName);


            gridFS = new GridFSBucket(database);

        }

        public async Task<string> CreateFile(byte[] bytes, string fileName)
        {
            ObjectId id = await gridFS.UploadFromBytesAsync(fileName, bytes);
            return (id).ToString();
        }

        public async Task<byte[]> GetFile(string id)
        {
            ObjectId objectId = new ObjectId();
            if (ObjectId.TryParse(id, out objectId))
                return await gridFS.DownloadAsBytesAsync(objectId);
            return null;
        }       

    }
}
