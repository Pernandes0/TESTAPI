using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TESTBNI.Models;

namespace TESTBNI.Repositories
{
    public interface IBarangRepository 
    {
        Task<IEnumerable<Barang>> Get();
        Task<Barang> Get(int Id);
        int Create(Barang barang);
        int Update(int id, Barang barang);
        int Delete(int id);
        Task<Barang> GetBarangByName(string name);
    }
}
