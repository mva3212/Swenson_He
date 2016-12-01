using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dapper;
using Npgsql;
using Swenson_He.Models;
using Attribute = Swenson_He.Models.Attribute;

namespace Swenson_He.Repositories
{
    public class ProductRepo
    {
        private readonly string _connectionString;

        public ProductRepo(string connString)
        {
            _connectionString = connString;
        }
  
        public async Task<List<Product>> SearchProductsAsync(string[] productTypes, string[] attributes  )
        {
            List<Product> results = null;
            try
            {
                var id = 0;
                using (var sqlConnection = new NpgsqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    var query =
                        @" select distinct p.id, p.name, p.sku, pt.id as producttype_id, pt.name as producttype_name from products p inner join product_types pt on pt.id = p.product_type_id inner join product_attributes pa on pa.product_id = p.id inner join attributes a on a.id = pa.attribute_id inner join attribute_types at on at.id = a.attribute_type_id where true ";
                    if (productTypes?.Length > 0)
                    {
                        
                        var result = "('" + string.Join("','", productTypes) + "')";
                        query += $" and lower(pt.name) in {result} ";
                       
                    }
                    if (attributes?.Length > 0)
                    {

                        foreach (var i in attributes)
                        {
                            query += $" and EXISTS(SELECT * FROM product_attributes t inner join attributes t2 on t.attribute_id = t2.id where t2.name = '{i}' and p.id = t.product_id ) ";
                            //query += $" and a.id = {i} ";
                        }
                    }
                    query += " order by sku";
                    var res = await sqlConnection.QueryAsync<dynamic>(query, new { });
                    Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(Product), new List<string> { "id" });
                    Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(ProductType), new List<string> { "producttype_id" });

                    results = (Slapper.AutoMapper.MapDynamic<Product>(res)).ToList();

                    sqlConnection.Close();
                }
              //  products = await GetDispensaryListingAsync(id);
            }
            catch (Exception e)
            {
                // TODO: Add Logging
               // Logger.Error("SearchProducts ", e);
            }
            return results;
        }

        public async Task<IEnumerable<ProductType>> GetProductTypesAsync()
        {
            IEnumerable<ProductType> results = null;
            try
            {
                var id = 0;
                using (var sqlConnection = new NpgsqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    var query =
                        @"select id, name from product_types;";
                    results = await sqlConnection.QueryAsync<ProductType>(query,new{});
                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                // TODO: Add Logging
                // Logger.Error("GetProductTypesAsync ", e);
            }
            return results;
        }

        public async Task<IEnumerable<AttributeType>> GetAttributeTypesAsync()
        {
            IEnumerable<AttributeType> results = null;
            try
            {
                var id = 0;
                using (var sqlConnection = new NpgsqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    var query =
                        @"select id, name from attribute_types;";
                    results = await sqlConnection.QueryAsync<AttributeType>(query, new { });
                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                // TODO: Add Logging
                // Logger.Error("GetAttributeTypesAsync ", e);
            }
            return results;
        }
        public async Task<IEnumerable<Models.Attribute>> GetAttributesAsync()
        {
            IEnumerable<Models.Attribute> results = null;
            try
            {
                var id = 0;
                using (var sqlConnection = new NpgsqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    var query =
                        @"select a.id, a.name, at.id as attributetype_id, at.name as attributetype_name from attributes a inner join attribute_types at on at.id = a.attribute_type_id;";
                    var res = await sqlConnection.QueryAsync<dynamic>(query, new { });

                    Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(Attribute), new List<string> { "id" });
                    Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(AttributeType), new List<string> { "id" });
                    results = (Slapper.AutoMapper.MapDynamic<Models.Attribute>(res)).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                // TODO: Add Logging
            }
            return results;
        }
    }
}