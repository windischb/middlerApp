using Microsoft.EntityFrameworkCore;
using middlerApp.Core.DataAccess.Entities.Models;
using Newtonsoft.Json.Linq;
using Reflectensions;

namespace middlerApp.Core.DataAccess
{
    public class APPDbContext : DbContext
    {
        public DbSet<EndpointRuleEntity> EndpointRules { get; set; }
        public DbSet<EndpointActionEntity> EndpointActions { get; set; }

        public DbSet<TreeNode> Variables { get; set; }

        public DbSet<TypeDefinition> TypeDefinitions { get; set; }

        public APPDbContext(DbContextOptions<APPDbContext> options): base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<EndpointActionEntity>()
                .Property(p => p.Parameters)
                .HasConversion(
                    v => v.ToString(),
                    str => JObject.Parse(str)
                    );

            modelBuilder
                .Entity<TreeNode>(e => e.Ignore(p => p.Children));

            modelBuilder
                .Entity<TreeNode>()
                .Property(p => p.Content)
                .HasConversion(
                    v => ToJsonString(v),
                    str =>ToJToken(str)
                );


        }

        private string ToJsonString(JToken jToken)
        {
            var jsonString = Json.Converter.ToJson(jToken);
            return jsonString;
        }

        private JToken ToJToken(string jsonString)
        {

            var jToken = Json.Converter.ToJToken(jsonString);
            return jToken;
        }
    }
}
