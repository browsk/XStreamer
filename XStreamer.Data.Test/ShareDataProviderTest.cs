using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using LinFu.IoC;
using XStreamer.Data.Exception;
using Xunit;

namespace XStreamer.Data.Test
{
    public class ShareDataProviderTest
    {
        private readonly ServiceContainer _container;
        private readonly ShareDataProvider _provider;

        public ShareDataProviderTest()
        {
            _container = new ServiceContainer();
            _container.Inject<DbProviderFactory>().Using(() => DbProviderFactories.GetFactory("System.Data.SQLite")).AsSingleton();
            _container.AddService("ConnectionString", "Data Source=ShareDataProviderTest.db3");
            _provider = new ShareDataProvider
                            {
                                ConnectionString =
                                    _container.GetService<string>("ConnectionString"),
                                DbFactory = _container.GetService<DbProviderFactory>()
                            };

            DbProviderFactory factory = _container.GetService<DbProviderFactory>();
            
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = _container.GetService<string>("ConnectionString");
                connection.Open();

                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (DbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "DROP TABLE IF EXISTS SHARES";

                        command.ExecuteNonQuery();
                    }

                    using (DbCommand command = connection.CreateCommand())
                    {
                        command.CommandText =
                            @"CREATE TABLE SHARES (
                    id INTEGER PRIMARY KEY,
                    name TEXT NOT NULL UNIQUE,
                    path TEXT NOT NULL
                    )";


                        command.ExecuteNonQuery();

                        command.CommandText = "INSERT INTO SHARES(id, name, path) VALUES(NULL, @name, @path)";
                        IDictionary<string, string> shares = new Dictionary<string, string>
                                                                 {
                                                                     {"shareA", "pathA"},
                                                                     {"shareB", "pathB"}
                                                                 };

                        foreach (var share in shares)
                        {
                            command.Parameters.Clear();

                            DbParameter parameter = _container.GetService<DbProviderFactory>().CreateParameter();
                            parameter.DbType = DbType.String;
                            parameter.ParameterName = "@name";
                            parameter.Value = share.Key;

                            command.Parameters.Add(parameter);

                            parameter = _container.GetService<DbProviderFactory>().CreateParameter();
                            parameter.DbType = DbType.String;
                            parameter.ParameterName = "@path";
                            parameter.Value = share.Value;

                            parameter.Value = share.Value;

                            command.Parameters.Add(parameter);

                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
            }
        }

        [Fact]
        void Test_Delete_Share_When_Deleting_An_Existing_Share()
        {
            // ensure that the expected data exists
            IEnumerable<string> shares = _provider.ShareNames();
            Assert.Equal(2, shares.Count());
            Assert.Contains("shareA", shares);
            
            _provider.DeleteShare("shareA");

            shares = _provider.ShareNames();
            Assert.Equal(1, shares.Count());
            Assert.DoesNotContain("shareA", shares);
        }

        [Fact]
        void Test_Delete_Share_Throws_Exception_When_Deleting_An_Non_Existant_Share()
        {
            Assert.Throws<DataStoreException>(() => _provider.DeleteShare("shareC"));
        }

        [Fact]
        public void Test_Share_Names()
        {
            IEnumerable<string> shares = _provider.ShareNames();

            Assert.Equal(2, shares.Count());
            Assert.Contains("shareA", shares);
            Assert.Contains("shareB", shares);
        }
        
        [Fact]
        public void Test_Share_Names_Returns_Empty_IEnumerable_When_No_Shares_Exist()
        {
            IEnumerable<string> shares = _provider.ShareNames();

            foreach (var share in shares)
            {
                _provider.DeleteShare(share);
            }

            shares = _provider.ShareNames();

            Assert.Equal(0, shares.Count());
        }

        [Fact]
        public void Test_Shares()
        {
            IEnumerable<ShareData> shares = _provider.Shares();

            Assert.Equal(2, shares.Count());
            Assert.Equal("pathA", shares.Single(s => s.Name == "shareA").Path);
            Assert.Equal("pathB", shares.Single(s => s.Name == "shareB").Path);
        }

        [Fact]
        public void Test_Can_Add_Share()
        {
            // validate test data 
            IEnumerable<string> shares = _provider.ShareNames();
            Assert.DoesNotContain("shareC", shares);

            _provider.AddShare("shareC", "c:\\");

            shares = _provider.ShareNames();

            Assert.Contains("shareC", shares);
        }

        [Fact]
        public void Test_Adding_Share_With_Duplicate_Name_Throws_Exception()
        {
            Assert.Throws<DataStoreException>(() =>
                                              _provider.AddShare("shareA", "c:\\"));
        }

        [Fact]
        public void Test_Can_Update_Path_For_Share()
        {
            var originalPath = _provider.Shares().Single(s => s.Name == "shareA").Path;
            const string newPath = "K:\newpath";

            _provider.UpdatePathForShare("shareA", newPath);

            var updatedPath = _provider.Shares().Single(s => s.Name == "shareA").Path;

            Assert.NotEqual(originalPath, newPath);
            Assert.Equal(newPath, updatedPath);
        }

        [Fact]
        public void Test_Updating_Path_For_Non_Existant_Share_Throws_Exception()
        {
            Assert.Throws<DataStoreException>(() => _provider.UpdatePathForShare("shareQ", ""));
        }

        [Fact]
        public void Test_Can_Update_Name_For_Share()
        {
            _provider.UpdateNameForShare("shareA", "shareX");

            var shares = _provider.ShareNames();

            Assert.Contains("shareX", shares);
        }

        [Fact]
        public void Test_Updating_Name_For_Non_Existant_Share_Throws_Exception()
        {
            Assert.Throws<DataStoreException>(() =>
                                              _provider.UpdateNameForShare("shareX", "")
                );
        }

        [Fact]
        public void Test_Updating_Name_To_A_Duplicate_Share_Name_Throws_Exception()
        {
            Assert.Throws<DataStoreException>(() =>
                                              _provider.UpdateNameForShare("shareA", "shareB")
                );
        }

        [Fact]
        public void Test_PathForShare()
        {
            var path = _provider.PathForShare("shareA");

            Assert.Equal("pathA", path);
        }

        [Fact]
        public void Test_PathForShare_Throws_Exception_For_Non_Existant_Share()
        {
            Assert.Throws<DataStoreException>(() => _provider.PathForShare("shareA"));
        }

    }
}
