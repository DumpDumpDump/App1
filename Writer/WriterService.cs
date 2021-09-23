using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using EntitiesAndModels;

using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Writer
{
    public class WriterService : IWriterService
    {
        private readonly IOptions<WriterConfig> options;

        public WriterService(IOptions<WriterConfig> options)
        {
            this.options = options;
        }

        public async Task<PostItemMain> PostItemMain(PostItemMain data)
        {
            using (MySqlConnection connection = new MySqlConnection(options.Value.ConnectionString))
            {
                connection.Open();
                IDbTransaction beginTran = null;

                try
                {
                    beginTran = connection.BeginTransaction();
                    string sql = "INSERT INTO ItemMain (No_KK, Tanggal_Terbit, Nama_KK, Alamat) VALUES";
                    sql += " (@No_KK, @Tanggal_Terbit, @Nama_KK, @Alamat); SELECT * FROM ItemMain WHERE NO_KK = @No_KK";
                    PostItemMain result = await connection.QueryFirstOrDefaultAsync<PostItemMain>(sql, data, beginTran);
                    beginTran.Commit();
                    connection.Close();
                    return WriterHelper.ObjectConverter<PostItemMain>(result);
                }
                catch (Exception ex)
                {
                    if (beginTran != null)
                    {
                        beginTran.Rollback();
                        connection.Close();
                    }
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public async Task<IEnumerable<GetItems>> PostItemDetail(PostItemDetail data)
        {
            using (MySqlConnection connection = new MySqlConnection(options.Value.ConnectionString))
            {
                connection.Open();
                IDbTransaction beginTran = null;

                string filter = data.No_KK;

                try
                {
                    beginTran = connection.BeginTransaction();
                    string sql = "INSERT INTO ItemDetail (No_KK, Nama_Lengkap, NIK, Jenis_Kelamin, Tempat_Lahir, Tanggal_Lahir, Agama, Pendidikan, Jenis_Pekerjaan, Status_Perkawinan, Status_Hubungan, Kewarganegaraan, Nama_Ayah, Nama_Ibu, Penduduk_Tetap) VALUES";
                    sql += " (@No_KK, @Nama_Lengkap, @NIK, @Jenis_Kelamin, @Tempat_Lahir, @Tanggal_Lahir, @Agama, @Pendidikan, @Jenis_Pekerjaan, @Status_Perkawinan, @Status_Hubungan, @Kewarganegaraan, @Nama_Ayah, @Nama_Ibu, @Penduduk_Tetap);";

                    sql += " SELECT IM.ID_Main, IM.Tanggal_Terbit, IM.Nama_KK, IM.Alamat, ID.* FROM ItemMain IM";
                    sql += " JOIN ItemDetail ID ON IM.No_KK = ID.No_KK";
                    sql += " WHERE IM.No_KK LIKE @No_KK";
                    sql += " ORDER BY IM.No_KK";

                    IEnumerable<GetItems> result = await connection.QueryAsync<GetItems>(sql, data, beginTran);
                    beginTran.Commit();
                    connection.Close();
                    return WriterHelper.ObjectConverter<IEnumerable<GetItems>>(result);
                }
                catch (Exception ex)
                {
                    if (beginTran != null)
                    {
                        beginTran.Rollback();
                        connection.Close();
                    }
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public async Task<PatchItemMain> PatchItemMain(PatchItemMain data)
        {
            using (MySqlConnection connection = new MySqlConnection(options.Value.ConnectionString))
            {
                connection.Open();
                IDbTransaction beginTran = null;
                int setCount = 0;

                try
                {
                    beginTran = connection.BeginTransaction();

                    #region Update Query
                    string sql = "UPDATE ItemMain";
                    if(data.No_KK != null)
                    {
                        sql += " SET No_KK = @No_KK";
                        setCount++;
                    }
                    if (data.Tanggal_Terbit != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Tanggal_Terbit = @Tanggal_Terbit";
                        setCount++;
                    }
                    if (data.Nama_KK != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Nama_KK = @Nama_KK";
                        setCount++;
                    }
                    if (data.Alamat != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Alamat = @Alamat";
                        setCount++;
                    }
                    sql += " WHERE ID_Main = @ID_Main;";
                    #endregion

                    sql += " SELECT * FROM ItemMain WHERE ID_Main = @ID_Main";
                    PatchItemMain result = await connection.QueryFirstOrDefaultAsync<PatchItemMain>(sql, data, beginTran);
                    beginTran.Commit();
                    connection.Close();
                    return WriterHelper.ObjectConverter<PatchItemMain>(result);
                }
                catch (Exception ex)
                {
                    if (beginTran != null)
                    {
                        beginTran.Rollback();
                        connection.Close();
                    }
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public async Task<IEnumerable<GetItems>> PatchItemDetail(PatchItemDetail data)
        {
            using (MySqlConnection connection = new MySqlConnection(options.Value.ConnectionString))
            {
                connection.Open();
                IDbTransaction beginTran = null;
                int setCount = 0;

                try
                {
                    beginTran = connection.BeginTransaction();

                    #region Update Query
                    string sql = "UPDATE ItemMain";
                    if (data.No_KK != null)
                    {
                        sql += " SET No_KK = @No_KK";
                        setCount++;
                    }
                    if (data.Nama_Lengkap != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Nama_Lengkap = @Nama_Lengkap";
                        setCount++;
                    }
                    if (data.NIK != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " NIK = @NIK";
                        setCount++;
                    }
                    if (data.Jenis_Kelamin != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Jenis_Kelamin = @Jenis_Kelamin";
                        setCount++;
                    }
                    if (data.Tempat_Lahir != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Tempat_Lahir = @Tempat_Lahir";
                        setCount++;
                    }
                    if (data.Tanggal_Lahir != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Tanggal_Lahir = @Tanggal_Lahir";
                        setCount++;
                    }
                    if (data.Agama != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Agama = @Agama";
                        setCount++;
                    }
                    if (data.Pendidikan != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Pendidikan = @Pendidikan";
                        setCount++;
                    }
                    if (data.Jenis_Pekerjaan != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Jenis_Pekerjaan = @Jenis_Pekerjaan";
                        setCount++;
                    }
                    if (data.Status_Perkawinan != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Status_Perkawinan = @Status_Perkawinan";
                        setCount++;
                    }
                    if (data.Status_Hubungan != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Status_Hubungan = @Status_Hubungan";
                        setCount++;
                    }
                    if (data.Kewarganegaraan != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Kewarganegaraan = @Kewarganegaraan";
                        setCount++;
                    }
                    if (data.Nama_Ayah != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Nama_Ayah = @Nama_Ayah";
                        setCount++;
                    }
                    if (data.Nama_Ibu != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Nama_Ibu = @Nama_Ibu";
                        setCount++;
                    }
                    if (data.Penduduk_Tetap != null)
                    {
                        sql += setCount > 0 ? ", " : " SET ";
                        sql += " Penduduk_Tetap = @Penduduk_Tetap";
                        setCount++;
                    }
                    sql += " WHERE ID_Detail = @ID_Detail;";
                    #endregion

                    sql += " SELECT IM.ID_Main, IM.Tanggal_Terbit, IM.Nama_KK, IM.Alamat, ID.* FROM ItemMain IM";
                    sql += " JOIN ItemDetail ID ON IM.No_KK = ID.No_KK";
                    sql += " WHERE ID.ID_Detail = @ID_Detail";
                    IEnumerable<GetItems> result = await connection.QueryAsync<GetItems>(sql, data, beginTran);
                    beginTran.Commit();
                    connection.Close();
                    return WriterHelper.ObjectConverter<IEnumerable<GetItems>>(result);
                }
                catch (Exception ex)
                {
                    if (beginTran != null)
                    {
                        beginTran.Rollback();
                        connection.Close();
                    }
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public async Task DeleteItemMain(string ID)
        {
            object sendObject = new { ID };

            using (MySqlConnection connection = new MySqlConnection(options.Value.ConnectionString))
            {
                connection.Open();
                IDbTransaction beginTran = null;

                try
                {
                    beginTran = connection.BeginTransaction();
                    string sql = "DELETE FROM ItemMain WHERE ID_Main = @ID;";
                    await connection.QueryFirstOrDefaultAsync(sql, sendObject, beginTran);

                    beginTran.Commit();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (beginTran != null)
                    {
                        beginTran.Rollback();
                        connection.Close();
                    }
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public async Task DeleteItemDetail(string ID)
        {
            object sendObject = new { ID };

            using (MySqlConnection connection = new MySqlConnection(options.Value.ConnectionString))
            {
                connection.Open();
                IDbTransaction beginTran = null;

                try
                {
                    beginTran = connection.BeginTransaction();
                    string sql = "DELETE FROM ItemDetail WHERE ID_Detail = @ID;";
                    await connection.QueryFirstOrDefaultAsync(sql, sendObject, beginTran);

                    beginTran.Commit();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (beginTran != null)
                    {
                        beginTran.Rollback();
                        connection.Close();
                    }
                    throw new ArgumentException(ex.Message);
                }
            }
        }
    }
}
