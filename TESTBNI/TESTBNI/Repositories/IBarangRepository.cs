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

    }
}
