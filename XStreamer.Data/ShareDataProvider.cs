using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using XStreamer.Data.Exceptions;
using XStreamer.Data.Interfaces;


namespace XStreamer.Data
{
    public class ShareDataProvider : IShareDataProvider
    {
        public string ConnectionString { get; set; }
        public DbProviderFactory DbFactory { get; set; }

        #region Implementation of IShareDataProvider

        /// <summary>
        /// Adds the share.
        /// </summary>
        /// <param name="name">The name of the share.</param>
        /// <param name="path">The filesystem path for the share.</param>
        /// <exception cref="ShareExistsException">
        /// If a share with the specified name already exists
        /// </exception>
        public void AddShare(string name, string path)
        {
            using (DbConnection connection = DbFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                using (DbCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO SHARES (Name, Path) VALUES (@name, @path)";

                    DbParameter parameter = DbFactory.CreateParameter();
                    parameter.DbType = DbType.String;
                    parameter.ParameterName = "@name";
                    parameter.Value = name;

                    command.Parameters.Add(parameter);

                    parameter = DbFactory.CreateParameter();
                    parameter.DbType = DbType.String;
                    parameter.ParameterName = "@path";
                    parameter.Value = path;

                    command.Parameters.Add(parameter);

                    try
                    {
                        connection.Open();

                        using (DbTransaction transaction = connection.BeginTransaction())
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                    catch (System.Exception e)
                    {
                        throw new DataStoreException("Failed adding share.", e);
                    }
                }
                
            }
        }

        /// <summary>
        /// Deletes the share with the specified name.
        /// </summary>
        /// <param name="name">The name of the share to delete.</param>
        /// <exception cref="ShareNotFoundException">
        /// When the specified share does not exist
        /// </exception>
        public void DeleteShare(string name)
        {
            using (DbConnection connection = DbFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                using (DbCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM SHARES WHERE name = @name";

                    DbParameter parameter = DbFactory.CreateParameter();
                    parameter.DbType = DbType.String;
                    parameter.ParameterName = "@name";
                    parameter.Value = name;

                    command.Parameters.Add(parameter);

                    try
                    {
                        connection.Open();

                        using (DbTransaction transaction = connection.BeginTransaction())
                        {
                            int rows = command.ExecuteNonQuery();

                            if (rows != 1)
                                throw new DataStoreException("Failed deleting share " + name);

                            transaction.Commit();
                        }
                    }
                    catch(DataStoreException)
                    {
                        throw;
                    }
                    catch (System.Exception e)
                    {
                        throw new DataStoreException("Failed deleting share " + name, e);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the path for the specified share.
        /// </summary>
        /// <param name="name">The name of the share.</param>
        /// <param name="path">The new path.</param>
        /// 
        /// <exception cref="ShareNotFoundException">
        /// When the specified share does not exist
        /// </exception>
        public void UpdatePathForShare(string name, string path)
        {
            using (DbConnection connection = DbFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                using (DbCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE SHARES SET PATH = @path WHERE NAME = @name";

                    DbParameter parameter = DbFactory.CreateParameter();
                    parameter.DbType = DbType.String;
                    parameter.ParameterName = "@name";
                    parameter.Value = name;
                    command.Parameters.Add(parameter);

                    parameter = DbFactory.CreateParameter();
                    parameter.DbType = DbType.String;
                    parameter.ParameterName = "@path";
                    parameter.Value = path;
                    command.Parameters.Add(parameter);

                    try
                    {
                        connection.Open();

                        using (DbTransaction transaction = connection.BeginTransaction())
                        {
                            int rows = command.ExecuteNonQuery();

                            if (rows != 1)
                                throw new DataStoreException("Failed updating share " + name);

                            transaction.Commit();
                        }
                    }
                    catch (DataStoreException)
                    {
                        throw;
                    }
                    catch (System.Exception e)
                    {
                        throw new DataStoreException("Failed updating share " + name, e);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the name for share.
        /// </summary>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        /// 
        /// <exception cref="ShareNotFoundException">
        /// When the specified share does not exist
        /// </exception>
        public void UpdateNameForShare(string oldName, string newName)
        {
            using (DbConnection connection = DbFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                using (DbCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE SHARES SET NAME = @newname WHERE NAME = @oldname";

                    DbParameter parameter = DbFactory.CreateParameter();
                    parameter.DbType = DbType.String;
                    parameter.ParameterName = "@newname";
                    parameter.Value = newName;
                    command.Parameters.Add(parameter);

                    parameter = DbFactory.CreateParameter();
                    parameter.DbType = DbType.String;
                    parameter.ParameterName = "@oldName";
                    parameter.Value = oldName;
                    command.Parameters.Add(parameter);

                    try
                    {
                        connection.Open();

                        using (DbTransaction transaction = connection.BeginTransaction())
                        {
                            int rows = command.ExecuteNonQuery();

                            if (rows != 1)
                                throw new DataStoreException("Failed updating share " + oldName);

                            transaction.Commit();
                        }
                    }
                    catch (DataStoreException)
                    {
                        throw;
                    }
                    catch (System.Exception e)
                    {
                        throw new DataStoreException("Failed updating share " + oldName, e);
                    }
                }
            }
        }

        /// <summary>
        /// Return an <see cref="IEnumerable{T}"/> containing all the
        /// share names.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all the
        /// share names</returns>
        public IEnumerable<string> ShareNames()
        {
            return Shares().Select(s => s.Name);
        }

        /// <summary>
        /// Returns the root path for the specified share.
        /// </summary>
        /// <param name="name">The name of the share.</param>
        /// <returns>The root path for the specified share.</returns>
        /// <exception cref="ShareNotFoundException">
        /// When the specified share does not exist
        /// </exception>
        public string PathForShare(string name)
        {
            using (DbConnection connection = DbFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                using (DbCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT PATH FROM SHARES WHERE NAME = @name";

                    DbParameter parameter = DbFactory.CreateParameter();
                    parameter.DbType = DbType.String;
                    parameter.ParameterName = "@name";
                    parameter.Value = name;
                    command.Parameters.Add(parameter);

                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (reader.Read())
                            {
                                return reader.GetString(0);
                            }
                            else
                            {
                                throw new DataStoreException("Unable to find share " + name);
                            }
                        }
                    }
                    catch(DataStoreException)
                    {
                        throw;
                    }
                    catch (System.Exception e)
                    {
                        throw new DataStoreException("Unable to find share " + name, e);
                    }
                }
            }
        }

        /// <summary>
        /// Return all the share data
        /// </summary>
        /// <returns>the share data</returns>
        public IEnumerable<ShareData> Shares()
        {
            IList<ShareData> result = new List<ShareData>();

            using (DbConnection connection = DbFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                using (DbCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT NAME, PATH FROM SHARES";

                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(
                                    new ShareData
                                        {
                                            Name = reader.GetString(0),
                                            Path = reader.GetString(1)
                                        }
                                    );
                            }

                        }
                    }
                    catch (System.Exception e)
                    {
                        throw new DataStoreException("Failed reading shares.", e);
                    }
                }
                return result;
            }
        }

        #endregion
    }
}
