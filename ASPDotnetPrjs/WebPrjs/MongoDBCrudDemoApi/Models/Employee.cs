using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBCrudDemoApi.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? _id { get; set; }

        [BsonElement("ecode")]
        public int Ecode { get; set; }

        [BsonElement("ename")]
        public string Ename { get; set; }

        [BsonElement("salary")]
        public int Salary {  get; set; }

        [BsonElement("deptid")]
        public int Deptid { get; set; } = 0;
    }
}
