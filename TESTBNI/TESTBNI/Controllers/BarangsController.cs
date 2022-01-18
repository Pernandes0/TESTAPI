using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TESTBNI.Context;
using TESTBNI.Models;
using TESTBNI.Repositories;

namespace TESTBNI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarangsController : ControllerBase
    {
        private readonly BarangRepository barangRepository;
        private readonly MyContext myContext;

        public BarangsController(BarangRepository repository)
        {
            barangRepository = repository;
        }
        [HttpGet("get")]
        public async Task<IEnumerable<Barang>> Get()
        {
            return await barangRepository.Get();
        }
        [HttpGet("{id}")]
        public async Task<Barang> Get(int id)
        {
            return await barangRepository.Get(id);
        }
        [HttpPost("post")]
        public async Task<ActionResult<Barang>> Post(Barang barang)
        {
            try
            {
                if (barang == null)
                {
                    return BadRequest(new { message = "gagal" });
                }
                var barangname = await barangRepository.GetBarangByName(barang.Name);
                if (barangname != null)
                {
                    return BadRequest(new { message = "nama barang sudah digunakan" });
                }
                var createbarang = barangRepository.Create(barang);
                return Ok(new { message = "Sukses" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    e.Message);
            }
        }
        [HttpPut("edit/{id}")]
        public IActionResult Put(int id, Barang barang)
        {
            barangRepository.Update(id, barang);
            return Ok(new { message = "barang berhasil diedit" });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            barangRepository.Delete(id);
            return Ok(new { message = "barang berhasil dihapus" });
        }
        [HttpGet("cari/{name}")]
        public bool Cari(string name)
        {
            var check = myContext.Barangs.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (check !=null)
            {
                return true;
            }
            return false;
        }
    }
}
