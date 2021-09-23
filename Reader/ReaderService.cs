using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EntitiesAndModels;

using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Linq;

namespace Reader
{
    public class ReaderService : IReaderService
    {
        private readonly IOptions<ReaderConfig> options;

        public ReaderService(IOptions<ReaderConfig> options)
        {
            this.options = options;
        }

        public async Task<IEnumerable<GetItems>> GetAllItems()
        {
            List<GetItems> returnValue = new List<GetItems>();
            using (MySqlConnection connection = new MySqlConnection(options.Value.ConnectionString))
            {
                connection.Open();
                
                string sql = "SELECT * FROM ItemMain";
                sql += " ORDER BY No_KK";
                IEnumerable<GetItemsMain> itemMain = await connection.QueryAsync<GetItemsMain>(sql);
                
                foreach(GetItemsMain main in itemMain)
                {
                    object sendObj = new { main.No_KK };
                    sql = " SELECT * FROM ItemDetail WHERE No_KK = @No_KK";

                    IEnumerable<GetItemsDetail> itemDetail = await connection.QueryAsync<GetItemsDetail>(sql, sendObj);

                    GetItems item = new GetItems()
                    {
                        ID_Main = main.ID_Main,
                        No_KK = main.No_KK,
                        Tanggal_Terbit = main.Tanggal_Terbit,
                        Nama_KK = main.Nama_KK,
                        Alamat = main.Alamat,
                        Details = itemDetail.ToList()
                    };

                    returnValue.Add(item);
                }

                connection.Close();

                return returnValue;
            }
        }

        public async Task<IEnumerable<GetItems>> GetItemsByNoKK(string filter)
        {
            List<GetItems> returnValue = new List<GetItems>();
            using (MySqlConnection connection = new MySqlConnection(options.Value.ConnectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM ItemMain";
                if (!String.IsNullOrWhiteSpace(filter))
                {
                    filter = "%" + filter + "%";
                    sql += " WHERE No_KK LIKE @filter";
                }

                object sendObject = new { filter };
                IEnumerable<GetItemsMain> itemMain = await connection.QueryAsync<GetItemsMain>(sql, sendObject);

                foreach (GetItemsMain main in itemMain)
                {
                    object sendObj = new { main.No_KK };
                    sql = " SELECT * FROM ItemDetail WHERE No_KK = @No_KK";

                    IEnumerable<GetItemsDetail> itemDetail = await connection.QueryAsync<GetItemsDetail>(sql, sendObj);

                    GetItems item = new GetItems()
                    {
                        ID_Main = main.ID_Main,
                        No_KK = main.No_KK,
                        Tanggal_Terbit = main.Tanggal_Terbit,
                        Nama_KK = main.Nama_KK,
                        Alamat = main.Alamat,
                        Details = itemDetail.ToList()
                    };

                    returnValue.Add(item);
                }

                connection.Close();

                return returnValue;
            }
        }

        public async Task<IEnumerable<GetItems>> GetItemsByNamaKK(string filter)
        {
            List<GetItems> returnValue = new List<GetItems>();
            using (MySqlConnection connection = new MySqlConnection(options.Value.ConnectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM ItemMain";
                if (!String.IsNullOrWhiteSpace(filter))
                {
                    filter = "%" + filter + "%";
                    sql += " WHERE Nama_KK LIKE @filter";
                }

                object sendObject = new { filter };
                IEnumerable<GetItemsMain> itemMain = await connection.QueryAsync<GetItemsMain>(sql, sendObject);

                foreach (GetItemsMain main in itemMain)
                {
                    object sendObj = new { main.No_KK };
                    sql = " SELECT * FROM ItemDetail WHERE No_KK = @No_KK";

                    IEnumerable<GetItemsDetail> itemDetail = await connection.QueryAsync<GetItemsDetail>(sql, sendObj);

                    GetItems item = new GetItems()
                    {
                        ID_Main = main.ID_Main,
                        No_KK = main.No_KK,
                        Tanggal_Terbit = main.Tanggal_Terbit,
                        Nama_KK = main.Nama_KK,
                        Alamat = main.Alamat,
                        Details = itemDetail.ToList()
                    };

                    returnValue.Add(item);
                }

                connection.Close();

                return returnValue;
            }
        }

        public async Task<IEnumerable<GetItems>> GetItemsByNamaLengkap(string filter)
        {
            List<GetItems> returnValue = new List<GetItems>();
            using (MySqlConnection connection = new MySqlConnection(options.Value.ConnectionString))
            {
                if (!String.IsNullOrWhiteSpace(filter))
                {
                    filter = "%" + filter + "%";
                } else
                {
                    filter = "";
                }

                connection.Open();

                string sql = "SELECT * FROM ItemMain";
                IEnumerable<GetItemsMain> itemMain = await connection.QueryAsync<GetItemsMain>(sql);

                foreach (GetItemsMain main in itemMain)
                {
                    

                    object sendObj = new { main.No_KK, Nama_Lenkap = filter };
                    sql = " SELECT * FROM ItemDetail WHERE No_KK = @No_KK AND Nama_Lengkap LIKE @Nama_Lenkap";

                    IEnumerable<GetItemsDetail> itemDetail = await connection.QueryAsync<GetItemsDetail>(sql, sendObj);

                    GetItems item = new GetItems()
                    {
                        ID_Main = main.ID_Main,
                        No_KK = main.No_KK,
                        Tanggal_Terbit = main.Tanggal_Terbit,
                        Nama_KK = main.Nama_KK,
                        Alamat = main.Alamat,
                        Details = itemDetail.ToList()
                    };

                    returnValue.Add(item);
                }

                connection.Close();

                return returnValue;
            }
        }

        public async Task<IEnumerable<GetItems>> GetItemsByNIK(string filter)
        {
            List<GetItems> returnValue = new List<GetItems>();
            using (MySqlConnection connection = new MySqlConnection(options.Value.ConnectionString))
            {
                if (!String.IsNullOrWhiteSpace(filter))
                {
                    filter = "%" + filter + "%";
                }
                else
                {
                    filter = "";
                }

                connection.Open();

                string sql = "SELECT * FROM ItemMain";
                IEnumerable<GetItemsMain> itemMain = await connection.QueryAsync<GetItemsMain>(sql);

                foreach (GetItemsMain main in itemMain)
                {


                    object sendObj = new { main.No_KK, NIK = filter };
                    sql = " SELECT * FROM ItemDetail WHERE No_KK = @No_KK AND NIK LIKE @NIK";

                    IEnumerable<GetItemsDetail> itemDetail = await connection.QueryAsync<GetItemsDetail>(sql, sendObj);

                    GetItems item = new GetItems()
                    {
                        ID_Main = main.ID_Main,
                        No_KK = main.No_KK,
                        Tanggal_Terbit = main.Tanggal_Terbit,
                        Nama_KK = main.Nama_KK,
                        Alamat = main.Alamat,
                        Details = itemDetail.ToList()
                    };

                    returnValue.Add(item);
                }

                connection.Close();

                return returnValue;
            }
        }
    }
}
