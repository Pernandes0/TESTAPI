using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TESTBNI.Context;
using TESTBNI.Models;

namespace TESTBNI.Repositories
{
    public class BarangRepository : IBarangRepository
    {
        private readonly MyContext context;
        private readonly IConfiguration configuration;
        private readonly SqlConnection connection;

        DynamicParameters parameters = new DynamicParameters();

        public BarangRepository(MyContext mycontext, IConfiguration myconfiguration)
        {
            context = mycontext;
            configuration = myconfiguration;
            connection = new SqlConnection(configuration["ConnectionStrings:TestBNI"]);
        }
        public int Create(Barang barang)
        {
            var prosedureName = "SP_Create_Barang";
            parameters.Add("@Name", barang.Name);
            parameters.Add("@Quantity", barang.Quantity);
            parameters.Add("@Deskripsi", barang.Deskripsi);
            parameters.Add("@Harga", barang.Harga);
            var create = connection.Execute(prosedureName, parameters, commandType: CommandType.StoredProcedure);
            return create;

        }

        public int Delete(int id)
        {
            var prosedureDelete = "SP_Delete_Barang";
            var delete = connection.Execute(prosedureDelete, new { id }, commandType: CommandType.StoredProcedure);
            return delete;
        }
        // render dara asingcronus == 
        public async Task<IEnumerable<Barang>> Get()
        {
            var prosedureGetall = "SP_Get_Barang";
            var getBarang = await connection.QueryAsync<Barang>(prosedureGetall, commandType: CommandType.StoredProcedure);
            // kenapa ini pake queryasync trus pubic async 
            return getBarang;
        }

        public async Task<Barang> Get(int Id)
        {
            var prosedureGetById = "SP_GetBarang_Id";
            var getbyId = await connection.QueryAsync<Barang>(prosedureGetById, new { Id }, commandType: CommandType.StoredProcedure);
            // kenapa pake firstordefault
            return getbyId.FirstOrDefault();
        }

        public async Task<Barang> GetBarangByName(string name)
        {
            // using entityframeworkcore
            return await context.Barangs.FirstOrDefaultAsync(x => x.Name == name);
        }

        public int Update(int id, Barang barang)
        {
            var prosedureUpdate = "SP_Update_Barang";
            var update = connection.Execute(prosedureUpdate, new { id, barang.Name, barang.Quantity, barang.Deskripsi, barang.Harga }, commandType: CommandType.StoredProcedure);
            return update;
        }
        public async Task<Barang> Check(string name)
        {
            var prosedureCheck = "SP_Check_Barang";
            var check = await connection.QueryAsync<Barang>(prosedureCheck, new { name }, commandType: CommandType.StoredProcedure);
            return check.FirstOrDefault();
        }
    }
}
